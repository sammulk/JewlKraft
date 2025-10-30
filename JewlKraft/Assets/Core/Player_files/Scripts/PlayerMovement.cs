using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Core.Player_files.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        public Rigidbody2D rb;
        private Vector2 moveInput;
        private Animator animator;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
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
            }
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

    }
}
