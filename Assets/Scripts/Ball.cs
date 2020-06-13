using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    
    [SerializeField] Vector3 StartPosition;

    Transform m_transform;
    bool restart;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        restart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_transform.position.y <= -5.0)
        {
            restart = true;
            m_transform.position = StartPosition;
        }
        else
        {
            restart = false;
        }

    }

    public bool GetRestart()
    {
        return restart;
    }
}