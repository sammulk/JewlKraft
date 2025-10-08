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


    [CreateAssetMenu(fileName = "GemData", menuName = "Custom/Scriptable Objects/GemData")]
    public class GemData : ScriptableObject
    {
        public GemTypes gemType;
        public Sprite gemSprite;
        public bool[][] ShapeData;
    }
}