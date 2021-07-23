using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Logging;

namespace MuckVR.Patches.Player
{
    /// <summary>
    /// Patches PlayerInput's Update to set camera to the VR Cam
    /// </summary>
    [HarmonyPatch(typeof(PlayerInput), "Update")]
    class PlayerInputUpdatePatch
    {
        static void Prefix(ref PlayerMovement ___playerMovement, ref Transform ___playerCam)
        {
            if (___playerCam != ___playerMovement.playerCam)
            {
                ___playerCam = ___playerMovement.playerCam;
            }
        }
    }

    /// <summary>
    /// Patches PlayerInput's Look to use VR Camera
    /// </summary>
    [HarmonyPatch(typeof(PlayerInput), "Look")]
    class PlayerInputLookPatch
    {
        static bool Prefix(ref Transform ___playerCam, ref Transform ___orientation)
        {
            ___orientation.transform.rotation = Quaternion.Euler(0f, ___playerCam.eulerAngles.y, 0f);
            return false;
        }
    }
}