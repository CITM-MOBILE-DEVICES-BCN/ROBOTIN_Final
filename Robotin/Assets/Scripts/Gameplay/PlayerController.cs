using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int direction = 1;
    [SerializeField] private float rayRange = 5f;
    [SerializeField] private float rayOffsetX = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionRay = Vector3.down;
        Vector3 rayOrigin = new Vector3(transform.position.x + (rayOffsetX * direction), transform.position.y, transform.position.z);
        Ray2D playerRay = new Ray2D(rayOrigin, transform.TransformDirection(directionRay * rayRange));
        Debug.DrawRay(rayOrigin, transform.TransformDirection(directionRay * rayRange), Color.red);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, directionRay, rayRange);

        if (hit)
        {
            Move();
        }
        else if (!hit)
        {
            direction *= -1;
        }
        
        

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.Translate(-0.5f * direction, 0, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
        }
    }

    private void Move()
    {
        transform.Translate(0.05f * direction, 0, 0);
    }
}
