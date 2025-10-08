using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Scripts
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Custom/Scriptable Objects/PlayerInventory")]
    public class PlayerInventory : ScriptableObject
    {
        public List<GemData> contents;

        private int _sizeX = 5;
        private int _sizeY = 5;
        
        
    }
}
