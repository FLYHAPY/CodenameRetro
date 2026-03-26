using UnityEngine;
using UnityEngine.InputSystem;

public class movemnt : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public InputActionReference playerControls;
    private Vector2 _moveDirection;

    [SerializeField]
    private Animator anim;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls.action.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = _moveDirection * speed;
        _moveDirection = playerControls.action.ReadValue<Vector2>();
        anim.SetFloat("MoveX", _moveDirection.x);
        anim.SetFloat("MoveY", _moveDirection.y);
    }
}
