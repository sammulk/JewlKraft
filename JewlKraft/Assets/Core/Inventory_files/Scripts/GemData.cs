using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Inventory_files.Scripts
{
    public enum GemTypes
    {
        Gold,
        Ruby,
        Sapphire,
        Emerald,
        Topaz,
        Diamond,
        Onyx,
        Amethyst
    }

/**
 * Describes the fixed properties of an item.
 */
    [CreateAssetMenu(fileName = "GemData", menuName = "Custom/Scriptable Objects/GemData")]
    public class GemData : ScriptableObject
    {
        public GemTypes gemType;
        public Sprite gemSprite;
        public Color gemColor;

        public int height;
        public int width;
        //public bool[][] ShapeData;
    }
}