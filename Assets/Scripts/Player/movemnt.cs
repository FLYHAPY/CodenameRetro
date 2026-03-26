using UnityEngine;
using UnityEngine.InputSystem;

public class movemnt : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public InputActionReference playerControls;
    private Vector2 _moveDirection;
    private Vector2 _lastMoveDirection;
    private bool facingleft = false;

    [SerializeField]
    private Animator anim;
    
    void Start()
    {
        playerControls.action.Enable();
    }

    void Update()
    {
        Animate();
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = _moveDirection * speed;
        _moveDirection = playerControls.action.ReadValue<Vector2>();
    }

    //Set up animation parameters
    void Animate()
    {
        if (_moveDirection.x != 0 || _moveDirection.y != 0)
        {
            anim.SetFloat("MoveX", _moveDirection.x);
            anim.SetFloat("MoveY", _moveDirection.y);
        }
        
        anim.SetFloat("moveMagnitude", _moveDirection.magnitude);

        if ((_moveDirection.x < 0 && !facingleft) || (_moveDirection.x > 0 && facingleft))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            facingleft = !facingleft;
        }
    }
}
