using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class ScreenCapturer
{
    /// <summary>
    /// ��ʃL���v�`��
    /// </summary>
    /// <param name="target">�L���v�`���Ώۂ̃J����(null�̏ꍇ��UI���܂ޑS���)</param>
    /// <param name="onCompleted">�L���v�`�������摜�f�[�^��Texture2D�`���Ŏ󂯎��R�[���o�b�N</param>
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

    // UI���܂ޑS��ʃL���v�`��
    private static IEnumerator CaptureAll(UnityAction<Texture2D> onCompleted)
    {
        // �����_�����O�I���܂őҋ@
        yield return new WaitForEndOfFrame();

        // ��ʑS�̂̃X�N���[���V���b�g�擾
        var screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        // �R�[���o�b�N�Ō��ʂ�Ԃ�
        onCompleted?.Invoke(screenShot);
    }

    // UI���܂܂Ȃ�����J�����L���v�`��
    private static IEnumerator CaptureCamera(Camera target, UnityAction<Texture2D> onCompleted)
    {
        // RenderTexture���쐬
        var rt = new RenderTexture(target.pixelWidth, target.pixelHeight, 24);

        // �J������RenderTexture���ꎞ�I�ɕύX���ăL���v�`��
        var prev = target.targetTexture;
        target.targetTexture = rt;
        target.Render();
        target.targetTexture = prev;

        // �s�N�Z���f�[�^�擾�p�̃e�N�X�`���쐬
        var screenShot = new Texture2D(
            target.pixelWidth,
            target.pixelHeight,
            TextureFormat.RGB24,
            false
        );

        // �s�N�Z���]��
        prev = RenderTexture.active;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();
        RenderTexture.active = prev;

        // �R�[���o�b�N�Ō��ʂ�Ԃ�
        onCompleted?.Invoke(screenShot);

        yield break;
    }
}