using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using MuckVR.VR.Gameplay;
using MuckVR.Utils;

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
            
			//Skip original code
			return false;
        }
    }

    /// <summary>
    /// Patches PlayerInput's MyInput to use controller inputs
    /// </summary>
	[HarmonyPatch(typeof(PlayerInput), "MyInput")]
    class PlayerInputMyInputPatch
    {
		static bool Prefix(PlayerInput __instance, ref PlayerMovement ___playerMovement, ref float ___mouseScroll)
        {
			//Skip movement
			if (OtherInput.Instance.OtherUiActive() && !Map.Instance.active)
			{
				var StopInput = typeof(PlayerInput).GetMethod("StopInput", BindingFlags.NonPublic | BindingFlags.Instance);
				StopInput.Invoke(__instance, null);
				return false;
			}

			if (!___playerMovement)
				return false;

			if (!VRInput.instance)
				return false;

			//Reset movement

			__instance.SetPrivatePropertyValue<float>("x", 0);
			__instance.SetPrivatePropertyValue<float>("y", 0);

			//WASD TODO: Need to be joystick
			/*
			if (Input.GetKey(InputManager.forward))
			{
				float num = ___y;
				___y = num + 1f;
			}
			else if (Input.GetKey(InputManager.backwards))
			{
				float num = ___y;
				___y = num - 1f;
			}
			if (Input.GetKey(InputManager.left))
			{
				float num = ___x;
				___x = num - 1f;
			}
			if (Input.GetKey(InputManager.right))
			{
				float num = ___x;
				___x = num + 1f;
			} */

			//Joystick movement

			Vector2 Joy = VRInput.instance.GetJoyStick;
			__instance.SetPrivatePropertyValue("x", Joy.x);
			__instance.SetPrivatePropertyValue("y", Joy.y);

			//Set properties/field
			__instance.SetPrivatePropertyValue("jumping", VRInput.instance.GetJump);
			__instance.SetPrivatePropertyValue("sprinting", Input.GetKey(InputManager.sprint));
			___mouseScroll = Input.mouseScrollDelta.y;

			//Jump
			if (Input.GetKeyDown(InputManager.jump))
			{
				___playerMovement.Jump();
			}

			//Attack / LMB Down
			if (Input.GetKey(InputManager.leftClick))
			{
				UseInventory.Instance.Use();
			}

			//Stop eat / LMB Up
			if (Input.GetKeyUp(InputManager.leftClick))
			{
				UseInventory.Instance.UseButtonUp();
			}

			//Place Item
			if (Input.GetKeyDown(InputManager.rightClick))
			{
				BuildManager.Instance.RequestBuildItem();
			}

			//Rotate build
			if (___mouseScroll != 0f)
			{
				BuildManager.Instance.RotateBuild((int)Mathf.Sign(___mouseScroll));
			}

			//Rotate build
			if (Input.GetKeyDown(KeyCode.R))
			{
				BuildManager.Instance.RotateBuild(1);
			}

			//Toggle hud
			if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.U) && Input.GetKeyDown(KeyCode.I))
			{
				UiController.Instance.ToggleHud();
			}

			float x = __instance.GetPrivatePropertyValue<float>("x");
			float y = __instance.GetPrivatePropertyValue<float>("y");


			___playerMovement.SetInput(
                new Vector2(x, y),
                __instance.GetPrivatePropertyValue<bool>("crouching"),
                __instance.GetPrivatePropertyValue<bool>("jumping"),
                __instance.GetPrivatePropertyValue<bool>("sprinting")
			);
			
			//Skip original code
			return false;
		}
    }
}