using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAwake : MonoBehaviour
{
    GameController gameController;
    Goal goal;
    //[SerializeField] private SoundManager soundManager;
    //// Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameControllerObj").GetComponent<GameController>();
        //goal = GameObject.Find("Goal").GetComponent<Goal>();
    }

    // Update is called once per frame
    void Update()
    {
        //スタートBGM

        SoundManager.instance.Play("なぞなぞのなぞ", SoundManager.PLAY_OPTION.CONTINUOUS, true, SoundManager.AUDIO_TYPE.BGM);

        //水が流れている間の効果音
        if (!gameController.GetPausable().pausing)
        {
            Invoke("WaterEffect", gameController.StartDelay);
        }

        //ゴールした時のBGM
        if (gameController.GetGoal().GetCleared() == true)
        {
            SoundManager.instance.Play("ベルのゲームクリア音", SoundManager.PLAY_OPTION.CONTINUOUS, false, SoundManager.AUDIO_TYPE.BGM);
        }

        //リスタート時の効果音
        if(gameController.GetBall().GetRestart() == true)
        {
            SoundManager.instance.Play("restart", SoundManager.PLAY_OPTION.CONTINUOUS, false, SoundManager.AUDIO_TYPE.EFFECT);
        }

        ////効果音再生
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    SoundManager.instance.Play(0, SoundManager.PLAY_OPTION.ADDITIVE, false, SoundManager.AUDIO_TYPE.EFFECT);
        //}

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    SoundManager.instance.Play(2, SoundManager.PLAY_OPTION.ADDITIVE, false, SoundManager.AUDIO_TYPE.BGM);
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    SoundManager.instance.Play("なぞなぞのなぞ", SoundManager.PLAY_OPTION.CONTINUOUS, false, SoundManager.AUDIO_TYPE.BGM);
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    SoundManager.instance.Play("なぞなぞのなぞ", SoundManager.PLAY_OPTION.RESTART, false, SoundManager.AUDIO_TYPE.BGM);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{

        //    SoundManager.instance.GradVolumeFromCurrent(SoundManager.AUDIO_TYPE.BGM, 0.0f, 5.0f);
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{

        //    SoundManager.instance.GradVolumeFromCurrent(SoundManager.AUDIO_TYPE.BGM, 1.0f, 5.0f);
        //}

        //if(Input.GetKeyDown(KeyCode.B))
        //{
        //    SoundManager.instance.ChangeBGM(1);
        //}
    }

    void WaterEffect()
    {
        SoundManager.instance.Play("川の流れる音", SoundManager.PLAY_OPTION.CONTINUOUS, true, SoundManager.AUDIO_TYPE.EFFECT);
    }
}
