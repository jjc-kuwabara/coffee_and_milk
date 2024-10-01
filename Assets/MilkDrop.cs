using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDrop : MonoBehaviour
{

    enum E_STATE{
        IDLE,
        DROP,
        TOUCHDOWN,
        END
    };

    const float POSITION_Y_MAX = 3.3f;

    float _dropTimer;

    const float DROPTIMER_MAX = 0.15f;    

    void Awake(){
        transform.position = new Vector3(
            0.0f,
            POSITION_Y_MAX,
            0.0f
        );
        _dropTimer = DROPTIMER_MAX;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            0.0f,
            POSITION_Y_MAX * _dropTimer / DROPTIMER_MAX,
            0.0f
        );

        _dropTimer -= Time.deltaTime;
        if(_dropTimer < 0.0f){
            Color currentColor = GameObject.Find("Milk100").GetComponent<SpriteRenderer>().color;
            float currentAlpha = currentColor.a;
            currentAlpha += 0.05f;
            if(currentAlpha > 1.0f){
                currentAlpha = 1.0f;
            }

            GameObject.Find("Milk100").GetComponent<SpriteRenderer>().color = new Color(
                currentColor.r,
                currentColor.g,
                currentColor.b,
                currentAlpha
            );

            Destroy(this.gameObject);
        }
    }
}
