using UnityEngine;

public class Snake3DMovement : MonoBehaviour
{

    public float Speed = 20f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = Vector3.zero;

        velocity += (transform.forward * vertical); //Move forward
        velocity += (transform.right * horizontal); //Strafe

        //Adjust speed and frame rate
        velocity *= Speed * Time.fixedDeltaTime;


        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }
}
