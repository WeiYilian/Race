using UnityEngine;

using UnityEngine.UI;

using System.Collections;
using Unity.Collections;

public class TextContorl : MonoBehaviour
{
    public static TextContorl instance;
    public float letterPause = 1f;
    public string word;
    public GameObject plen;

    public GameObject text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }


    }

    private void Start()
    {



        text.GetComponent<Text>().text = "";



    }

//在外面引用面板直接例如
    public void str(string k)
    { plen.SetActive(true);
        word = k;
        StartCoroutine(TypeText());
    }

    public IEnumerator TypeText()
    {
     
        foreach (char letter in word.ToCharArray())

        {
           text.GetComponent<Text>().text += letter;
            yield return new WaitForSeconds(letterPause);

        }

        Invoke("gameobfalse", 5f);

    }

    private void gameobfalse()
    {
        plen.SetActive(false);
    }









}
