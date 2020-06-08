using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearImage : MonoBehaviour
{
    const float scaleStep = 0.3f;    
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 徐々にサイズを大きくする。
        if(transform.localScale.x <= 2.0f)
        {
            float d = transform.localScale.x + scaleStep * Time.deltaTime;
            transform.localScale = new Vector3(d, d, 1.0f);
        }
    }
}
