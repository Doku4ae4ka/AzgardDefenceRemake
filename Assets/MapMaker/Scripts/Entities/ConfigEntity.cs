using MapMaker.Scripts.EntitySettings.Configs;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/ConfigEntity"), SelectionBase]
    public class ConfigEntity : MonoBehaviour, IEntityObject
    {
        [SerializeField] public ConfigSettings configs;
        public void Save(string entityID, Slot slot)
        {
            var entity = new SlotEntity(entityID, SlotCategory.Config, SavePath.EntityType.LevelConfig);
            slot.AddConfig(entity);
            var lastEntityID = FindAnyObjectByType<MapEditor>().Increment;
            
            entity.SetField(SavePath.Config.FreeEntityID, $"{lastEntityID}");
            this.SerializeObject(entity);
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype)
        {
            configs = new ();
            this.DeserializeObject(slotEntity);
        }
        
        void OnDrawGizmos()
        {
            var mapBounds = configs.mapBorders.mapBorders;
            
            bool isXBoundsCorrect = mapBounds.x < mapBounds.z; // minX < maxX
            bool isYBoundsCorrect = mapBounds.y < mapBounds.w; // minY < maxY

            // Левый нижний угол (x, y)
            Vector3 bottomLeft = new Vector3(mapBounds.x, mapBounds.y, 0);

            // Правый нижний угол (z, y)
            Vector3 bottomRight = new Vector3(mapBounds.z, mapBounds.y, 0);

            // Правый верхний угол (z, w)
            Vector3 topRight = new Vector3(mapBounds.z, mapBounds.w, 0);

            // Левый верхний угол (x, w)
            Vector3 topLeft = new Vector3(mapBounds.x, mapBounds.w, 0);

            // Отрисовка горизонтальных линий: нижняя и верхняя
            Gizmos.color = isYBoundsCorrect ? Color.magenta : Color.red;
            Gizmos.DrawLine(bottomLeft, bottomRight);  // Нижняя сторона
            Gizmos.DrawLine(topLeft, topRight);        // Верхняя сторона

            // Отрисовка вертикальных линий: левая и правая
            Gizmos.color = isXBoundsCorrect ? Color.magenta : Color.red;
            Gizmos.DrawLine(bottomLeft, topLeft);      // Левая сторона
            Gizmos.DrawLine(bottomRight, topRight);    // Правая сторона
        }
    }
}