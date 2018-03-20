using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class MainScript : MonoBehaviour {

    public bool initOnReady = true;

    public Dictionary<string, string> roomAreaDescriptions = new Dictionary<string, string> {
            { "Room1", "mctew_room1_adf" },
            { "Room2", "mctew_room2_adf" },
            { "MainScene", "hub_adf" },
            { "Room4", "white_adf" },
            { "Room5", "blue_adf" },
            { "Room6", "green_adf" },
        };

    void Awake() {
        var mains = FindObjectsOfType<MainScript>();
        if (mains.Length > 1) {
            if (initOnReady) initTango();

            Destroy(gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);

		Input.simulateMouseWithTouches = true;

        ImportADMs();
        initTango();

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        //StartCoroutine(API.NetworkService.DoAuthorization());

	}

    private void OnDestroy() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1) {
        initTango();
    }

    private void initTango() {
		var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;

		//TextObject.text = sceneName;
        
        mCurrentAD = roomAreaDescriptions.ContainsKey(sceneName) ?
                roomAreaDescriptions[sceneName] :
                roomAreaDescriptions["MainScene"];
        //DebugLog("set area description " + m_areaUUID);
		m_tangoApplication = FindObjectOfType<TangoApplication> ();
        if (m_tangoApplication != null) {
            m_tangoApplication.Register(this);
            m_tangoApplication.RequestPermissions();
		}
    }

	public void OnTangoPermissions(bool permissionsGranted){
        if (permissionsGranted && initOnReady) {
            var uuid = getAdUuid(mCurrentAD);
            m_tangoApplication.Startup(AreaDescription.ForUUID(uuid));
        }
	}
	public void OnTangoServiceConnected(){}
	public void OnTangoServiceDisconnected(){}

    string getAdUuid(string adName) {
        var name = adName.Remove(adName.Length - 4);
        var assetName = name + "_info";
        var data = Resources.Load(assetName) as TextAsset;
        return data.text;
    }

    void ImportADMs() {
        Debug.Log("getting adms");
        var adms = AreaDescription.GetList();
        Debug.Log(adms == null ? "adms = null" : "gotten adms: " + adms.Length.ToString());
        if (adms == null || adms.Length == 0) {
            System.Action<string> ImportAreaDescription = delegate (string ADF_ID) {
                var dataPath = System.IO.Path.Combine(
                        Application.persistentDataPath,
                        ADF_ID
                    );
                //string dataPath = Application.persistentDataPath + "/" + ADF_ID;
                var assetPath = System.IO.Path.Combine(
                        Application.streamingAssetsPath,
                        ADF_ID
                    );
                //string assetPath = "StreamingAssets/" + ADF_ID;
                if (!File.Exists(dataPath)) {
                    var wwwfile = new WWW(assetPath);
                    while (!wwwfile.isDone) { }

                    //File.Create(dataPath);
                    File.WriteAllBytes(dataPath, wwwfile.bytes);

                    //File.Copy(assetPath, dataPath);
                } else {
                    this.DebugLog("file already exist");
                }
                
                Debug.Log("importing by path: " + dataPath);

                if (AreaDescription.ImportFromFile(dataPath)) this.DebugLog("ADM imported "+dataPath);
                else this.DebugLog("ADM import failed "+dataPath);

                //File.Delete(dataPath);
                // can not delete file here because AreaDescription.ImportFromFile is asyncronious and probably \
                // still not finished at the moment
            };

            var AdfList = roomAreaDescriptions.Values;

            foreach(var AdfId in AdfList) {
                ImportAreaDescription(AdfId);
            }
        }
    }
}
