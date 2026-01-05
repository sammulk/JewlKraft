using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/BreakableWallTile")]
public class BreakableWallTile : Tile
{
    public AudioClip breakSound;
    public GameObject breakEffect;       // optional VFX prefab
    public TileBase revealedTile;        // tile to replace with (or leave null to clear)
}
