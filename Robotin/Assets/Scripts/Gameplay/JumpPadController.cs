using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public int a;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(1, 1).normalized;
        rb.AddForce(direction * rb.velocity.magnitude * 10, ForceMode2D.Impulse);
        
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        a = 10;
        Debug.Log(player.gameObject.name);

        if (Input.GetKey(KeyCode.Backspace))
        {
            Jump();
        }
    }
}
