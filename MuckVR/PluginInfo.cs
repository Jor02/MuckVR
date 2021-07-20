using MuckVR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: AssemblyVersion(PluginInfo.VERSION)]
[assembly: AssemblyTitle(PluginInfo.NAME + " (" + PluginInfo.GUID + ")")]
[assembly: AssemblyProduct(PluginInfo.NAME)]

/// <summary>
/// Based on https://github.com/BepInEx/BepInEx.PluginTemplate/blob/main/src/PluginInfo.cs
/// </summary>
namespace MuckVR
{
    internal static class PluginInfo
    {
        public const string GUID = "com.jor02.muckvr";
        public const string NAME = "MuckVR";
        public const string VERSION = "0.1";
    }
}
