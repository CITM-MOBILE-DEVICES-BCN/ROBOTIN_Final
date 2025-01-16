using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public bool IsMoving() => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
    public bool IsJumping() => Input.GetKeyDown(KeyCode.Space);
    public bool IsDashing() => Input.GetKeyDown(KeyCode.LeftShift);

}

