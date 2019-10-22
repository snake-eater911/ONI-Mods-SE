using System;
using System.Collections.Generic;
using Database;
using Harmony;
using STRINGS;
using UnityEngine;

namespace ChlorineAndHydrogenDiffusers
{
    internal class ChlorineDiffuserPatch
    {
        [HarmonyPatch(typeof(BuildingComplete), "OnSpawn")]
        public class TintPatch
        {
            public static void Postfix(BuildingComplete __instance)
            {
                if (__instance.name != null)
                {
                    if (__instance.name == "ChlorineDiffuserComplete" && __instance.GetComponent<KAnimControllerBase>() != null)
                    {
                        __instance.GetComponent<KAnimControllerBase>().TintColour = new Color(0.53f, 0.79f, 0.34f);
                    }
                }
                //KAnimControllerBase component = __instance.GetComponent<KAnimControllerBase>();
                //bool flag = component != null;
                //if (flag)
                //{
                //    bool flag2 = __instance.name == "ChlorineDiffuserComplete";
                //    if (flag2)
                //    {
                //        float num = 135f;
                //        float num2 = 200f;
                //        float num3 = 85f;
                //        component.TintColour = new Color(num / 255f, num2 / 255f, num3 / 255f);
                //    }
                //}
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
                    ChlorineDiffuserPatch.ImplementationPatch.NAME.key.String,
                    ChlorineDiffuserPatch.ImplementationPatch.NAME.text
                });
                Strings.Add(new string[]
                {
                    ChlorineDiffuserPatch.ImplementationPatch.DESC.key.String,
                    ChlorineDiffuserPatch.ImplementationPatch.DESC.text
                });
                Strings.Add(new string[]
                {
                    ChlorineDiffuserPatch.ImplementationPatch.EFFECT.key.String,
                    ChlorineDiffuserPatch.ImplementationPatch.EFFECT.text
                });
                ModUtil.AddBuildingToPlanScreen("Refining", "ChlorineDiffuser");
            }

            private static void Postfix()
            {
                object obj = Activator.CreateInstance(typeof(ChlorineDiffuserConfig));
                BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
            }

            public static LocString NAME = new LocString("Chlorine Diffuser", "STRINGS.BUILDINGS.PREFABS." + "ChlorineDiffuser".ToUpper() + ".NAME");
            public static LocString DESC = new LocString("This machine rapidly decomposes balm lily flowers and releases bonded chlorine.", "STRINGS.BUILDINGS.PREFABS." + "ChlorineDiffuser".ToUpper() + ".DESC");
            public static LocString EFFECT = new LocString(string.Concat(new string[]
            {
                "Decomposes balm lily flowers",
                //UI.FormatAsLink("balm lily flowers", "SwampLilyFlower"),
                " in order to release ",
                UI.FormatAsLink("chlorine", "CHLORINEGAS"),
                " stored in them, which is useful for disinfection, along with some residual ",
                UI.FormatAsLink("plant matter", "ALGAE"),
                ".\n"
            }), "STRINGS.BUILDINGS.PREFABS." + "ChlorineDiffuser".ToUpper() + ".EFFECT");
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class DatabaseAddingPatch
        {
            public static void Prefix()
            {
                List<string> list = new List<string>(Techs.TECH_GROUPING["Distillation"])
                {
                    "ChlorineDiffuser"
                };
                Techs.TECH_GROUPING["Distillation"] = list.ToArray();
            }
        }
    }
}
