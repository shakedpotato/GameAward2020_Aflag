using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.PlayerLoop;

// !!!!!!!!!!!! READ ME !!!!!!!!!!!!!!!!!!!!!!!    2020/06/09 現在
// SoundManager 
// 概略: シングルトンになっています。シーン上にあらかじめ配置してください。
// Play(...)関数の呼び出しで再生できます。
// 音量調節とかはPlay(...)関数で再生された音源にのみ、適用されます。
// 一応AudioMixerGroupに生成された音源をセットするようにしていますが、音量調節は各音源のAudioSourceに由来しています。
// 再生の呼び出しまでAudioSourceは生成されないので注意してください(上記と重複しますが...)
// DontDestroyOnLoad()してないのでシーンまたぐとまたロードからやり直します。
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!      
public class SoundManager : MonoBehaviour
{
    // シングルトン
    static public SoundManager instance
    {
        get;
        private set;
    }

    // AudioSourceをあつかいやすくするためのやつ。
    class AudioEntity
    {
        public GameObject gameObject;
        public AudioSource audioSource;
        public string audioName
        {
            get { return audioSource.name; }
        }
        public AUDIO_TYPE audioType
        {
            get;
            private set;
        }


        public AudioEntity(AudioClip clip, GameObject manager, AUDIO_TYPE t)
        {
            gameObject = new GameObject(clip.name);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            gameObject.transform.parent = manager.transform;
            audioType = t; 
            ResetSetting();
            SetMixerGroup();
        }

        public float time
        {
            get { return audioSource.time; }
            set { audioSource.time = value; }
        }

        public bool isLoop
        {
            get { return audioSource.loop; }
            set { audioSource.loop = value; }
        }

