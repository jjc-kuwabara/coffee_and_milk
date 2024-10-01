using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetButton : MonoBehaviour
{
    [SerializeField]protected Texture2D currentScreenShotTexture;

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
