using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAwake : MonoBehaviour
{
    //[SerializeField] private SoundManager soundManager;
    //// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //効果音再生
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.instance.Play(0, SoundManager.PLAY_OPTION.ADDITIVE, false, SoundManager.AUDIO_TYPE.EFFECT);
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.instance.Play(2, SoundManager.PLAY_OPTION.ADDITIVE, false, SoundManager.AUDIO_TYPE.BGM);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SoundManager.instance.Play("なぞなぞのなぞ", SoundManager.PLAY_OPTION.CONTINUOUS, false, SoundManager.AUDIO_TYPE.BGM);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SoundManager.instance.Play("なぞなぞのなぞ", SoundManager.PLAY_OPTION.RESTART, false, SoundManager.AUDIO_TYPE.BGM);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            SoundManager.instance.GradVolumeFromCurrent(SoundManager.AUDIO_TYPE.BGM, 0.0f, 5.0f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            SoundManager.instance.GradVolumeFromCurrent(SoundManager.AUDIO_TYPE.BGM, 1.0f, 5.0f);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.instance.ChangeBGM(1);
        }
    }
}
