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
    [SerializeField] private bool canJumpWall = true;
    private float wallJumpTimer = 0;


    enum playerState
    {
        idle,
        moving,
        preparingToJump,
        jumping,
        preparingToWallJump,
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

        if (Input.GetKey(KeyCode.Space) && state != playerState.falling && state != playerState.preparingToWallJump)
        {
            state = playerState.preparingToJump;
            if (jumpForce < maxJumpForce)
            {
                jumpForce += 0.5f;
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
            case playerState.preparingToWallJump:
                
                wallJumpTimer += Time.deltaTime;
                if (wallJumpTimer > 2)
                {
                    state = playerState.falling;
                    wallJumpTimer = 0;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    if (jumpForce < maxJumpForce)
                    {
                        jumpForce += 0.5f;
                    }
                    
                }
                else if (Input.GetKeyUp(KeyCode.Space) && state == playerState.preparingToWallJump)
                {
                    state = playerState.jumping;
                    wallJumpTimer = 0;
                }
                rb.velocity = Vector2.zero;
                break;
            case playerState.falling:
                break;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.Translate(-0.3f * direction, 0, 0);

            if(state == playerState.falling && canJumpWall)
            {
                state = playerState.preparingToWallJump;
                
                
            }
        }
        
        if (IsTouchingGround() && state == playerState.falling)
        {
            state = playerState.moving;
            rb.velocity = Vector2.zero;
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

    private bool IsTouchingGround()
    {
        if(hit.collider == null)
        {
            return false;
        }
        else if (hit.collider.CompareTag("Terrain"))
        {
            return true;
        }

        return false;
    }

    IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        state = playerState.falling;
    }

    IEnumerator WallJumpCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if(state == playerState.preparingToWallJump)
        {
            state = playerState.falling;
        }
        
    }
}