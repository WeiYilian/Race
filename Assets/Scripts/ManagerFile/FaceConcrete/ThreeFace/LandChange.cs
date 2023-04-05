using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandChange : MonoBehaviour
{// Start is called before the first frame update
    //土地改变的颜色
    public Sprite ChangimaImage1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //触发检测，检测到锄头，浇水，进行操作
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name=="hoe")//层级为open的物体
        {
            Debug.Log("jr");
            // currentTarget = other.gameObject; targetColor = other.GetComponent<Image>().color; 
            gameObject.GetComponent<Image>().sprite = ChangimaImage1;
        }
    }
}
