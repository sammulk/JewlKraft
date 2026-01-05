using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Core.Player_files.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        public Rigidbody2D rb;
        private Vector2 moveInput;
        private Animator animator;

        [Header("Walking SFX")]
        [SerializeField] private AudioClip[] walkClips;
        [SerializeField] private float walkVolume = 0.5f;
        [SerializeField] private float walkPitchMin = 0.95f;
        [SerializeField] private float walkPitchMax = 1.05f;
        [SerializeField] private float stepInterval = 0.8f;
        private float stepTimer;

        private AudioSource walkAudioSource;
        [SerializeField] private AudioMixerGroup caveMixerGroup;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            walkAudioSource = gameObject.AddComponent<AudioSource>();
            walkAudioSource.loop = false;
            walkAudioSource.playOnAwake = false;
            walkAudioSource.outputAudioMixerGroup = caveMixerGroup;
            walkAudioSource.volume = walkVolume;
        }
        void Update()
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            moveInput = new Vector2(horizontal, vertical).normalized;

            // Movement flag
            bool isWalking = moveInput != Vector2.zero;
            animator.SetBool("IsWalking", isWalking);

            if (isWalking)
            {
                // Update direction floats while moving
                animator.SetFloat("InputX", moveInput.x);
                animator.SetFloat("InputY", moveInput.y);

                // Remember last direction for idle state
                animator.SetFloat("LastInputX", moveInput.x);
                animator.SetFloat("LastInputY", moveInput.y);

                HandleWalkingSFX();
            }
            else stepTimer = 0;
        }

        void FixedUpdate()
        {
            // Liigutame Rigidbody kaudu -> collisionid
            Vector2 targetPos = rb.position + moveInput * (moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(targetPos);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Portal")) return;
            SceneManager.LoadScene("Shop_scene");
        }
        public IEnumerator Trapped(float TimeHeld)
        {
            float MoveSpeed_alt = moveSpeed;
            moveSpeed = 0;
            yield return new WaitForSeconds(TimeHeld);
            moveSpeed = MoveSpeed_alt;
        }

        private void HandleWalkingSFX()
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayStepSound();
                stepTimer = stepInterval; // Reset timer for next step
            }
        }

        private void PlayStepSound()
        {
            if (walkClips.Length == 0) return;

            AudioClip clip = walkClips[Random.Range(0, walkClips.Length)];
            walkAudioSource.pitch = Random.Range(walkPitchMin, walkPitchMax);
            walkAudioSource.PlayOneShot(clip, walkVolume);
        }
    }
}
