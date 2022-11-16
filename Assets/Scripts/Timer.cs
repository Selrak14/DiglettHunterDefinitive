using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;

    void Start()
    {

    }

    public void ModificarTexto(float timerValue, float TiempoDeEspera)
    {
        
        if(timerValue <= TiempoDeEspera)
        {
            TimerText.SetText(""+Mathf.Ceil(TiempoDeEspera-timerValue));
        }
        else {DestruirTiempoDeEspera();}
    }

    public void DestruirTiempoDeEspera()
    {
        Debug.Log("DESTRUIR TIEMPODECARGA");
        Destroy(gameObject);
    }

}

// public class Timer : MonoBehaviour
// {
//     public int CuentaAtras = 3; 
//     private float _CuentaAtras = 0f;
//     private float timePassed = 0;
//     private int TextoDelTiempo;
//     [SerializeField] private TextMeshProUGUI TextoPuntuacion;
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         TextoDelTiempo = CuentaAtras;
//         TextoPuntuacion.SetText(""+TextoDelTiempo);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         timePassed += Time.deltaTime;
//         _CuentaAtras+=Time.deltaTime;

//         if (_CuentaAtras >= CuentaAtras)
//         {
//             Destroy(gameObject);
//         }

//         if(timePassed > 1f)
//         {
//             timePassed = 0f;
//             TextoDelTiempo--;
//             Debug.Log("HA PASADO UN SEGUNDO");
//             TextoPuntuacion.SetText(""+TextoDelTiempo);
//         } 
//     }


// }