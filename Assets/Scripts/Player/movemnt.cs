using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movemnt : MonoBehaviour, IDirectionProvider
{
    public Rigidbody2D rb;
    public float speed;
    public InputActionReference playerControls;
    private Vector2 _moveDirection;
    private Vector2 _lastMoveDirection;
    private bool _facingLeft = false;

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
    }

    //Set up animation parameters
    void Animate()
    {
        if (_moveDirection.x != 0 || _moveDirection.y != 0)
        {
            _lastMoveDirection = _moveDirection;
            anim.SetFloat("MoveX", _lastMoveDirection.x);
            anim.SetFloat("MoveY", _lastMoveDirection.y);
        }
        
        anim.SetFloat("moveMagnitude", _moveDirection.magnitude);

        if ((_moveDirection.x < 0 && !_facingLeft) || (_moveDirection.x > 0 && _facingLeft))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            _facingLeft = !_facingLeft;
        }
    }
    
    public Vector2 GetDirection()
    {
        return _lastMoveDirection;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveDirection = playerControls.action.ReadValue<Vector2>();
    }
}