        public bool isPlaying
        {
            get { return audioSource.isPlaying; }
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public void Play(float delaySec)
        {

            audioSource.PlayDelayed(delaySec);
            // Audio Mixerにぶち込む TODO---!>
        }

        // ボリューム
        public float currentVolume
        {
            get { return audioSource.volume; }
            set { audioSource.volume = value; }
        }

        private float originVol = 1.0f;
        private float targetVol = 1.0f;
        private float gperiod = 0.0f; // 音量遷移にかける時間
        private float gdt = 0.0f;     // 音量遷移の経過時間


        // 音量の遷移の設定。
        public void SetGradVolume(float origin, float target, float period)
        {
            originVol = origin;
            targetVol = target;
            gperiod = period;
            gdt = 0.0f;
        }


        public void Update()
        {
            // 音量の遷移
            if (originVol != targetVol)
            {
                currentVolume = currentVolume + (targetVol - originVol) / gperiod * Time.deltaTime;
                gdt += Time.deltaTime;

                if (gperiod < gdt)
                {
                    originVol = currentVolume = targetVol;
                }
            }

            
        }

        public void SetMixerGroup()
        {
            AudioMixerGroup mixer = null;

            if (audioType == AUDIO_TYPE.BGM)
            {
                mixer = SoundManager.instance.bgmMixer;
            }
            else if (audioType == AUDIO_TYPE.EFFECT)
            {
                mixer = SoundManager.instance.seMixer;
            }
            else if (audioType == AUDIO_TYPE.NON_CATEGORIZED)
            {
                mixer = SoundManager.instance.nonCategorizedMixer;
            }
            else
            {
                Debug.Log("arienai hanashi");
            }

            audioSource.outputAudioMixerGroup = mixer;
        }

        public void ResetSetting()
        {
            time = 0.0f;
            currentVolume = 1.0f;
            originVol = 1.0f;
            targetVol = 1.0f;
            gperiod = 0.0f;
            gdt = 0.0f;
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }

    // メンバー
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private List<AudioEntity> audioEntities; // 子のAudio Source Component達 
    private int seCount = 0;
    private int bgmCount = 0;
    // Audio Mixer置き場。Inspectorで挿入してください(スクリプトでの取得がわからなかったので)。
    [SerializeField] private AudioMixerGroup seMixer = null;
    [SerializeField] private AudioMixerGroup bgmMixer = null;
    [SerializeField] private AudioMixerGroup nonCategorizedMixer = null;

    public enum AUDIO_TYPE // AudioEntityの種類。
    {
        BGM,
        EFFECT,
        NON_CATEGORIZED,    // これややこしいだけかも
        ENUM_MAX
    }

    public enum PLAY_OPTION
    {
        ADDITIVE,           // 既に再生中の場合、追加で再生する。
        RESTART,            // 既に再生中の場合、リスタートする。
        CONTINUOUS          // 既に再生中の場合、新たに再生せず、そのまま。
                            // 上記のいずれもこれまで再生されてなければ、新たに再生されます。
    }

    private void Awake()
    {
        // シングルトン用の処理
        if (instance && instance != this)
        {
            Debug.Log("SoundManager object is about to be destroyed properly.");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        Init();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var e in audioEntities)
        {
            e.Update();
        }

        // 再生中でないものをさくじょ
        foreach(var e in audioEntities)
        {
            if(!e.isPlaying)
            {
                e.Destroy();
            }
        }
        audioEntities.RemoveAll((e => !e.isPlaying));
    }

    // 初期化処理
    private void Init()
    {
        audioEntities = new List<AudioEntity>();
        LoadAllAudioClips();
    }

    // 全Audio Clipの読込
    private void LoadAllAudioClips()
    {
        // Resourcesフォルダ内の各フォルダからSoundを読み込みAudioClipを生成する。
        // フォルダ内の上から格納されます。
        AudioClip[] effect = Resources.LoadAll<AudioClip>("Sound/EffectSound");
        seCount = effect.Length;
        AudioClip[] bgm = Resources.LoadAll<AudioClip>("Sound/BGM");
        bgmCount = bgm.Length;

        audioClips = new AudioClip[seCount + bgmCount];

        Array.Copy(effect, audioClips, effect.Length);
        Array.Copy(bgm, 0, audioClips, effect.Length, bgm.Length);
    }

    // para0: 再生する音源データのaudioClips中の0から始まる番号。
    // para1: 再生オプション。 型のコメントを参照。
    // para2: ループ再生するか。                                                 
    // para3: 音源のタイプ(BGM or SE or 未分別) <= これ正直よくなかった。曲と種類の相関表を作るべきでした。
    // para4: 音源の再生開始位置(秒)
    // para5: 再生開始の遅延(秒)。
    // デフォルトの音量は最大です。*当然ですが、各種Mixerによる影響は受けます。
    public void Play(int index, PLAY_OPTION option, bool isLoop = false, AUDIO_TYPE type = AUDIO_TYPE.NON_CATEGORIZED, float startSec = 0.0f, float delay = 0.0f)
    {
        if (option == PLAY_OPTION.CONTINUOUS)
        { // 指定された音源が再生中かチェック
            AudioEntity entity = audioEntities.Find(x => { return x.audioName == audioClips[index].name; });

            if (entity != null)
            {
                if (entity.isPlaying)
                {
                    Debug.Log("Already played. The option type is CONTINUOUS: audio name is " + audioClips[index].name + ".");
                    return;
                }
                else
                {
                    entity.Play(delay);
                }
                return;
            }
        }
        else if (option == PLAY_OPTION.RESTART)
        {
            AudioEntity entity = audioEntities.Find(x => { return x.audioName == audioClips[index].name; });

            if (entity != null)
            {
                if (entity.isPlaying)
                {
                    entity.time = 0.0f;
                    entity.Play(delay);
                    return;
                }
            }
        }
        else //  PLAY_OPTION::ADDITIVE
        {
            AudioEntity entity = audioEntities.Find(x => { return x.audioName == audioClips[index].name; });

            if (entity != null)
            {
                if (!entity.isPlaying)
                {
                    // 既に確保されたAudioSourceを再利用する。
                    entity.time = 0.0f;
                    entity.ResetSetting();
                    entity.Play(delay);
                    return;
                }
            }
        }

        // 新たにAudio Sourceを生成して、再生する。
        PlayNewAudio(index, isLoop, type, delay);
    }

    private void PlayNewAudio(int index, bool isLoop = false, AUDIO_TYPE type = AUDIO_TYPE.NON_CATEGORIZED, float startSec = 0.0f, float delay = 0.0f)
    {
        // AudioEntityを生成する。
        AudioEntity entity = new AudioEntity(audioClips[index], gameObject, type);

        audioEntities.Add(entity); // 子にAudio Source Componentを持たせて、参照を保持しておく。

        int endId = audioEntities.Count - 1;
        audioEntities[endId].isLoop = isLoop;
        audioEntities[endId].time = startSec;

        audioEntities[endId].Play(delay);
    }

    public void Play(string name, PLAY_OPTION option, bool isLoop = false, AUDIO_TYPE type = AUDIO_TYPE.NON_CATEGORIZED, float startSec = 0.0f, float delay = 0.0f)
    {
        int index = Array.FindIndex<AudioClip>(audioClips, (AudioClip e) => { return e.name == name; });
        Play(index, option, isLoop, type, startSec, delay);
    }

    // 音量調節
    public void SetVolume(AUDIO_TYPE type, float volume)
    {
        var list = audioEntities.FindAll((AudioEntity e) => { return e.audioType == type; });

        foreach (var e in list)
        {
            e.currentVolume = volume;
        }
    }

    // 音量の遷移
    public void GradVolume(int index, float origin, float target, float period)
    {
        var entity = audioEntities.Find((AudioEntity e) => { return e.audioSource.clip == audioClips[index]; });

        if (entity != null)
            entity.SetGradVolume(origin, target, period);
    }

    // 音量の遷移
    public void GradVolume(string name, float origin, float target, float period)
    {
        GradVolume(GetAudioIndexFromName(name), origin, target, period);
    }

    // 音量の遷移
    public void GradVolume(AUDIO_TYPE type, float origin, float target, float period)
    {
        var list = audioEntities.FindAll((AudioEntity e) => { return e.audioType == type; });

        foreach (var e in list)
        {
            e.SetGradVolume(origin, target, period);
        }
    }

    // 現在の音量から遷移
    public void GradVolumeFromCurrent(int index, float target, float period)
    {
        var entity = audioEntities.Find((AudioEntity e) => { return e.audioSource.clip == audioClips[index]; });

        if (entity != null)
            entity.SetGradVolume(entity.currentVolume, target, period);
    }

    // 現在の音量から遷移
    public void GradVolumeFromCurrent(string name, float target, float period)
    {
        GradVolumeFromCurrent(GetAudioIndexFromName(name), target, period);
    }

    public void GradVolumeFromCurrent(AUDIO_TYPE type, float target, float period)
    {
        var list = audioEntities.FindAll((AudioEntity e) => { return e.audioType == type; });

        foreach (var e in list)
        {
            e.SetGradVolume(e.currentVolume, target, period);
        }
    }
    // 音源のインデックスの取得。
    public int GetAudioIndexFromName(string name)
    {
        return audioEntities.FindIndex((AudioEntity e) => { return e.audioName == name; });
    }

    public void ChangeBGM(int index )
    {
        var list = audioEntities.FindAll((AudioEntity e) => { return e.audioType == AUDIO_TYPE.BGM; });

        foreach (var e in list)
        {
            e.Stop();
        }

        Play(index, PLAY_OPTION.RESTART, true, AUDIO_TYPE.BGM);
    }

}
