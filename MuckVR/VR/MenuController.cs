using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MuckVR.VR.UI;
using Valve.VR;
using Steamworks;

namespace MuckVR.VR
{
    class MenuController : MonoBehaviour
    {
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
        
        /// <summary>
        /// Enables vr in the main menu
        /// </summary>
        public MenuController()
        {
            //Initalize controlls and controllers
            SteamVR_Actions.PreInitialize();

            //Instantiate VR camera rig from asset bundle
            GameObject CameraRig = Instantiate(Base.vrAssets.LoadAsset<GameObject>("MenuRig"));

            //Place VR camera rig at the right position
            CameraRig.transform.position = new Vector3(103.17f, 22.54f, 652.07f);
            CameraRig.transform.localScale = Vector3.one * 3.0626f;

            //Add UI input to controllers
            CameraRig.transform.Find("Controller (left)").gameObject.AddComponent<VRUIInput>();
            CameraRig.transform.Find("Controller (right)").gameObject.AddComponent<VRUIInput>();

            //Add raycast colliders to buttons
            StartCoroutine(AddButtons());

#if DEBUGNOMENU
            //Automatically starts the game on the DEBUGNOMENU configuration
            StartCoroutine(StartGame());
        }

        /// <summary>
        /// Creates a lobby and starts it
        /// </summary>
        IEnumerator StartGame()
        {
            while (!SteamManager.Instance.ConnectedToSteam()) yield return null;
            Resources.FindObjectsOfTypeAll<MenuUI>()[0].StartLobby();
            yield return new WaitForSeconds(1f);
            Resources.FindObjectsOfTypeAll<MenuUI>()[0].StartGame();
        }
#else
        }
#endif

        /// <summary>
        /// Adds raycast colliders to buttons
        /// </summary>
        IEnumerator AddButtons()
        {
            Transform settings = GameObject.Find("/UI/Lobby/LobbySettings/SettingsPanel").transform;
            Transform s1 = settings.Find("Setting_Difficulty");
            Transform s2 = settings.Find("Setting_PlayerDamage");
            Transform s3 = settings.Find("Setting_Gamemdoe");
            while (s1.childCount == 0 && s2.childCount == 0 && s3.childCount == 0)
                yield return new WaitForEndOfFrame();
            
            foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
            {
                btn.gameObject.AddComponent<UIButtonCollider>();
            }
        }
    }
}
