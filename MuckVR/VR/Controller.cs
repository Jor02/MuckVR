using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Valve.VR;

namespace MuckVR.VR
{
    public class Controller : MonoBehaviour
    {
        public Transform rayPivot { get; private set; }

        //LineRenderer uiLine;

        public Controller()
        {
            rayPivot = transform.Find("HandDir");

            /*
            uiLine = gameObject.AddComponent<LineRenderer>();
            uiLine.material = new Material(Shader.Find("Particles/Standard Unlit"));
            uiLine.startColor = uiLine.endColor = Color.green;
            uiLine.startWidth = uiLine.endWidth = 0.01f;
            */
        }

        /*
        void Update()
        {
            uiLine.SetPosition(0, rayPivot.position);
            uiLine.SetPosition(1, rayPivot.position + rayPivot.forward * 1);
        }
        */

        /* //Don't think i'll need this 
        public Transform rayPivot { get; private set; }
        private LineRenderer uiLine;

        public SteamVR_Behaviour_Pose pose { get; private set; }

        public Controller DebugInit()
        {
            rayPivot = transform;

            uiLine = gameObject.AddComponent<LineRenderer>();
            uiLine.material = new Material(Shader.Find("Particles/Standard Unlit"));
            uiLine.startColor = uiLine.endColor = Color.green;
            uiLine.startWidth = uiLine.endWidth = 0.01f;
            return this;
        }

        public Controller Init(SteamVR_Input_Sources? source)
        {
            pose = GetComponent<SteamVR_Behaviour_Pose>();

            //rayPivot = transform.Find("Model/openxr_aim/attach"); Doesn't get loaded on time
            rayPivot = transform.Find("Model");

            uiLine = gameObject.AddComponent<LineRenderer>();
            uiLine.material = new Material(Shader.Find("Particles/Standard Unlit"));
            uiLine.startColor = uiLine.endColor = Color.white;
            uiLine.startWidth = uiLine.endWidth = 0.01f;

            return this;
        }

        public void Focus()
        {
            uiLine.startColor = uiLine.endColor = Color.white;
        }

        public void Defocus()
        {
            uiLine.material.color = new Color(1, 1, 1, 0.4f);
        }

        public void SetLine(float length)
        {
            uiLine.enabled = true;
            uiLine.SetPosition(0, rayPivot.position);
            uiLine.SetPosition(1, rayPivot.position + rayPivot.forward * length);
        }

        public void DisableLine()
        {
            uiLine.enabled = false;
        }
        */
    }
}
