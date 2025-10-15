using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontal, vertical, 0) * MoveSpeed * Time.deltaTime;
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
