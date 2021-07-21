using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MuckVR.VR.UI;
using Valve.VR;

namespace MuckVR.VR
{
    class MenuController : MonoBehaviour
    {
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        public Controller curController { get; private set; }

        public MenuController()
        {
            //Initalize controlls and controllers
            SteamVR_Actions.PreInitialize();

            //Instantiate VR camera rig from asset bundle
            AssetBundle vrAssets = AssetBundle.LoadFromFile(Application.dataPath + "/vrassets");
            GameObject CameraRig = Instantiate(vrAssets.LoadAsset<GameObject>("MenuRig"));
            vrAssets.Unload(false);

            //Place VR camera rig at the right position
            CameraRig.transform.position = new Vector3(103.17f, 22.54f, 652.07f);
            CameraRig.transform.localScale = Vector3.one * 3.0626f;

            //Add UI input to controllers
            CameraRig.transform.Find("Controller (left)").gameObject.AddComponent<VRUIInput>();
            CameraRig.transform.Find("Controller (right)").gameObject.AddComponent<VRUIInput>();

            //Add raycast colliders to buttons
            StartCoroutine(AddButtons());
        }

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
