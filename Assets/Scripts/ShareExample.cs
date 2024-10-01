using System.Collections;
using System.IO;
using UnityEngine;

public class ShareExample : MonoBehaviour
{
    [Header("�L���v�`���Ώۂ̃J����(null�̏ꍇ�͑S��ʃL���v�`��)")]
    [SerializeField]
    private Camera _target;

    [Header("Imgur�A�v���P�[�V������Client ID")]
    [SerializeField]
    private string _imgurClientID = "ecbc8b51badcccb";

    [Header("�c�C�[�g����")]
    [SerializeField]
    private string _tweetText;

    [Header("�c�C�[�g�̃n�b�V���^�O")]
    [SerializeField]
    private string[] _hashTags;

    private void Update()
    {

    }

    public void OnClick()
    {
        StartCoroutine(UploadAndTweet());
    }

    private IEnumerator UploadAndTweet()
    {
        // ��ʃL���v�`��
        Texture2D image = null;
        yield return ScreenCapturer.Capture(_target, x => image = x);

        // Imgur�ւ̉摜�f�[�^�A�b�v���[�h
        string imageUrl = null;
        string errorMessage = null;

        yield return ImgurUploader.UploadToImgur(
            _imgurClientID,
            image,
            x => imageUrl = x,
            x => errorMessage = x
        );

        // �A�b�v���[�h�̐��ۃ`�F�b�N
        if (!string.IsNullOrEmpty(errorMessage))
        {
            // ���s�̏ꍇ�͏������f
            Debug.LogError(errorMessage);
            yield break;
        }

        // �g���q�����������e�pURL�ɉ��H
        imageUrl = Path.ChangeExtension(imageUrl, null);

        // �c�C�[�g��ʂ��J��
        TwitterShare.Share(
            string.Format(_tweetText, imageUrl),
            _hashTags
        );
    }
}