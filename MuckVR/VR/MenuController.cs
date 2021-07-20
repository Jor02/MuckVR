using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MuckVR.VR.UI;
using Valve.VR;

namespace MuckVR.VR
{
    class MenuController : MonoBehaviour
    {
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("Teleport");

        public Camera UICam;
        readonly Controller lController;
        readonly Controller rController;

        public Controller curController { get; private set; }

        Transform sphere;

        VRInputModule inputModule;

        public MenuController()
        {
            SteamVR_Actions.PreInitialize();

            AssetBundle vrAssets = AssetBundle.LoadFromFile(Application.dataPath + "/vrassets");
            GameObject CameraRig = Instantiate(vrAssets.LoadAsset<GameObject>("MenuRig"));
            CameraRig.transform.position = new Vector3(103.17f, 22.54f, 652.07f);
            CameraRig.transform.localScale = Vector3.one * 3.0626f;

            inputModule = gameObject.AddComponent<VRInputModule>();
            inputModule.menu = this;

            UICam = inputModule.Cam;

            lController = CameraRig.transform.Find("Controller (left)").gameObject.AddComponent<Controller>().Init(SteamVR_Input_Sources.LeftHand);
            rController = CameraRig.transform.Find("Controller (right)").gameObject.AddComponent<Controller>().Init(SteamVR_Input_Sources.RightHand);

            SetController(false);

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            Destroy(sphere.GetComponent<SphereCollider>());
            sphere.transform.localScale = Vector3.one * 0.15f;
            sphere.name = "Dot";
        }

        void Update()
        {

            //Length Raycast
            float targetDist = inputModule.Distance != 0 ? inputModule.Distance : 15f;

            if (Physics.Raycast(UICam.transform.position, UICam.transform.forward, out RaycastHit hit, targetDist))
            {
                curController.SetLine(hit.distance);
                sphere.position = hit.point;
            }
            else
            {
                curController.SetLine(targetDist);
                sphere.position = UICam.transform.position + UICam.transform.forward * targetDist;
            }
        }

        public void GetInputs(PointerEventData eventData)
        {
            if (interactWithUI.GetStateDown(rController.pose.inputSource)) inputModule.ProcessDown(eventData);
            if (interactWithUI.GetStateUp(rController.pose.inputSource)) inputModule.ProcessUp(eventData);
        }

        void SetController(bool left)
        {
            curController = (left) ? lController : rController;
            UICam.transform.parent = curController.transform;
            UICam.transform.position = curController.transform.position;
            UICam.transform.rotation = curController.transform.rotation;
        }
    }
}
