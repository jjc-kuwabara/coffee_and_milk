using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class ScreenCapturer
{
    /// <summary>
    /// 画面キャプチャ
    /// </summary>
    /// <param name="target">キャプチャ対象のカメラ(nullの場合はUIを含む全画面)</param>
    /// <param name="onCompleted">キャプチャした画像データをTexture2D形式で受け取るコールバック</param>
    public static IEnumerator Capture(
        Camera target,
        UnityAction<Texture2D> onCompleted
    )
    {
        if (target == null)
        {
            yield return CaptureAll(onCompleted);
        }
        else
        {
            yield return CaptureCamera(target, onCompleted);
        }
    }

    // UIを含む全画面キャプチャ
    private static IEnumerator CaptureAll(UnityAction<Texture2D> onCompleted)
    {
        // レンダリング終了まで待機
        yield return new WaitForEndOfFrame();

        // 画面全体のスクリーンショット取得
        var screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        // コールバックで結果を返す
        onCompleted?.Invoke(screenShot);
    }

    // UIを含まない特定カメラキャプチャ
    private static IEnumerator CaptureCamera(Camera target, UnityAction<Texture2D> onCompleted)
    {
        // RenderTextureを作成
        var rt = new RenderTexture(target.pixelWidth, target.pixelHeight, 24);

        // カメラのRenderTextureを一時的に変更してキャプチャ
        var prev = target.targetTexture;
        target.targetTexture = rt;
        target.Render();
        target.targetTexture = prev;

        // ピクセルデータ取得用のテクスチャ作成
        var screenShot = new Texture2D(
            target.pixelWidth,
            target.pixelHeight,
            TextureFormat.RGB24,
            false
        );

        // ピクセル転送
        prev = RenderTexture.active;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();
        RenderTexture.active = prev;

        // コールバックで結果を返す
        onCompleted?.Invoke(screenShot);

        yield break;
    }
}