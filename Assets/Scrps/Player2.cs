using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 jump;
    public float jumpForce = 1.0f;
    public bool isTouchingGround = true;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
    }
    void OnCollisionStay() //indica que una de las dos colisiones permanece (este dentro) dentro de otra
    {
        isTouchingGround = true;
    }
    void OnCollisionExit()
    {
        isTouchingGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Keyboard.current.spaceKey.IsPressed()) && (isTouchingGround == true))
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isTouchingGround = false;
        }
        if (Keyboard.current.sKey.IsPressed())
        {
            transform.position += Vector3.forward*speed*Time.deltaTime;
        }
        if (Keyboard.current.wKey.IsPressed())
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }
        if (Keyboard.current.aKey.IsPressed())
         {
             transform.position += Vector3.right * speed * Time.deltaTime;
         }
         if (Keyboard.current.dKey.IsPressed())
         {
             transform.position += Vector3.left * speed * Time.deltaTime;
         }
    }
}
