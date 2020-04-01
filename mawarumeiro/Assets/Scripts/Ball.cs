using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public string scene_name;

    Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_transform.position.y <= -5.0)
        {
            SceneManager.LoadScene(scene_name);
        }

    }


}