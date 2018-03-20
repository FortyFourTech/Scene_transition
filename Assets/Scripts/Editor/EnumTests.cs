using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using System;
using System.Collections;

using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Testing {
    public partial class EnumTests {

        [Test]
        public void TestHackGamesEnum() {
            TestExtensionMethods(typeof(HackGame));
        }

        [Test]
        public void TestHackGamesPathes() {
            var values = Enum.GetValues(typeof(HackGame)) as HackGame[];
            foreach (var val in values) {
                GameObject gameGo = null;
                try {
                    gameGo = GameProvider.Instance.GetGame(val);
                } catch {
                    Assert.IsNotNull(gameGo, "path for game " + val.ToString() + " is wrong");
                } finally {
                    UnityEngine.Object.DestroyImmediate(gameGo);
                }
            }
        }

        [Test]
        public void TestScenesEnum() {
            //var values = Enum.GetValues(typeof(ProjectScenes)) as ProjectScenes[];
            var valuesNum = (Enum.GetValues(typeof(ProjectScenes)) as ProjectScenes[]).Length;
            var scenesNum = SceneManager.sceneCountInBuildSettings;

            Assert.AreEqual(scenesNum, valuesNum, "scene number not equals to describes scene numeb");
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        //[UnityTest]
        //public IEnumerator EnumTestsWithEnumeratorPasses() {
        //	// Use the Assert class to test conditions.
        //	// yield to skip a frame
        //	yield return null;
        //}

        static void TestExtensionMethods(Type enumeration) {
            var values = Enum.GetValues(enumeration);
            var methods = GetExtensionMethods(enumeration);

            foreach (var val in values) {
                foreach (var method in methods) {
                    method.Invoke(null, new object[] { val });
                }
            }
        }

        static IEnumerable<MethodInfo> GetExtensionMethods(Type extendedType) {
            var assembly = extendedType.Assembly;
            var query = from type in assembly.GetTypes()
                        where type.IsSealed && !type.IsGenericType && !type.IsNested
                        from method in type.GetMethods(BindingFlags.Static
                            | BindingFlags.Public | BindingFlags.NonPublic)
                        where method.IsDefined(typeof(ExtensionAttribute), false)
                        where method.GetParameters()[0].ParameterType == extendedType
                        select method;
            return query;
        }
    }
}
