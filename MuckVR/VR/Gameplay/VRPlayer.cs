using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MuckVR.VR.Gameplay
{
    public class VRPlayer : MonoBehaviour
    {
        public static VRPlayer instance;

        public VRInput input { get; }

        public Transform VRCamera { get; }
        CapsuleCollider collider;

        public Transform LTrans { get; }
        public Transform RTrans { get; }

        public Controller LHand { get; }
        public Controller RHand { get; }

        public VRPlayer()
        {
            instance = this;

            gameObject.AddComponent<SetHandPos>();
            VRCamera = GetComponentInChildren<Camera>().transform;

            PlayerMovement.Instance.playerCam = VRCamera;
            input = PlayerMovement.Instance.gameObject.AddComponent<VRInput>();

            collider = PlayerMovement.Instance.gameObject.GetComponent<CapsuleCollider>();

            LTrans = transform.Find("Controller (left)");
            RTrans = transform.Find("Controller (right)");

            LHand = LTrans.Find("LeftHand").gameObject.AddComponent<Controller>();
            RHand = RTrans.Find("RightHand").gameObject.AddComponent<Controller>();
        }

        void FixedUpdate()
        {
            Vector3 center = VRCamera.localPosition;
            center.x *= 1 / collider.transform.lossyScale.x;
            center.y = 0;
            center.z *= 1 / collider.transform.lossyScale.z;
            collider.center = center;
        }
    }


}
