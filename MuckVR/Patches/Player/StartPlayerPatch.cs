using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace MuckVR.Patches
{
    [HarmonyPatch(typeof(StartPlayer), "Start")]
    class StartPlayerPatch
    {
        //Scale for player
        const float playerSize = 2.1124f;

        static void Prefix(StartPlayer __instance)
        {
            Transform cameraPivot = __instance.transform.Find("Camera");
            Vector3 offset = cameraPivot.GetComponent<MoveCamera>().offset;

            Transform gameRig = UnityEngine.Object.Instantiate(Base.vrAssets.LoadAsset<GameObject>("GameRig"), cameraPivot).transform;
            Transform camera = cameraPivot.Find("Shake/Main Camera");
            camera.parent = gameRig;

            gameRig.gameObject.AddComponent<VR.Gameplay.VRPlayer>();

            CapsuleCollider collider = __instance.transform.Find("Player").GetComponent<CapsuleCollider>();

            cameraPivot.position = __instance.transform.position + offset;
            gameRig.position = cameraPivot.position - offset + Vector3.down * (collider.height * collider.transform.lossyScale.y / 2);

            gameRig.localScale = Vector3.one * playerSize;
        }
    }
}
