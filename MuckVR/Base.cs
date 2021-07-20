using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BepInEx;
using TMPro;
using Valve.VR;

namespace MuckVR
{
    /// <summary>
    /// Main entrypoint for plugin
    /// </summary>
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    [BepInProcess("Muck.exe")]
    [BepInProcess("MuckVR.exe")]
    public class Base : BaseUnityPlugin
    {
        /// <summary>
        /// Main entry point for MuckVR
        /// </summary>
        void Awake()
        {
            //Prevent this object from getting destroyed
            DontDestroyOnLoad(gameObject);

            //Add SteamVR
            SteamVR_Settings settings = ScriptableObject.CreateInstance<SteamVR_Settings>();
            SteamVR.Initialize(true);

            //Subscribe to sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        #region Setup
        /// <summary>
        /// Prepares menu for VR
        /// </summary>
        private void InitMenu()
        {
            //Add plugin to version
            StartCoroutine(UpdateVersion());

            //Disable camera shaking + movement
            Camera.main.enabled = false;

            //Get UI
            Canvas UI = GameObject.Find("/UI").GetComponent<Canvas>();

            //Transform
            RectTransform UITransform = UI.GetComponent<RectTransform>();
            UITransform.position = new Vector3(103.16f, 26.24f, 663.05f); // Position
            UITransform.localScale = Vector3.one * 0.011f; // Scale
            UITransform.sizeDelta = new Vector2(1374, 1082); // Aspect

            //Add Graphic Raycaster components
            GameObject.Find("/UI/Main/Buttons").AddComponent<GraphicRaycaster>();
            GameObject.Find("/UI/StatusMessage/StatusMessage (1)").AddComponent<GraphicRaycaster>();

            /*//Add UIHover component to all buttons -= UIHover doesn't currently work
            foreach(Button btn in UI.GetComponentsInChildren<Button>())
            {
                btn.gameObject.AddComponent<VR.UI.UIHover>();
            }*/

            VR.MenuController menuController = new GameObject("MenuVR").AddComponent<VR.MenuController>();

            //World space
            UI.worldCamera = menuController.UICam;
            UI.renderMode = RenderMode.WorldSpace;
        }

        /// <summary>
        /// Prepares game for VR
        /// </summary>
        private void InitGame()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates game version in UI
        /// </summary>
        IEnumerator UpdateVersion()
        {
            //Find version
            GameObject VersionName = GameObject.Find("/UI/VersionName");

            //Resize version rect
            RectTransform rectTransform = VersionName.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(400, 100);

            TextMeshProUGUI version = VersionName.GetComponent<TextMeshProUGUI>();

            while (version.text == "ver_0.8")
            {
                yield return null;
            }

            //Add plugin info to version
            version.text += $"<br>{PluginInfo.NAME} ({PluginInfo.VERSION})";
        }
        #endregion

        #region Unity Messages
        /// <summary>
        /// Gets called on scene change
        /// </summary>
        private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode sceneMode)
        {
            switch (scene.name)
            {
                case "GameAfterLobby":
                    InitGame();
                    break;
                case "Menu":
                    InitMenu();
                    break;
            }
        }

        void Update()
        {
#if DEBUG
            if (Screen.fullScreen) Screen.fullScreen = false;
#endif
        }
        #endregion
    }
}
