using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacking : MonoBehaviour
{
    public InputActionReference playerFire;
    public IDirectionProvider DirectionProvider;
    private bool _isAttacking = false;
    private Vector2 ofset;
    
    
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject attackingCollider;
    
    Dictionary<Vector2, Vector2> _offsets = new Dictionary<Vector2, Vector2>()
    {
        { new Vector2(0, 1),  new Vector2(0.112f, 0.94f) },   // up
        { new Vector2(1, 0),  new Vector2(1.19f, -0.47f) },   // right
        { new Vector2(0, -1),  new Vector2(-0.13f, -1.51f) },   // down
        { new Vector2(-1, 0),  new Vector2(1.19f, -0.47f) },   // left
    };
    
    
    //for shooting
    /*public bool isShooting;
    public GameObject bullet;
    public float shootForce = 10f;*/
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerFire.action.Enable();
        playerFire.action.started += Attack;
        DirectionProvider = GetComponent<IDirectionProvider>();
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        if (_isAttacking)
        {
            return;
        }
        
        _isAttacking = true;
        anim.SetBool("isAttacking", true);
        Vector2 dir = DirectionProvider.GetDirection();
        attackingCollider.SetActive(true);
        if (_offsets.TryGetValue(dir, out Vector2 offset))
        {
            attackingCollider.transform.localPosition = offset;
        }
        
        //Rotating
        //First checking if dir is not 0
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            attackingCollider.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void ResetAttacking()
    {
        _isAttacking = false;
        anim.SetBool("isAttacking", false);
        attackingCollider.SetActive(false);
    }
    
    
    
    //In case I need
    /*private void Shoot(InputAction.CallbackContext obj)
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;
        
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        
        GameObject ball = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
        
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(DirectionProvider.GetDirection() * shootForce, ForceMode2D.Impulse);
    }*/
}
