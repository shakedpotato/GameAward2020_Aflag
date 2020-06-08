using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] effectSound;
    [SerializeField] private AudioClip[] BGM;
    private AudioSource[] audioSources;

    private bool IsPlayBGM;
    private int stock;

    
    // Start is called before the first frame update
    void Start()
    {
        //Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Load関数はBGMと効果音を読み込むのでゲーム全体の始めの方で呼ぶ
    //実質Start関数
    public void Load()
    {
        audioSources = this.gameObject.GetComponentsInChildren<AudioSource>();

        //Resourcesフォルダ内の各フォルダからSoundを読み込み
        //フォルダ内の上から格納されます
        effectSound = Resources.LoadAll<AudioClip>("Sound/EffectSound");
        BGM         = Resources.LoadAll<AudioClip>("Sound/BGM");
        IsPlayBGM   = false;
        stock = 0;
    }



    //こいつの呼び出しで効果音再生
    //引数でResources/Sound/EffectSound内のindex番目を呼ぶ
    public void PlayEffectSound(int index)
    {
        audioSources[1].clip = effectSound[index];
        audioSources[1].PlayOneShot(audioSources[1].clip);
    }


    //こいつの呼び出しでBGm再生
    //引数でResources/Sound/BGM内のindex番目を呼ぶ
    //各ステージの最初にStartで呼ぶ
    public void StartBGM(int index)
    {
        audioSources[0].clip = BGM[0];
        audioSources[0].Play();
    }



    //こいつの呼び出しでBGM再生
    //引数でResources/Sound/BGM内のindex番目を呼ぶ
    //GameScene内でBGMを変更する際に使用
    public void OnBGM(int index)
    {
        
        if (IsPlayBGM == false)
        {
            audioSources[0].clip = BGM[index];
            IsPlayBGM = true;
        }
        else
        {
            if(stock != index)
            {
                stock = index;
                audioSources[0].clip = BGM[stock];
            }
            audioSources[0].Play();
        }
        

        audioSources[0].Play();
    }


    //BGM消し
    //引数でfadeの有無（falseで突然止まる、trueでジョジョに消えていく）

    public void OffBGM()
    {
        audioSources[0].Stop();
    }
}
