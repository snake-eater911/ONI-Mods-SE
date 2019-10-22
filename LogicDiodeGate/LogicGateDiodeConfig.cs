using System;
using UnityEngine;
using TUNING;

namespace LogicGateDiode
{
    public class LogicGateDiodeConfig : LogicGateBaseConfig
    {
        protected override LogicGateBase.Op GetLogicOp()
        {
            return LogicGateBase.Op.CustomSingle;
        }
        public override BuildingDef CreateBuildingDef()
        {
            //return base.CreateBuildingDef("LogicDiodeGate", "logic_not_kanim", 2, 1);
            //float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
            string ID = "LogicDiodeGate";
            int width = 2;
            int height = 1;
            string anim = "logic_not_kanim";
            int hitpoints = 10;
            float construction_time = 3f;
            float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
            string[] refined_METALS = MATERIALS.REFINED_METALS;
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tier, refined_METALS, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
            buildingDef.ViewMode = OverlayModes.Logic.ID;
            buildingDef.ObjectLayer = ObjectLayer.LogicGates;
            buildingDef.SceneLayer = Grid.SceneLayer.LogicGates;
            buildingDef.ThermalConductivity = 0.05f;
            buildingDef.Floodable = false;
            buildingDef.Overheatable = false;
            buildingDef.Entombable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.DragBuild = true;
            LogicGateBase.uiSrcData = Assets.instance.logicModeUIData;
            GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, ID);
            return buildingDef;
        }
        protected override LogicGate.LogicGateDescriptions GetDescriptions()
		{
            return new LogicGate.LogicGateDescriptions
            {
                output = new LogicGate.LogicGateDescriptions.Description
                {
                    name = LogicGateDiodeConfig.name,
                    active = LogicGateDiodeConfig.activedesc,
                    inactive = LogicGateDiodeConfig.inactivedesc
                }
            };
		}
        public override void DoPostConfigureComplete(GameObject go)
        {
            LogicGateDiode logicGateDiode = go.AddComponent<LogicGateDiode>();
            logicGateDiode.op = this.GetLogicOp();
            go.GetComponent<KPrefabID>().prefabInitFn += this.LogicDiodeGateConfig_prefabInitFn;
        }
        protected virtual void LogicDiodeGateConfig_prefabInitFn(GameObject go)
        {
            go.GetComponent<LogicGateDiode>().SetPortDescriptions(this.GetDescriptions());
        }
        public const string name = "Logical Diode Gate";
        public const string activedesc = "Outputs Green signal";
        public const string inactivedesc = "Outputs Red signal";
    }
}
