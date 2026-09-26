using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public Vector3 jump;
    public float jumpForce = 1.0f;
    public bool isTouchingGround = true;
    Rigidbody rb;
    /*public CharacterController controller;
    public CameraFollowbetter cam;
    float currentVelocity; 
    float smoothTime = 0.1f;*/
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
        if (Input.GetKeyDown(KeyCode.Space) && (isTouchingGround == true))
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isTouchingGround = false;
        }

        /*float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");*/
        if (Input.GetKey(KeyCode.S))
        {
        transform.position -= Vector3.forward*speed*Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.W)) 
        {
        transform.position += Vector3.forward*speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            transform.position += Vector3.right*speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.position += Vector3.left*speed*Time.deltaTime;
        }
        /*Vector3 direction = new Vector3 (vertical * Time.deltaTime, 0, horizontal * Time.deltaTime);
        transform.position += direction;
        if (direction.magnitude >= 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;//+localEulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir*speed*Time.deltaTime);
        }*/
       /* void OnTriggerEnter(Collider other)
        {
            Destroy(this);
        }*/
    }
}

        


