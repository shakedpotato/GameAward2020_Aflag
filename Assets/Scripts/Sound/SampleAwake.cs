using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAwake : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager.Load();
        //BGM開始
        soundManager.StartBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        //BGM変更
        if(Input.GetKeyDown(KeyCode.A))
        {
            soundManager.OnBGM(1);
        }
        //効果音再生
        if (Input.GetKeyDown(KeyCode.S))
        {
            soundManager.PlayEffectSound(0);
        }
        //BGM止め
        if (Input.GetKeyDown(KeyCode.D))
        {
            soundManager.OffBGM();
        }
    }
}
