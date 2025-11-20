using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Time Trap Tile")]
public class TimeTrapTile : TileBase
{
    public float timeHeld = 2f;
    public Sprite trapSprite;
    public Color activeColor = Color.white;
    public TileBase revealedTile;


    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = trapSprite;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }
}
