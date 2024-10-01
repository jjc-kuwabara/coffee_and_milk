using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkJag : MonoBehaviour
{
    [SerializeField]GameObject milkDropPrefab;
    bool _isPour = false;

    enum E_STATE{
        IDLE,
        POUR_IN,
        POUR,
        POUR_OUT,
        END
    };

    [SerializeField]E_STATE _state = E_STATE.IDLE;
    const float ROTATE_Z_PER_SEC = 30.0f;
    const float ROTATE_Z_MAX = 60.0f;
    const float ROTATE_Z_MIN = 0.0f;

    float _dropTimer;

    const float DROPTIMER_MAX = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state){
            case E_STATE.IDLE:
                if(_isPour){
                    _state = E_STATE.POUR_IN;
                }
                break;
            case E_STATE.POUR_IN:
                {
                    transform.rotation = Quaternion.Euler(
                        0.0f,
                        0.0f,
                        transform.rotation.eulerAngles.z + ROTATE_Z_PER_SEC * Time.deltaTime
                    );

                    if(transform.rotation.eulerAngles.z >= ROTATE_Z_MAX){
                        transform.rotation = Quaternion.Euler(
                            0.0f,
                            0.0f,
                            ROTATE_Z_MAX
                        );
                        _state = E_STATE.POUR;
                    }

                    if(_isPour == false){
                        _state = E_STATE.POUR_OUT;
                    }
                }
                break;
            case E_STATE.POUR:
                _dropTimer -= Time.deltaTime;
                if(_dropTimer < 0.0f){
                    DropMilk();
                    _dropTimer = DROPTIMER_MAX;
                }

                if(_isPour == false){
                    _state = E_STATE.POUR_OUT;
                }
                break;
            case E_STATE.POUR_OUT:
                {
                    transform.rotation = Quaternion.Euler(
                        0.0f,
                        0.0f,
                        transform.rotation.eulerAngles.z - ROTATE_Z_PER_SEC * Time.deltaTime
                    );

                    if(transform.rotation.eulerAngles.z <= ROTATE_Z_MIN || transform.rotation.eulerAngles.z > ROTATE_Z_MAX + 0.01f){
                        transform.rotation = Quaternion.Euler(
                            0.0f,
                            0.0f,
                            ROTATE_Z_MIN
                        );
                        _state = E_STATE.END;
                        Finish();
                    }       
                }         
                break;
            default:
            break;
        }
    }

    public void SetPour(bool isPour){
        _isPour = isPour;
    }

    private void DropMilk(){
        Instantiate(milkDropPrefab);
    }

    private void Finish()
    {
        CallFinish();
        GameObject.Find("ResultText").GetComponent<ResultText>().CallFinish();
        GameObject.Find("TweetButton").GetComponent<TweetButton>().CallFinish();
    }

    public void CallFinish(){
        Color currentColor;
        currentColor = transform.GetComponent<SpriteRenderer>().color;
        transform.GetComponent<SpriteRenderer>().color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            0.0f
        );
    }
}
