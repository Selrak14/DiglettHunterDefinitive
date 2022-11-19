using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torrta : MonoBehaviour
{
    public float VelTorreta = 3f;
    float _Time;
    // float Speed = 8f;
    bool isBala = false;
    Vector2 moverse;
    GameObject b;
    // Transform[] ts;
    

    void Start()
    {
        // ts = gameObject.GetComponentsInChildren<Transform>();
        _Time = VelTorreta;
        // mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mouseWorldPosition.z = 0f;
        // transform.position = new Vector3(50f,20f ,-2f);
    }



    IEnumerator DestroyBala(GameObject bala)
    {
        Vector3 m = new Vector3( moverse[0],moverse[1],0f );
        Debug.Log("DESPLAZAR");
        bala.transform.position += m;

        yield return new WaitForSeconds(1f);
        isBala = false;
        Destroy(bala);


    }

    public Vector2 RandomUnitVector()
    {
        float random = Random.Range(0f, 2f);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    GameObject CrearBala()
    {
        // GENRAR
        isBala = true;
        GameObject bala = new GameObject("bala");
        bala.AddComponent<Image>();
        bala.AddComponent<BoxCollider2D>();
        bala.AddComponent<Rigidbody2D>();
        bala.transform.parent = gameObject.transform;
        StartCoroutine(DestroyBala(bala)); 
        moverse = RandomUnitVector();
        Debug.Log("DIRECCION BALA "+moverse.ToString());
        //MOVER EN EL TIEMPO
        return bala;
    }

    void MoverBala(GameObject _bala)
    {
        Vector3 m = new Vector3( moverse[0],moverse[1],0f )*6f;
        Debug.Log("DESPLAZAR");
        _bala.transform.position += m;
    }
    

    // Update is called once per frame
    void Update()
    {
        _Time-= Time.deltaTime;
        if(_Time <=0 & !isBala){
            b = CrearBala();
            _Time = VelTorreta;
            }
        if (isBala)MoverBala(b);

        // if (_Time > 2f)Destroy(gameObject);

        // gameObject.transform.GetChild(0).transform.position += -ts[0].transform.right*Time.deltaTime*Speed;
        // gameObject.transform.GetChild(1).transform.position += ts[1].transform.right*Time.deltaTime*Speed;
        // Vector3 y = new Vector3((ts[2].transform.right*Time.deltaTime*Speed)[1],(ts[2].transform.right*Time.deltaTime*Speed)[0],0f );
        // gameObject.transform.GetChild(2).transform.position += y;
        // gameObject.transform.GetChild(3).transform.position -= y;
        



    }
}
