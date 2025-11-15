using UnityEngine;
using static Core.Inventory_files.Scripts.ItemScripts.Helpers;

namespace Core.Inventory_files.Scripts.ItemScripts
{
/**
 * Describes the fixed properties of an item.
 */
    [CreateAssetMenu(fileName = "ItemData", menuName = "Custom/Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string guid;  // Unique ID
        public MaterialType MaterialType;
        public CraftStage CraftStage;
        
        public Sprite sprite;
        public Color color;

        public int height = 1;
        public int width = 1;
        //public bool[][] ShapeData;
    }
}