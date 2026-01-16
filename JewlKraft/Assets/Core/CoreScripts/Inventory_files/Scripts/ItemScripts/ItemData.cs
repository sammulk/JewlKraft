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
        public MaterialType MaterialType;
        
        //RAW
        public CraftStage CraftStage;
        //CRAFTED
        public ItemType ItemType;
        
        public Sprite sprite;
        public Color color;

        public int goldValue;

        public int height = 1;
        public int width = 1;
        //public bool[][] ShapeData;
    }
}