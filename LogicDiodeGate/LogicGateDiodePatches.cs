using System;
using System.Collections.Generic;
using Database;
using Harmony;
using UnityEngine;
using STRINGS;

namespace LogicGateDiode
{
    internal class LogicGateDiodeBuildingPatches
    {
        [HarmonyPatch(typeof(BuildingComplete), "OnSpawn")]
        public class TintPatch
        {
            public static void Postfix(BuildingComplete __instance)
            {
                if (__instance.name != null)
                {
                    if (__instance.name == "LogicGateDiodeComplete" && __instance.GetComponent<KAnimControllerBase>() != null)
                    {
                        __instance.GetComponent<KAnimControllerBase>().TintColour = new Color(0.8f, 0.1f, 0.7f);
                    }
                }
            }
        }
        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch("LoadGeneratedBuildings")]
        public class LogicGateDiodeBuildingPatch
        {
            public static void Prefix()
            {
                string str = "STRINGS.BUILDINGS.PREFABS.";
                LogicGateDiodeBuildingPatch.SetString(str + LogicGateDiodeBuildingPatch.ID_UPPER, LogicGateDiodeBuildingPatch.NAME, LogicGateDiodeBuildingPatch.DESC, LogicGateDiodeBuildingPatch.EFFC);
                ModUtil.AddBuildingToPlanScreen("Automation", "LogicDiodeGate");
            }

            private static void SetString(string fullid, string name, string desc, string effect)
            {
                Strings.Add(new string[]
                {
                fullid + ".NAME",
                name
                });
                Strings.Add(new string[]
                {
                fullid + ".DESC",
                desc
                });
                Strings.Add(new string[]
                {
                fullid + ".EFFECT",
                effect
                });
            }
            private static readonly string ID_UPPER = "LogicDiodeGate".ToUpper();
            private static readonly LocString NAME = UI.FormatAsLink("DIODE gate", "LogicDiodeGate") ?? "";
            private static readonly LocString DESC = "Passes through the signal received on its input. Can be used to filter automation signals without manually setting a delay.";
            private static readonly string EFFC = "Passes through an automation signal received on its input.";
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class LogicGateDiodeTechPatch
        {
            public static void Prefix()
            {
                List<string> list = new List<string>(Techs.TECH_GROUPING["LogicCircuits"])
                {
                    "LogicDiodeGate"
                };
                Techs.TECH_GROUPING["LogicCircuits"] = list.ToArray();
            }
        }
    }
}
