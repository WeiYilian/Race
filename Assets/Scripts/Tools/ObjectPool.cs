using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResType
{
    Music,
    GameObject
}

//对象池
public class ObjectPool
{
 
    //要生成的对象池预设
    private GameObject prefab;
    //对象池列表
    private List<GameObject> pool;

    private Dictionary<string, AssetBundle> ABDic = new Dictionary<string, AssetBundle>();

    private Dictionary<string, AudioClip> MusicDic = new Dictionary<string, AudioClip>();

    private Dictionary<string, GameObject> ObjDic = new Dictionary<string, GameObject>();
    //构造函数
    public ObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            //进行初始化
            AllLocateInstance();
        }
        
        //将所有ab包解包并存入ABdic
        if (ABDic.Count==0)
        {
            AssetBundle manifestAB = AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
            AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            foreach (string ABName in manifest.GetAllAssetBundles())
            {
                AssetBundle ab = AssetBundle.LoadFromFile("AssetBundles/" + ABName);
                ABDic.Add(ABName, ab);
            }
        }
    }

    #region 生成存放音乐播放器的实例

    //若audiosource不够用，可调用此方法获取新实例
    public GameObject GetInstance()
    {
        if (pool.Count == 0)
        {
            //创建实例
        }
        GameObject instance = pool[0];
        pool.RemoveAt(0);
        instance.SetActive(true);
        return instance;
    }
    
    //生成本地实例
    private GameObject AllLocateInstance()
    {
        GameObject instance = GameObject.Instantiate(prefab);
        instance.transform.SetParent(AudioManager.Instance.transform);
        instance.SetActive(false);
        pool.Add(instance);
        return instance;
    }

    #endregion


    #region ab包

    //从解出来的包中拿出所需的对象
    public void LoadResources(string resName, string filePath,ResType resType)
    {
        LoadAB(resName, filePath, resType);
    }

    private void LoadAB(string resName, string filePath,ResType resType)
    {
        switch (resType)
        {
            case ResType.Music:
                AudioClip sound = ABDic[filePath].LoadAsset<AudioClip>(resName);
                MusicDic.Add(resName, sound);
                break;
            case ResType.GameObject:
                GameObject obj = ABDic[filePath].LoadAsset<GameObject>(resName);
                ObjDic.Add(resName, obj);
                break;
            default:
                break;
        }
    }
    
    //获得音乐
    public AudioClip GetABMusic(string resName)
    {
        return MusicDic[resName];
    }
    
    //获得游戏对象
    public GameObject GetABObj(string resName)
    {
        return ObjDic[resName];
    }

    #endregion
   
}