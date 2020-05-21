using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_rotation : MonoBehaviour
{

    Rigidbody2D rigidbody;
    public float RotateSpeed = 2.0f;
    void Start()
    {
        rigidbody =  gameObject.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.mass = 10000;
        rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.MoveRotation(rigidbody.rotation + RotateSpeed);
            //this.transform.Rotate(0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.MoveRotation(rigidbody.rotation - RotateSpeed);
        }


    }
}
