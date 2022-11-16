using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class ClassicGameMode : MonoBehaviour
{
    TheGame playerInstance;
    public GameObject MenuDePausa;
    public GameObject MenuDeFin;
    public int PuntuacionPartida = 0;
    [SerializeField] private TextMeshProUGUI TextoPuntuacion;
    [SerializeField] private TextMeshProUGUI TiempoDePartidaTexto;
    Timer TimerInstance;
    private GameObject DebugObject;

    //Digletts
    public bool RandomPosition;
    public List<Vector2> listOfPosition = new List<Vector2>();
    public GameObject DiglettBase;
    public float TimeBeteenSpawn = .5f;

    // ControlDelTiempo
    float TiempoRealDeInicioDelClassicGameMode;
    public float TiempoEsperaInicio = 6f;
    public float Timer = 3f;
    public float TiempoDeLaPartida = 30f;
    public bool TiempoDePartidaReverso = true;
    float TimeNormalizado;
    bool juegoHaTerminado;
    

    

    void Awake()
    {
        // private GameObject DiglettBase;
        // DiglettBase = Instantiate(Resources.Load("DiglettBase", typeof(GameObject))) as GameObject;
    }


    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("GameController").Length == 0)
        {
            Debug.Log("NO HAY THE GAME");
            // someObject doesn't exist
            DebugObject = new GameObject("Cool GameObject made from Code");
            DebugObject.AddComponent<TheGame>();
            playerInstance = DebugObject.GetComponent<TheGame>();
        }
        else
        {
            Debug.Log("REUTILIZAR THE GAME");
            playerInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheGame>();
        }

        paused = false;
        // playerInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheGame>();
        TimerInstance = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        TiempoRealDeInicioDelClassicGameMode = Time.time;
        StartInGameTime();
        MenuDePausa.SetActive(paused);
        // StartCoroutine(FinalizarPartidaPorTiempo(TiempoDeLaPartida+Timer));


    }
    



    public IEnumerator WaitForRealSeconds( float delay )
    {
        float start = Time.realtimeSinceStartup;
        while( Time.realtimeSinceStartup < start + delay )
        {   
            // Debug.Log("NO ESTA PASANDO");
            yield return null;
            
        }
        Debug.Log("SIIIIII ESTA PASANDO"+Time.realtimeSinceStartup);
        paused = false;
    }

    private bool _paused;
    public bool paused 
    {
        get { return _paused; }
        set 
        {
            _paused = value;
            Time.timeScale = value  ? 0.0f : 1.0f;
            // Debug.Log("VALOR ENVIADO A UI SHOW: "+value); ui.ShowPauseMenu(value);
        }
    }
    public void AddPuntuation()
    {
        PuntuacionPartida++;
        TextoPuntuacion.SetText(PuntuacionPartida.ToString());

    }

    public void WhenGameEnds(string level)
    {
        Time.timeScale = 1f;
        playerInstance.storeData(PuntuacionPartida);
        SceneManager.LoadScene(level);
    }

    public void WhenGameEndsByTime(string level)
    {
        playerInstance.storeData(PuntuacionPartida);
        SceneManager.LoadScene(level);
    }
    



    // PONER DIGLETTS
    private Vector2 seleccionaAgujero(bool RandomPosition)
    {
        if(RandomPosition)
        {
            int seleccion = Random.Range (0, listOfPosition.Count);
            // Debug.Log(seleccion);
            // Debug.Log(listOfPosition[seleccion]);
            return listOfPosition[seleccion];
        }
        else
        {
            return new Vector2((Random.value-Random.value)*8, (Random.value-Random.value)*5);
        }


    }
    private void Deal(GameObject prefab, Vector2 pos)
    {
        Debug.Log("New position for topo : " + pos);
        // Vector2 pos = new Vector2(1,5);
        float posX = pos[0];
        float posY = pos[1];
        var TopoInstancia = Instantiate<GameObject>(prefab);
        // TopoInstancia.SetTopoPosition(id, images[id]);
        TopoInstancia.transform.position = new Vector3(posX, posY, 0.0f);
        TopoInstancia.transform.parent = gameObject.transform;
    }

    // UI

    void StartInGameTime()
    {
        if(TiempoDePartidaReverso)
        {
            TiempoDePartidaTexto.SetText(""+Mathf.Ceil(TiempoDeLaPartida));
        }
        else
        {
            TiempoDePartidaTexto.SetText("0");
        }
    }
    private void SetInGameTime(float tiempo)
    {

        if(TiempoDePartidaReverso)
        {
            tiempo = (TiempoDeLaPartida + TiempoEsperaInicio)-tiempo;
            float minutes = Mathf.Floor(tiempo / 60);
            float seconds = tiempo%60;
            // TiempoDePartidaTexto.SetText(""+Mathf.Ceil((TiempoDeLaPartida + TiempoEsperaInicio)-tiempo));
            TiempoDePartidaTexto.SetText(string.Format("{0}:{1}", minutes, Mathf.Floor(seconds)));
        }
        else
        {
            float minutes = Mathf.Floor(tiempo / 60);
            float seconds = tiempo%60;
            TiempoDePartidaTexto.SetText(""+Mathf.Ceil(tiempo));
            TiempoDePartidaTexto.SetText(string.Format("{0}:{1}", minutes, Mathf.Floor(seconds)));
        }
        
    }

    // ALTERACIONES DEL JUEGO
    private void JuegoPausado()
    {
        Debug.Log("JUEGO PAUSADO");
        paused=!paused;
        MenuDePausa.SetActive(paused);
    }
    private void JuegoTerminado()
    {
        juegoHaTerminado = true;
        Debug.Log("JUEGO TERMINADO");
        paused=!paused;
        MenuDeFin.SetActive(true);
    }

    // IEnumerator FinalizarPartidaPorTiempo(float tiempo)
    // {
    //     //Print the time of when the function is first called.
    //     Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //     //yield on a new YieldInstruction that waits for 5 seconds.
    //     yield return new WaitForSeconds(tiempo);

    //     //After we have waited 5 seconds print the time again.
    //     Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //     JuegoTerminado();
    // }

    void OnGUI()
    {
        TimeNormalizado = Time.time - TiempoRealDeInicioDelClassicGameMode;
        if(juegoHaTerminado)return;
        
        if(TimeNormalizado >= TiempoDeLaPartida+TiempoEsperaInicio)
        {
            JuegoTerminado();
        }

        if(TimeNormalizado >= TiempoEsperaInicio & TimeNormalizado <= TiempoEsperaInicio+TiempoDeLaPartida)
        {
            SetInGameTime(TimeNormalizado);
        }

        if (TimerInstance != null )
        {
            TimerInstance.ModificarTexto(TimeNormalizado, TiempoEsperaInicio);
        }

        
    }


    // Update is called once per frame
    void Update()
    {   

        // AL PAUSAR EL JUEGO
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            JuegoPausado();
        }

        // GENERATE NEW TOPO
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Deal(DiglettBase, seleccionaAgujero(RandomPosition));
            Timer = TimeBeteenSpawn;
        }
    }
}

