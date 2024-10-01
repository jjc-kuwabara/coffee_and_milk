using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color currentColor;
        currentColor = transform.Find("Text1").GetComponent<Text>().color;
        transform.Find("Text1").GetComponent<Text>().color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            0.0f
        );
        currentColor = transform.Find("Text2").GetComponent<Text>().color;
        transform.Find("Text2").GetComponent<Text>().color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            0.0f
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallFinish(){
        Color currentColor;
        currentColor = transform.Find("Text1").GetComponent<Text>().color;
        transform.Find("Text1").GetComponent<Text>().color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            1.0f
        );
        currentColor = transform.Find("Text2").GetComponent<Text>().color;
        transform.Find("Text2").GetComponent<Text>().color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            1.0f
        );
    }
}
