using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;//单例
 
    private bool isOut;
    private bool isIn;
 
    void Awake()
    {
        var find = GameObject.Find("GameLoop");

        if (find == this.gameObject)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//场景跳转之后不销毁该游戏物体
        }
        else
            Destroy(gameObject);
        
        GameObject audioPrefab = new GameObject("AudioObjectPool");
        audioPrefab.AddComponent<AudioSource>();
        audioPrefab.GetComponent<AudioSource>().playOnAwake = false;
        m_ObjectPool = new ObjectPool(audioPrefab, initAudioPrefabcount);
        audioPrefab.hideFlags = HideFlags.HideInHierarchy;
        audioPrefab.transform.SetParent(transform);

        ABInit();
        AudioInit();
        
        foreach (AudioClip ac in audioList)
        {
            audioDic.Add(ac.name, ac);
        }
        
        bgAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    //audioClipl列表
    private List<AudioClip> audioList;
    //初始声音预设数量
    private int initAudioPrefabcount = 7;
    //记录静音前的音量大小
    [HideInInspector]
    public float tempVolume = 0;
    //是否静音
    private bool isMute = false;
    public bool IsMute
    {
        set
        {
            isMute = value;
            if (isMute)
            {
                tempVolume = AudioListener.volume;
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = tempVolume;
            }
        }
        private get { return isMute; }
    }
    
    //声音大小系数
    private float volumeScale = 1;
    
    public float VolumeScale
    {
        set
        {
            volumeScale = Mathf.Clamp01(value);
            if (!IsMute)
            {
                AudioListener.volume = value;
            }
        }
        private get { return volumeScale; }
    }
 
    //audio字典
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
 
    //背景音乐
    private AudioSource bgAudioSource;
 
    //声音对象池
    private ObjectPool m_ObjectPool;

    //初始化ab包
    private void ABInit()
    {
        m_ObjectPool.LoadResources("button","button.ab",ResType.Music);
        m_ObjectPool.LoadResources("1","start.ab",ResType.Music);
        m_ObjectPool.LoadResources("2","music.ab",ResType.Music);
        
    }
    
    private void AudioInit()
    {
        audioList = new List<AudioClip>()
        {
            //TODO:添加音乐
            m_ObjectPool.GetABMusic("button"),
            m_ObjectPool.GetABMusic("1"),
            m_ObjectPool.GetABMusic("2")
        };
    }

    /// <summary>
    /// 音频播放
    /// </summary>
    /// <param name="index">播放器序号（用第几个AudioSource播放）</param>
    public void PauseAudio(int index)
    {
        AudioSource audioSource = transform.GetChild(index).GetComponent<AudioSource>();
        audioSource.Pause();
    }

    /// <summary>
    /// 停止播放声音
    /// </summary>
    /// <param name="index">播放器序号</param>
    public void StopAudio(int index)
    {
        AudioSource audioSource = transform.GetChild(index).GetComponent<AudioSource>();
        audioSource.Stop();
    }
 
 
    /// <summary>
    /// 播放背景音乐，固定0号播放器用来播放背景音乐
    /// </summary>
    /// <param name="audioNme"></param>
    public void PlayBGMAudio(string audioNme)
    {
        AudioClip audioClip;
        if (audioDic.TryGetValue(audioNme, out audioClip))
        {
            bgAudioSource.gameObject.SetActive(true);
            bgAudioSource.clip = audioClip;
            transform.GetChild(0).gameObject.SetActive(true);
            bgAudioSource.Play();
            bgAudioSource.loop = true;
        }
    }

    /// <summary>
    /// 按按钮固定播放声音方法,用1号播放器
    /// </summary>
    public void PlayButtonAudio()
    {
        PlayAudio(1, "Button");
    }

    /// <summary>
    /// 重载播放，只有播放器和音频
    /// </summary>
    /// <param name="index">播放器序号</param>
    /// <param name="audioName">音频的名称</param>
    public void PlayAudio(int index, string audioName)
    {
        AudioSource audioSource = this.transform.GetChild(index).GetComponent<AudioSource>();
 
        if (IsMute || audioSource == null)
        {
            return;
        }
        StopAllCoroutines();
        AudioClip audioClip;
        if (audioDic.TryGetValue(audioName, out audioClip))
        {
            //Debug.Log("按钮点击的clip名字是："+audioClip.name);
            audioSource.gameObject.SetActive(true);
            audioSource.clip = audioClip;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
}

