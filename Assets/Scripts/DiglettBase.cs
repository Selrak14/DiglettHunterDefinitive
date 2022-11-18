using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DiglettBase : MonoBehaviour
{
    private ClassicGameMode GameInstance;
    public SpriteRenderer DiglettGolpeado;
    public SpriteRenderer DiglettNormal;
    public Collision2D BombaCollision;
    

    private float Timer;
    private float initializationTime;
    public float TimeAlive = 2f;
    public float TimeAliveAfterDeath = 1f;
    public float ContraRelojTiempoAnyadir = 1f;
    public int puntuacionDiglett = 1;
    public int VidasDelDiglett = 1;
    int DiglettType; // PARA CUANDO TENGAMOS MAS DIGLETS

    // Start is called before the first frame update
    void Start()
    {
        DiglettGolpeado.enabled = false;
        GameInstance = FindObjectOfType<ClassicGameMode>();
        initializationTime = Time.timeSinceLevelLoad;
        // ContraRelojTiempoAnyadir = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Time.timeSinceLevelLoad-initializationTime);
        // Comprovar que no vive mas de la cuenta 
        if(Time.timeSinceLevelLoad-initializationTime >= TimeAlive)
        {
            GameInstance.AnyadirPosicion(gameObject.transform.position);
            Destroy(gameObject);
        }
    }


    IEnumerator DetroyOnTime()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(TimeAliveAfterDeath);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        GameInstance.AnyadirPosicion(gameObject.transform.position);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomba")
        {
            Debug.Log("Te Mamaste");
            // collision.gameObject.SendMessage("ApplyDamage", 10);
        }
    }

    public void OnMouseDown () 
    {
        // RAYCAST BLOCK
        if(Time.timeScale == 0) return;

        if (Input.GetKey ("mouse 0")) 
        {

            VidasDelDiglett -=1;
            if(VidasDelDiglett == 0){
                // EVITAR DOBLE CLICK
                GetComponent<BoxCollider2D> ().enabled = false;
                // CAMBIAR IMAGEN
                DiglettGolpeado.enabled = true;
                DiglettNormal.enabled = false;
                // Esperar a desruir elemento
                StartCoroutine(DetroyOnTime());
                }
            
            


            // AÑADIR PUNTOS
            GameInstance.AddPuntuation(puntuacionDiglett);
            GameInstance.ModoContrarelojTiempoAnyadido(ContraRelojTiempoAnyadir);
            Debug.Log("Box Clicked!");
            
        }
    }
}
