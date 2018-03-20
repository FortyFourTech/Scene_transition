
using UnityEngine.Assertions;

public enum ProjectScenes {
    main,
    hub,
    corridor,
    professorsRoom,
    game1,
}

static class ScenesMethodsExtensions {
    public static int sceneIndex(this ProjectScenes scene) {
        var result = (int)scene;
        //switch (scene) {
        //    case ProjectScenes.main:
        //        result = 0;
        //        break;
        //    case ProjectScenes.corridor:
        //        result = 1;
        //        break;
        //    case ProjectScenes.professorsRoom:
        //        result = 2;
        //        break;
        //    default:
        //        result = 0;
        //        break;
        //}

        return result;
    }
}

public enum HackGame {
    lazors,
    xnb,
    freaky_architect,
    round_ball,
    shooter,
}

static class GamesMethodsExtensions {
    public static string prefabPath(this HackGame gameType) {
        var result = "Games/";
        switch (gameType) {
            case HackGame.lazors:
                result += "LazorsGame";
                break;
            case HackGame.xnb:
                result += "XNBGame";
                break;
            case HackGame.freaky_architect:
                result += "FAGame";
                break;
            case HackGame.round_ball:
                result += "RBGame";
                break;
            case HackGame.shooter:
                result += "ARShooter";
                break;
            default:
                Assert.IsTrue(false, "prefab path for hack game '" + gameType.ToString() + "' is not defined");
                break;
        }

        return result;
    }
}
