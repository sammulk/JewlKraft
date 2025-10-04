using UnityEngine;

namespace Core.Scripts
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


    [CreateAssetMenu(fileName = "GemData", menuName = "Custom/GemData")]
    public class GemData : ScriptableObject
    {
        public GemTypes gemType;
        public Sprite gemSprite;
        //public ShapeData shapeData;
    }
}