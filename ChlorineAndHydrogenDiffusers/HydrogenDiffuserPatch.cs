using System;
using System.Collections.Generic;
using Database;
using Harmony;
using STRINGS;
using UnityEngine;

namespace ChlorineAndHydrogenDiffusers
{
    internal class HydrogenDiffuserPatch
    {
        [HarmonyPatch(typeof(BuildingComplete), "OnSpawn")]
        public class TintPatch
        {
            public static void Postfix(BuildingComplete __instance)
            {
                if (__instance.name != null)
                {
                    if (__instance.name == "HydrogenDiffuserComplete" && __instance.GetComponent<KAnimControllerBase>() != null)
                    {
                        __instance.GetComponent<KAnimControllerBase>().TintColour = new Color(0.9f, 0.5f, 0.6f);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch("LoadGeneratedBuildings")]
        public class ImplementationPatch
        {
            private static void Prefix()
            {
                Strings.Add(new string[]
                {
                    HydrogenDiffuserPatch.ImplementationPatch.NAME.key.String,
                    HydrogenDiffuserPatch.ImplementationPatch.NAME.text
                });
                Strings.Add(new string[]
                {
                    HydrogenDiffuserPatch.ImplementationPatch.DESC.key.String,
                    HydrogenDiffuserPatch.ImplementationPatch.DESC.text
                });
                Strings.Add(new string[]
                {
                    HydrogenDiffuserPatch.ImplementationPatch.EFFECT.key.String,
                    HydrogenDiffuserPatch.ImplementationPatch.EFFECT.text
                });
                ModUtil.AddBuildingToPlanScreen("Refining", "HydrogenDiffuser");
            }

            private static void Postfix()
            {
                object obj = Activator.CreateInstance(typeof(HydrogenDiffuserConfig));
                BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
            }

            public static LocString NAME = new LocString("Hydrogen Diffuser", "STRINGS.BUILDINGS.PREFABS." + "HydrogenDiffuser".ToUpper() + ".NAME");
            public static LocString DESC = new LocString("This machine rapidly decomposes pincha peppernuts and releases bonded hydrogen.", "STRINGS.BUILDINGS.PREFABS." + "HydrogenDiffuser".ToUpper() + ".DESC");
            public static LocString EFFECT = new LocString(string.Concat(new string[]
            {
                "Decomposes pincha peppernuts and releases ",
                //UI.FormatAsLink("balm lily flowers", "SwampLilyFlower"),
                UI.FormatAsLink("hydrogen", "HYDROGEN"),
                " , along with some residual ",
                UI.FormatAsLink("plant matter", "ALGAE"),
                " and ",
                UI.FormatAsLink("phosphorite", "PHOSPHORITE"),
                ".\n"
            }), "STRINGS.BUILDINGS.PREFABS." + "HydrogenDiffuser".ToUpper() + ".EFFECT");
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class DatabaseAddingPatch
        {
            public static void Prefix()
            {
                List<string> list = new List<string>(Techs.TECH_GROUPING["Distillation"])
                {
                    "HydrogenDiffuser"
                };
                Techs.TECH_GROUPING["Distillation"] = list.ToArray();
            }
        }
    }
}