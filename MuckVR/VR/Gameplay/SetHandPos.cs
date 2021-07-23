using System;
using System.Collections;
using UnityEngine;
using Valve.VR;

namespace MuckVR.VR.Gameplay
{
    public class SetHandPos : MonoBehaviour
    {
        Transform LTrans;
        Transform RTrans;

        GameObject LHand;
        GameObject RHand;

        SteamVR_RenderModel LModel;
        SteamVR_RenderModel RModel;

        void Awake()
        {
            LTrans = transform.Find("Controller (left)");
            RTrans = transform.Find("Controller (right)");

            LModel = LTrans.Find("Model").GetComponent<SteamVR_RenderModel>();
            RModel = RTrans.Find("Model").GetComponent<SteamVR_RenderModel>();

            LHand = LTrans.Find("LeftHand").gameObject;
            RHand = RTrans.Find("RightHand").gameObject;

            StartCoroutine(AttachHand());
        }

        IEnumerator AttachHand()
        {
            while (!LModel.initializedAttachPoints && !RModel.initializedAttachPoints) yield return null;

            RModel.gameObject.SetActive(false);
            LModel.gameObject.SetActive(false);

            LHand.transform.parent = LModel.GetComponentTransform("grip");
            RHand.transform.parent = RModel.GetComponentTransform("grip");

            LHand.transform.localPosition = new Vector3(0, 0.06f, 0.028f);
            LHand.transform.localRotation = Quaternion.Euler(0, -90, -270);
            LHand.transform.localScale = Vector3.one * 0.2356684f;

            RHand.transform.localPosition = new Vector3(0, 0.06f, 0.028f);
            RHand.transform.localRotation = Quaternion.Euler(180, 270, 450);
            RHand.transform.localScale = Vector3.one * 0.2356684f;

            LHand.transform.parent = LTrans;
            RHand.transform.parent = RTrans;
        }
    }

}
