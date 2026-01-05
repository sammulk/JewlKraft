using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BreakableWallController : MonoBehaviour
{
    private Tilemap map;

    [SerializeField] private GameObject hiddenRoomGameObject;

    void Awake()
    {
        map = GetComponent<Tilemap>();
    }
    public void HitTile(Vector2 worldPos)
    {
        Vector3Int cell = map.WorldToCell((Vector3)worldPos);
        TileBase tile = map.GetTile(cell);
        if (tile == null) return;

        BreakableWallTile breakable = tile as BreakableWallTile;
        if (breakable == null) return;

        // SFX
        if (breakable.breakSound != null)
            AudioSource.PlayClipAtPoint(breakable.breakSound, map.GetCellCenterWorld(cell));

        // VFX
        if (breakable.breakEffect != null)
            Instantiate(breakable.breakEffect, map.GetCellCenterWorld(cell), Quaternion.identity);

        // Replace tile (revealedTile may be null -> clears tile)
        map.SetTile(cell, breakable.revealedTile);

        // Force refresh
        map.RefreshTile(cell);

        // Reveal hidden room
        if (hiddenRoomGameObject != null && !hiddenRoomGameObject.activeSelf)
        {
            hiddenRoomGameObject.SetActive(true);
        }
    }
}
