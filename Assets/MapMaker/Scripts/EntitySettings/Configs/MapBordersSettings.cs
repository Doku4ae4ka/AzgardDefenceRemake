 using System;
 using Sirenix.OdinInspector;
 using Source.Scripts.ECS.Groups.SlotSaver.Core;
 using Source.Scripts.Extensions;
 using UnityEngine;

 namespace MapMaker.Scripts.EntitySettings.Configs
 {
     [Serializable, Toggle("enabled")]
     public class MapBordersSettings
     {
         public bool enabled;
         public Vector4 mapBorders;

         public void TryLoad(SlotEntity slotEntity)
         {
             if (slotEntity.TryGetField(SavePath.Config.MapBounds, out var field))
             {
                 enabled = true;
                 mapBorders = field.ParseVector4();
             }
             else enabled = false;
         }
         
         public void TrySave(SlotEntity slotEntity)
         {
             if (!enabled) return;
             slotEntity.SetField(SavePath.Config.MapBounds, $"{mapBorders}");
         }
     }
}