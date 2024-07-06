using Components;
using Components.Commands;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;

namespace Systems.EcsInput
{
    sealed class ShopButtonsInput : EcsUguiCallbackSystem {
        private readonly EcsPoolInject<TowerPreview> _towerPreviewPool = default;
        private readonly EcsPoolInject<SpawnCommand> _spawnCommandPool = default;

        [Preserve]
        [EcsUguiClickEvent (Idents.Ui.TowerArcherBtn, Idents.Worlds.Events)]
        void OnClickTowerArcherBtn (in EcsUguiClickEvent e) {
            CreateTowerPreviewEntity(Idents.Ui.TowerArcherBtn);
        }
        
        [Preserve]
        [EcsUguiClickEvent (Idents.Ui.TowerGunBtn, Idents.Worlds.Events)]
        void OnClickTowerGunBtn (in EcsUguiClickEvent e) {
            CreateTowerPreviewEntity(Idents.Ui.TowerGunBtn);
        }

        [Preserve]
        [EcsUguiClickEvent (Idents.Ui.TowerWindBtn, Idents.Worlds.Events)]
        void OnClickTowerWindBtn (in EcsUguiClickEvent e)
        {
            CreateTowerPreviewEntity(Idents.Ui.TowerWindBtn);
        }

        private void CreateTowerPreviewEntity(string type)
        {
            var entity = _towerPreviewPool.Value.GetWorld ().NewEntity ();
        
            ref var towerPreview = ref _towerPreviewPool.Value.Add (entity);
            _spawnCommandPool.Value.Add(entity);
                
            towerPreview.Type = type;
            towerPreview.Size = 1;
            towerPreview.Transform = null;
        }
    
    }
}