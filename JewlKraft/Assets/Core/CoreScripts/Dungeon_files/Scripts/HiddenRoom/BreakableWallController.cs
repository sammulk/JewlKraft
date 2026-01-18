using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BreakableWallController : MonoBehaviour
{
    private Tilemap map;

    [SerializeField] private GameObject hiddenRoomGameObject;

    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField, Range(0.5f, 1.5f)] private float pitchMin = 0.95f;
    [SerializeField, Range(0.5f, 1.5f)] private float pitchMax = 1.05f;

    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup caveMixerGroup;

    void Awake()
    {
        map = GetComponent<Tilemap>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = caveMixerGroup;
    }
    public void HitTile(Vector2 worldPos)
    {
        Vector3Int cell = map.WorldToCell((Vector3)worldPos);
        TileBase tile = map.GetTile(cell);
        if (tile == null) return;

        // Play hit sound
        if (hitSounds.Length > 0)
        {
            AudioClip clip = hitSounds[Random.Range(0, hitSounds.Length)];
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            audioSource.PlayOneShot(clip);
        }

        BreakableWallTile breakable = tile as BreakableWallTile;
        if (breakable == null) return;

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
