using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamerra : MonoBehaviour
{

    public float speed = 1f;
    public Transform Target;
    public Camera cam;
    //Vector3 offset;

    /**void Start()
    {
        offset = transform.position - Target.transform.position;
    }
    **/

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    public void Move()
    {

        Vector3 direction = (Target.position - cam.transform.position).normalized;

        Quaternion lookrotation = Quaternion.LookRotation(direction);

        lookrotation.x = transform.rotation.x;
        lookrotation.y = transform.rotation.y;
        //lookrotation.z = transform.rotation.z;

        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 10);

        //Vector3 desiredPosition = Target.transform.position + offset;
        //transform.position = desiredPosition; 
        transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime * speed);


        //transform.LookAt(Target.transform.position);

    }

}
