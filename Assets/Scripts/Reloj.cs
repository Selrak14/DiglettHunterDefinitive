using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reloj : MonoBehaviour
{
    private ClassicGameMode GameInstance;
    private bool reverso;
    private float timeStart;
    [SerializeField] private TextMeshProUGUI TimerText;
    private float TiempoTranscurrido;
    private float TiempoQueFalta;
    private float CadaSegundo;

    // Start is called before the first frame update
    void Start()
    {
        GameInstance = FindObjectOfType<ClassicGameMode>();
        CadaSegundo = GameInstance.Timer;
        reverso = GameInstance.TiempoDePartidaReverso;
        timeStart = GameInstance.TiempoDeLaPartida;
        TiempoQueFalta = timeStart + CadaSegundo;
        TimerText.SetText(""+timeStart);
        TiempoTranscurrido = -2.9f;
        if(reverso)
        {
            TimerText.SetText(""+timeStart);
        }
        else{
            TimerText.SetText("0");
        }
    }


    // Update is called once per frame
    void Update()
    {
        TiempoTranscurrido += Time.deltaTime;
        TiempoQueFalta -= Time.deltaTime;
        CadaSegundo -= Time.deltaTime;

        // ejecutar Cada segundo
        if (CadaSegundo<=0f)
        {
            CadaSegundo = 1f;

            if(reverso)
            {
                TimerText.SetText(Mathf.FloorToInt(TiempoQueFalta).ToString());
            }
            else
            {
                TimerText.SetText(Mathf.FloorToInt(TiempoTranscurrido).ToString());
            }
        }

    }
}
