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
            if (playerJump != null)
            {
                playerJump.ApplyAdditionalJumpForce(additionalJumpForce);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RobotinJump playerJump = other.GetComponent<RobotinJump>();
            if (playerJump != null)
            {
                playerJump.RemoveAdditionalJumpForce(additionalJumpForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}
