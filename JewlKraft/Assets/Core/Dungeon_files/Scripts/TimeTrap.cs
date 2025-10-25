using System;
using Core.Player_files.Scripts;
using UnityEngine;

public class TimeTrap : MonoBehaviour
{
    [SerializeField] private float timeHeld;
    private Boolean used = false;
    private SpriteRenderer sr;
    private Color color;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
        color.a = 0f;
        sr.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && !used){
            color.a = 1f;
            sr.color = color;
            StartCoroutine(player.Trapped(timeHeld));
            used = true;
        }
    }
}
