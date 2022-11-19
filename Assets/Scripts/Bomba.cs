using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    // Start is called before the first frame update
    float _Time;
    float Speed = 8f;
    Transform[] ts;
    Vector3 newRot = new Vector3(0f, 90f, 0f);

    void Start()
    {
        ts = gameObject.GetComponentsInChildren<Transform>();
        _Time = 0;
        // mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mouseWorldPosition.z = 0f;
        // transform.position = new Vector3(50f,20f ,-2f);
    }


    // Update is called once per frame
    void Update()
    {
        _Time+= Time.deltaTime;
        if (_Time > 2f)Destroy(gameObject);

        gameObject.transform.GetChild(0).transform.position += -ts[0].transform.right*Time.deltaTime*Speed;
        gameObject.transform.GetChild(1).transform.position += ts[1].transform.right*Time.deltaTime*Speed;
        Vector3 y = new Vector3((ts[2].transform.right*Time.deltaTime*Speed)[1],(ts[2].transform.right*Time.deltaTime*Speed)[0],0f );
        gameObject.transform.GetChild(2).transform.position += y;
        gameObject.transform.GetChild(3).transform.position -= y;
        



    }
}
