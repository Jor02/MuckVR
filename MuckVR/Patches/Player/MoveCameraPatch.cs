using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MuckVR.Patches.Player
{
    [HarmonyPatch(typeof(MoveCamera), "PlayerCamera")]
    class MoveCameraPatch
    {
        static bool Prefix(MoveCamera __instance)
        {
            __instance.transform.position = __instance.player.transform.position + __instance.offset;
            return false;
        }
    }
}