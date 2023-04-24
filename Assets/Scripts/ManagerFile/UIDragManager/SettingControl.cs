using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    //得到速度控制器和声音控制器
    private Slider seedsilder;
    private Slider Adiosilder;

    public AudioSource Adiol;
    // Start is called before the first frame update
   private void Start()
    {
        seedsilder = gameObject.transform.Find("seedset").Find("Slider").gameObject.GetComponent<Slider>();
        Adiosilder = gameObject.transform.Find("Adiost").Find("Silder").gameObject.GetComponent<Slider>();
        seedsilder.value = GameObject.Find("Main Camera").GetComponent<ViewController>().rotSpeed;
        Adiosilder.value = Adiol.GetComponent<AudioSource>().volume;
    }

    public void OnvalueChanged()
    {Debug.Log("旋转速度："+GameObject.Find("Main Camera").GetComponent<ViewController>().rotSpeed);
    Debug.Log("速度："+seedsilder.value);
        GameObject.Find("Main Camera").GetComponent<ViewController>().rotSpeed = seedsilder.value;
        
    }

    public void Onchangvadio()
    {
        Adiol.GetComponent<AudioSource>().volume = Adiosilder.value;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
