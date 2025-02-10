using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void SetDirection1(Vector2 direction)
    {
        rb2d.velocity = direction * speed;
    }

    public void SetDirection(CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        if(ctx.phase == UnityEngine.InputSystem.InputActionPhase.Performed || ctx.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
        {
            Debug.Log(ctx.ReadValue<Vector2>());
            SetDirection1(direction);
        }

    }
}
