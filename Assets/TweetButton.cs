using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweetButton : MonoBehaviour
{
    [SerializeField]protected Texture2D currentScreenShotTexture;

    private void Awake()
    {
        SetAlpha(0.0f);
    }

    public void CallFinish()
    {
        SetAlpha(1.0f);
    }

    public void SetAlpha(float alpha)
    {
        Color currentColor = transform.Find("BackGround1").GetComponent<Image>().color;
        transform.Find("BackGround1").GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        currentColor = transform.Find("BackGround2").GetComponent<Image>().color;
        transform.Find("BackGround2").GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        currentColor = transform.Find("Text").GetComponent<Text>().color;
        transform.Find("Text").GetComponent<Text>().color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }


    public void OnClick()
    {
        // スクリーンショット用のTexture2D用意
        currentScreenShotTexture = new Texture2D(Screen.width, Screen.height);
    
        StartCoroutine(UpdateCurrentScreenShot());
    }
    
    protected IEnumerator UpdateCurrentScreenShot()
    {
        // これがないとReadPixels()でエラーになる
        yield return new WaitForEndOfFrame();
    
        currentScreenShotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        currentScreenShotTexture.Apply();

        byte[] imageBytes = currentScreenShotTexture.EncodeToPNG();
        var encodedImage = Convert.ToBase64String(imageBytes);
    }
}
