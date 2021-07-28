using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Valve.VR;

namespace MuckVR.VR.Gameplay
{
    public class VRInput : MonoBehaviour
    {
        public SteamVR_Action_Vector2 joystick = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("default", "Move");
        public SteamVR_Action_Boolean jump = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "Jump");

        public static VRInput instance;

        const SteamVR_Input_Sources right = SteamVR_Input_Sources.RightHand;
        const SteamVR_Input_Sources left = SteamVR_Input_Sources.LeftHand;

        public VRInput()
        {
            instance = this;
        }

        public Vector2 GetJoyStick
        {
            get
            {
                return joystick[right].axis;
            }
        }

        public bool GetJump
        {
            get
            {
                return jump[right].state;
            }
        }
    }
}
