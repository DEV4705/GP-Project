using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    public void SetDirection1(Vector2 direction)
    {

    }

    public void SetDirection(CallbackContext ctx)
    {
        //Debug.Log("Move");
        if(ctx.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            Debug.Log(ctx.ReadValue<Vector2>());
        }

    }
}
