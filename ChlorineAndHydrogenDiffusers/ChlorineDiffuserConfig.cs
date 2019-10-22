using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUNING;
using UnityEngine;

namespace ChlorineAndHydrogenDiffusers
{
    public class ChlorineDiffuserConfig : IBuildingConfig
    {
        public override BuildingDef CreateBuildingDef()
        {
            string id = "ChlorineDiffuser";
            int width = 1;
            int height = 2;
            string anim = "mineraldeoxidizer_kanim";
            int hitpoints = 50;
            float construction_time = 50f;
            float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
            string[] all_METALS = MATERIALS.ALL_METALS;
            float melting_point = 800f;
            BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
            EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tier, all_METALS, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 180f;
            buildingDef.ExhaustKilowattsWhenActive = 0.75f;
            buildingDef.SelfHeatKilowattsWhenActive = 1.25f;
            buildingDef.ViewMode = OverlayModes.Oxygen.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.Breakable = true;
            return buildingDef;
        }
        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            Electrolyzer electrolyzer = go.AddOrGet<Electrolyzer>();
            electrolyzer.maxMass = 3.0f;
            electrolyzer.hasMeter = true;
            Storage storage = go.AddOrGet<Storage>();
            storage.capacityKg = 80f;
            storage.showInUI = true;
            ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
            {
                new ElementConverter.ConsumedElement(new Tag("SwampLilyFlower"), 1.0f)
            };
            elementConverter.outputElements = new ElementConverter.OutputElement[]
            {
                new ElementConverter.OutputElement(0.25f, SimHashes.ChlorineGas, 348.15f, false, false, 0f, 1f, 1f, byte.MaxValue, 0),
                new ElementConverter.OutputElement(0.66f, SimHashes.Algae, 323.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0)
            };
            ElementDropper elementDropper = go.AddComponent<ElementDropper>();
            elementDropper.emitMass = 10f;
            elementDropper.emitTag = SimHashes.Algae.CreateTag();
            elementDropper.emitOffset = new Vector3(0.5f, 0.5f, 0f);
            ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
            manualDeliveryKG.SetStorage(storage);
            manualDeliveryKG.requestedItemTag = new Tag("SwampLilyFlower");
            manualDeliveryKG.capacity = 80f;
            manualDeliveryKG.refillMass = 32f;
            manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
        }
        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
        }

        public const string ID = "ChlorineDiffuser";

        private const float SWAMPLILYFLOWER_BURN_RATE = 1.0f;

        private const float SWAMPLILYFLOWER_STORAGE = 50f;

        private const float CHLORINEGAS_GENERATION_RATE = 0.25f;

        private const float CHLORINEGAS_TEMPERATURE = 348.15f;

        private const float ALGAE_GENERATION_RATE = 0.66f;

        private const float ALGAE_TEMPERATURE = 323.15f;
    }
}