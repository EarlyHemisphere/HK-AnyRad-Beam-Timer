using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace BeamTimer {
    public class BeamTimer : Mod {
        internal static BeamTimer instance;

        internal static bool created;

        public BeamTimer() : base("Beam Timer") {
            instance = this;
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            instance = this;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += CheckForRadiance;
            Log("Initialized");
        }

        public override string GetVersion() {
            return "1.0.0.0";
        }

        public void Unload() {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= CheckForRadiance;
        }

        private static void CheckForRadiance(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to) {
            if (to.name != "GG_Radiance" || created) {
                return;
            }

            new GameObject("BeamTimer", typeof(Timer));
            created = true;
        }
    }
}
