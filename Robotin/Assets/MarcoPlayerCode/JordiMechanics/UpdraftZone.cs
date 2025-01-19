using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdraftZone : MonoBehaviour
{
    public float additionalJumpForce = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RobotinJump playerJump = other.GetComponent<RobotinJump>();
            RobotinCollision playerCollision = other.GetComponent<RobotinCollision>();
            if (playerJump != null)
            {
                playerJump.ApplyAdditionalJumpForce(additionalJumpForce);
      
            }
            if (playerCollision != null)
            {
                playerCollision.downGravity /= 2.0f;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RobotinJump playerJump = other.GetComponent<RobotinJump>();
            RobotinCollision playerCollision = other.GetComponent<RobotinCollision>();
            if (playerJump != null)
            {
                playerJump.RemoveAdditionalJumpForce(additionalJumpForce);
            }
            if (playerCollision != null)
            {
                playerCollision.downGravity *= 2.0f;
            }
        }
    }

}
