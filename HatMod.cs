﻿
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace Curaci2
{
    [BepInPlugin("kiraa.curaci", "AmongCuraci", "1.0")]
    public class RemovePlayerLimitPlugin : BasePlugin
    {

        static internal BepInEx.Logging.ManualLogSource Logger;
        static Harmony _harmony;

        public override void Load()
        {
            Logger = Log;
            KMOGFLPJLLK.EICIGKMJIMF = KMOGFLPJLLK.MGGHFLMODBE = Enumerable.Repeat<int>(255, 255).ToArray<int>();
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }
    }
}