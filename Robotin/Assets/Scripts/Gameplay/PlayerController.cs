using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int direction = 1;
    private Rigidbody2D rb;
    [SerializeField] private float rayRange = 5f;
    [SerializeField] private float rayOffsetX = 0.75f;
    [SerializeField] private float jumpForce = 0;
    [SerializeField] private float maxJumpForce = 100f;
    private RaycastHit2D hit;


    enum playerState
    {
        idle,
        moving,
        preparingToJump,
        jumping,
        falling
    }
    playerState state = playerState.moving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionRay = Vector3.down;
        Vector3 rayOrigin = new Vector3(transform.position.x + (rayOffsetX * direction), transform.position.y, transform.position.z);
        Ray2D playerRay = new Ray2D(rayOrigin, transform.TransformDirection(directionRay * rayRange));
        Debug.DrawRay(rayOrigin, transform.TransformDirection(directionRay * rayRange), Color.red);

        hit = Physics2D.Raycast(rayOrigin, directionRay, rayRange);

        if (Input.GetKey(KeyCode.Space) && state == playerState.moving)
        {
            state = playerState.preparingToJump;
            if (jumpForce < maxJumpForce)
            {
                jumpForce += 10f;
            }
                

        }
        else if (Input.GetKeyUp(KeyCode.Space) && state == playerState.preparingToJump)
        {
            state = playerState.jumping;
        }


        switch (state)
        {
            case playerState.idle:
                break;
            case playerState.moving:
                Move();
                break;
            case playerState.preparingToJump:
                break;
            case playerState.jumping:
                Jump();
                StartCoroutine(JumpCoroutine());
                break;
            case playerState.falling:
                IsTouchingGround();
                break;
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
        if (hit)
        {
            transform.Translate(0.05f * direction, 0, 0);
        }
        else if (!hit)
        {
            direction *= -1;
        }
    }

    private void Jump()
    {
        Vector2 jumpDirection = new Vector2(direction, 1);
        rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
        jumpForce = 0;
    }

    private void IsTouchingGround()
    {
        if(hit)
        {
            state = playerState.moving;
        }
        
    }

    IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        state = playerState.falling;
    }
}
