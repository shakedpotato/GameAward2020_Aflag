using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_rotation : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(0.0f, 0.0f, -1.0f);
        }


    }
}
