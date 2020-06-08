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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            soundManager.OnBGM(1);
        }
        //効果音再生
        if (Input.GetKeyDown(KeyCode.X))
        {
            soundManager.PlayEffectSound(0);
        }
        //BGM止め
        if (Input.GetKeyDown(KeyCode.C))
        {
            soundManager.OffBGM();
        }
    }
}
