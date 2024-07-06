using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Services
{
    sealed class InputUtils
    {
        public Vector3Int GetMouseOnGridPos(Tilemap tilemap)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int mouseCellPos = tilemap.WorldToCell(mousePos);
            mouseCellPos.z = 0;
 
            return mouseCellPos;
        }
    }
}