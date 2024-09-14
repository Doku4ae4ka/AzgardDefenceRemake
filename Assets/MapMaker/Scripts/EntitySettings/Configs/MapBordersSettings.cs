 using System;
 using Sirenix.OdinInspector;
 using Source.Scripts.Extensions;
 using Source.Scripts.SaveSystem;
 using UnityEngine;

 namespace MapMaker.Scripts.EntitySettings.Configs
 {
     [Serializable, Toggle("enabled")]
     public class MapBordersSettings
     {
         public bool enabled;
         public Vector4 mapBorders;

         public void TryLoad(Entity entity)
         {
             if (entity.TryGetField(SavePath.Config.MapBorders, out var field))
             {
                 enabled = true;
                 mapBorders = field.ParseVector4();
             }
             else enabled = false;
         }
         
         public void TrySave(Entity entity)
         {
             if (!enabled) return;
             entity.SetField(SavePath.Config.MapBorders, $"{mapBorders}");
         }
     }
}