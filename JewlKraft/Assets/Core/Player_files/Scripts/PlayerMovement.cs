using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;

    public Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        //transform.position += new Vector3(horizontal, vertical, 0) * MoveSpeed * Time.deltaTime;
        moveInput = new Vector2(horizontal, vertical).normalized; //valdib kiiruse suurenemist diagonaalis
    }

    void FixedUpdate()
    {
        // Liigutame Rigidbody kaudu -> collisionid
        Vector2 targetPos = rb.position + moveInput * MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Portal")) return;
        SceneManager.LoadScene("Main_menu");
    }
    public IEnumerator Trapped(float TimeHeld)
    {
        float MoveSpeed_alt = MoveSpeed;
        MoveSpeed = 0;
        yield return new WaitForSeconds(TimeHeld);
        MoveSpeed = MoveSpeed_alt;
    }

}
