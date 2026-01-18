using Core.Player_files.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TrapTilemapHandler : MonoBehaviour
{
    private Tilemap tilemap;

    [SerializeField] private AudioClip LeavesSounds;
    [SerializeField, Range(0.5f, 1.5f)] private float pitchMin = 0.95f;
    [SerializeField, Range(0.5f, 1.5f)] private float pitchMax = 1.05f;

    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup caveMixerGroup;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();

        // Hide all traps initially
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) is TimeTrapTile)
                tilemap.SetColor(pos, new Color(1, 1, 1, 0));
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = caveMixerGroup; audioSource.enabled = false;
    }

    void Update()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player == null) return;

        Vector3Int cellPos = tilemap.WorldToCell(player.transform.position);
        TileBase tile = tilemap.GetTile(cellPos);

        if (tile is TimeTrapTile trapTile)
        {

            // Make visible
            tilemap.SetColor(cellPos, trapTile.activeColor);
            tilemap.RefreshTile(cellPos);

            // Trigger player trap effect
            StartCoroutine(player.Trapped(trapTile.timeHeld));

            tilemap.SetTile(cellPos, trapTile.revealedTile);

            // Play sound
            audioSource.enabled = true;
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            audioSource.clip = LeavesSounds;
            audioSource.Play();

        }
    }
}
