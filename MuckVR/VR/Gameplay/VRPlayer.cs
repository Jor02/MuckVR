using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MuckVR.VR.Gameplay
{
    public class VRPlayer : MonoBehaviour
    {
        public static VRPlayer instance;

        public VRInput input;

        public Transform VRCamera;
        CapsuleCollider collider;

        public VRPlayer()
        {
            instance = this;

            gameObject.AddComponent<SetHandPos>();
            VRCamera = GetComponentInChildren<Camera>().transform;

            PlayerMovement.Instance.playerCam = VRCamera;
            input = PlayerMovement.Instance.gameObject.AddComponent<VRInput>();

            collider = PlayerMovement.Instance.gameObject.GetComponent<CapsuleCollider>();
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
