using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SFCore.Utils;
using UnityEngine;
using UnityEngine.UI;
using Logger = Modding.Logger;

internal class Timer : MonoBehaviour {
    private GameObject abs;

    private GameObject knight;

    private Text timer;

    private float time;

    private bool actionAdded;

    private void Awake() {
        Logger.Log("Creating Canvas");
        gameObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        // No GraphicRaycaster here: this UI is display-only and must not intercept
        // pointer clicks meant for other UI (e.g. the pause menu).

        knight = GameObject.Find("Knight");
        timer = gameObject.AddComponent<Text>();
        timer.transform.SetParent(knight.transform);
        timer.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        timer.fontSize = 70;
        timer.raycastTarget = false;
        ResetTimer();
    }

    private void Update() {
        timer.enabled = BeamTimer.BeamTimer.settings.showTimer;

        if (abs == null) {
            abs = GameObject.Find("Absolute Radiance");
            actionAdded = false;
            return;
        }

        if (!actionAdded) {
            Logger.Log("[Beam Timer] - Added Timer Reset Action");
            GameObject.Find("Absolute Radiance").LocateMyFSM("Attack Commands").AddAction("Aim", new CallMethod {
                behaviour = this,
                methodName = "ResetTimer",
                parameters = new FsmVar[0],
                everyFrame = false
            });
            actionAdded = true;
        }

        time -= Time.deltaTime;
        timer.text = time.ToString();
    }

    public void ResetTimer() {
        time = 0.5f;
        timer.text = time.ToString();
    }
}
