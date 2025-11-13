using UnityEngine;
using UnityEngine.Tilemaps;
using Core.Player_files.Scripts;
using System.Collections;

[RequireComponent(typeof(Tilemap))]
public class TrapTilemapHandler : MonoBehaviour
{
    private Tilemap tilemap;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();

        // Hide all traps initially
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) is TimeTrapTile)
                tilemap.SetColor(pos, new Color(1, 1, 1, 0));
        }
    }

    void Update()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player == null) return;

        Vector3Int cellPos = tilemap.WorldToCell(player.transform.position);
        TileBase tile = tilemap.GetTile(cellPos);

        if (tile is TimeTrapTile trapTile)
        {
            Debug.Log($"Player stepped on trap at {cellPos}");

            // Make visible
            tilemap.SetColor(cellPos, trapTile.activeColor);
            tilemap.RefreshTile(cellPos);

            // Trigger player trap effect
            StartCoroutine(player.Trapped(trapTile.timeHeld));

            // Gotta make it delayed somehow
            tilemap.SetTile(cellPos, null);
        }
    }
}
