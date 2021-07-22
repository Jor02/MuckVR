﻿using System;
using System.Collections;
using System.Text;
using UnityEngine;
using Valve.VR;

namespace MuckVR.VR.Gameplay
{
    public class InitializeUI : MonoBehaviour
    {
        RectTransform UITransform;
        Camera camera;

        readonly float width = 0;
        readonly float height = 0;

        public InitializeUI()
        {
            Canvas UI = GameObject.Find("/UI (1)").GetComponent<Canvas>();
            UI.renderMode = RenderMode.WorldSpace;
            
            camera = Camera.main;

            UITransform = UI.GetComponent<RectTransform>();
            UITransform.parent = camera.transform;
            UITransform.localRotation = Quaternion.Euler(Vector3.zero);

            width = 1 / UITransform.rect.width;
            height = 1 / UITransform.rect.height;

            ResizeCanvas(1f);
            SetActive();
        }

        void SetActive()
        {
            UITransform.Find("Crosshair").gameObject.SetActive(false);
            UITransform.Find("Chat").gameObject.SetActive(false);
            UITransform.GetComponentInChildren<ExtraUI>().gameObject.SetActive(false);
        }

        void ResizeCanvas(float distance)
        {
            float frustumHeight = 2.0f * distance * Mathf.Tan(SteamVR.instance.fieldOfView * 0.5f * Mathf.Deg2Rad);
            //float frustumWidth = frustumHeight * SteamVR.instance.aspect;

            Vector3 camsize = camera.transform.lossyScale;
            camsize.x = 1 / camsize.x;
            camsize.y = 1 / camsize.y;
            camsize.z = 1 / camsize.z;

            //UITransform.localScale = camsize * width * frustumWidth;
            UITransform.localScale = camsize * width * frustumHeight;
            UITransform.localPosition = Vector3.forward * camsize.z * distance;
        }

        public static GameObject CreateLoader(Vector3 position, float scale)
        {
            GameObject Loader = Instantiate(Base.vrAssets.LoadAsset<GameObject>("InvertedCube"));
            Loader.transform.position = position;
            Loader.transform.localScale = Vector3.one * scale;

            RectTransform loadingScreen = GameObject.Find("/UI (1)/LoadingScreen").GetComponent<RectTransform>();
            Transform canvas = Loader.transform.Find("Canvas");

            loadingScreen.parent = canvas;

            loadingScreen.localPosition = Vector3.zero;
            loadingScreen.localScale = Vector3.one;
            loadingScreen.offsetMax = Vector2.zero;
            loadingScreen.offsetMin = Vector2.zero;

            return Loader;
        }
    }
}
