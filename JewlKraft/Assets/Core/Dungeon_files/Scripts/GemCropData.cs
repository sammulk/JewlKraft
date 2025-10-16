using Core.Inventory_files.Scripts;
using UnityEngine;
[CreateAssetMenu(fileName = "GemData", menuName = "Custom/Scriptable Objects/GemCrop")]

public class GemCropData : ScriptableObject
{
    public Sprite GemCropSprite;
    public GemData GemType;
    public int HitsToMine;
    public int ShardCount;
    public GameObject DustPrefab;
    public GameObject GemPrefab;

}
