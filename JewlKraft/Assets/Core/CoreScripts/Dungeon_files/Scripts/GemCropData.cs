using Core.Inventory_files.Scripts;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemData", menuName = "Custom/Scriptable Objects/GemCrop")]

public class GemCropData : ScriptableObject
{
    public Sprite GemCropSprite;
    [FormerlySerializedAs("GemType")] public ItemData itemType;
    public int HitsToMine;
    public int ShardCount;
    public GameObject DustPrefab;
    

}
