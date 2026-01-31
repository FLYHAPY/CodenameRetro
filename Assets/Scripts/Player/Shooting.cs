using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public InputActionReference playerFire;
    public InputActionReference playerMOve;
    public bool isShooting;
    public GameObject bullet;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerFire.action.Enable();
        playerFire.action.started += Fire;   
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        GameObject bulletInstance = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().linearVelocity = speed * playerMOve.action.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
