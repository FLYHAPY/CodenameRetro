using UnityEngine;
using UnityEngine.InputSystem;

public class movemnt : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public InputActionReference playercontrols;
    private Vector2 moveDiretion;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playercontrols.action.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = moveDiretion * speed;
        moveDiretion = playercontrols.action.ReadValue<Vector2>();
    }
}
