using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class ClassicGameMode : MonoBehaviour
{
    TheGame playerInstance;
    public string TipoDePartida = "ClassicMode";
    public GameObject MenuDePausa;
    public GameObject MenuDeFin;
    [SerializeField] private TextMeshProUGUI TextoPuntuacion;
    [SerializeField] private TextMeshProUGUI TiempoDePartidaTexto;
    [SerializeField] private TextMeshProUGUI TextoVida;
    [SerializeField] private TextMeshProUGUI TextoDinero;
    Timer TimerInstance;
    private GameObject DebugObject;
    public GameObject HabBomba;
    public bool ModoContraReloj;
    public GameObject Torreta;
   
    // Fondos y esteticas    
    string MapaString;
    Image BackgroundSprite;

    // Puntuaciones
    int PuntuacionPartida = 0;
    float Money = 0f;
    public int Vida = 20;

    //Digletts
    public List<GameObject> DiglettList = new List<GameObject>();
    public bool RandomPosition;
    public List<Vector2> listOfPosition = new List<Vector2>();
    public GameObject DiglettBase;
    public float TimeBeteenSpawn = .5f;
    

    // ControlDelTiempo
    float TiempoRealDeInicioDelClassicGameMode;
    public float TiempoEsperaInicio = 6f;
    float _Timer;
    public float TiempoDeLaPartida = 30f;
    public bool TiempoDePartidaReverso = true;
    float TimeNormalizado;
    bool juegoHaTerminado;
    public float Acelerador = 0;
    float _Acelerador;
    bool IsAccelerating;
    float BombaMejoraEspera;
    bool BombaCoolDown = true;
    bool PartidaTerminada;

    // Otros
    bool IsBattle = false;

    

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
            DebugObject.AddComponent<GameDataManager>();
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
        _Timer = TiempoEsperaInicio;
        TimerInstance.InicioCuentaAtras(TiempoEsperaInicio);
        StartInGameTime();
        MenuDePausa.SetActive(paused);
        
        if(TipoDePartida == "Batalla") IsBattle = true;
        if(IsBattle)TextoVida.SetText(Vida.ToString());
        if(IsBattle)TextoDinero.SetText(Money.ToString()+"$");
        if(IsBattle & playerInstance._GameData.gameData._NivelTorreta >=1)
        {
            Torreta.SetActive(true);
            Torreta.GetComponent<Torrta>().SetNivelTorreta(playerInstance._GameData.gameData._NivelTorreta);
        }
        // COMPROBAR SETTINGS DE USUARIO
        MapaString = playerInstance._GameData.gameData._MapaSkinCustom;
        Debug.Log("MAPA SPRITE A CARGAR "+MapaString);
        BackgroundSprite = GameObject.Find($"/UIgame/BackgroundEntero").GetComponent<Image>();
        SetMapWithUserSettings();
    }
    

    void SetMapWithUserSettings()
    {
        BackgroundSprite.sprite = Resources.Load<Sprite>($"MapsSkin/{MapaString}");
        Debug.Log(BackgroundSprite);
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
    public void AddPuntuation(int puntos)
    {
        PuntuacionPartida+=puntos;
        TextoPuntuacion.SetText(PuntuacionPartida.ToString());

    }

    public void AddMoney(int money)
    {
        if(IsBattle){
            Money += Mathf.Round(money)/10;
            Debug.Log("DINERO "+Money);
            TextoDinero.SetText(Money.ToString(".0#")+"$");
        }

    }


    public void AddVida(int vida)
    {
        if(IsBattle)
        {
            Vida -= vida;
            TextoVida.SetText(Vida.ToString()); 
        }
       
    }

    // MODO CONTRARRELOJ
    public void ModoContrarelojTiempoAnyadido(float TiempoAnyadir)
    {
        float escala = 200f;
        // Modificar que el tiempo sea menro como avanza la partida 
        if(ModoContraReloj)
        {
            if(TiempoAnyadir * TimeNormalizado/escala >=TiempoAnyadir)
            {
                TiempoDeLaPartida+=.01f;
            }
            else{
                TiempoDeLaPartida+=TiempoAnyadir-(TiempoAnyadir * TimeNormalizado/escala);
            }
        }
        
    }

    public void WhenGameEnds(string level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    public void WhenGameEndsByTime(string level)
    {
        // playerInstance.GuardarPartidaJSON(PlayerPrefs.GetString("LastUser"), PuntuacionPartida, (int) TiempoDeLaPartida , ModoContraReloj);
        // playerInstance.storeData(PuntuacionPartida);
        SceneManager.LoadScene(level);
        
    }
    



    // PONER DIGLETTS

    private void EliminarPosiciones(Vector2 posicion)
    {
        listOfPosition.Remove(posicion);
    }

    public void  AnyadirPosicion(Vector2 posicion)
    {
        listOfPosition.Add(posicion);
    }

    private Vector2 seleccionaAgujero(bool rp)
    {
        if(rp)
        {   

            if(listOfPosition.Count == 0)
            {
                Debug.Log("No hay espacio disponible");
                return new Vector2(100,100);
            }
            int seleccion = Random.Range (0, listOfPosition.Count);
            // Debug.Log(seleccion);
            // Debug.Log(listOfPosition[seleccion]);
            return listOfPosition[seleccion];
        }
        else
        {
            if(TipoDePartida == "Batalla"){
                return new Vector2(Random.Range(-8f,8f), Random.Range(1.5f,-4f));
            }
            return new Vector2((Random.value-Random.value)*8, (Random.value-Random.value)*5);
        }


    }
    private GameObject DiglettSelectionProbabilityes()
    {   
        GameObject retunrer =  DiglettList[0];

        if(Random.Range(0,100) > 80) retunrer = DiglettList[1];
        if(Random.Range(0,100) > 95) retunrer = DiglettList[2];
        if(Random.Range(0,100) > 95) retunrer = DiglettList[3];

        return retunrer;
    }

     IEnumerator coolDown()
    {
        yield return new WaitForSeconds(10f);
        BombaCoolDown = true;
    }

     IEnumerator nuevaBomba()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        var Inst = Instantiate<GameObject>(HabBomba);
        Inst.transform.position = mouseWorldPosition;
        
        yield return new WaitForSeconds(.5f);

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Inst = Instantiate<GameObject>(HabBomba);
        Inst.transform.position = mouseWorldPosition;
        
        yield return new WaitForSeconds(.5f);

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Inst = Instantiate<GameObject>(HabBomba);
        Inst.transform.position = mouseWorldPosition;
        
        yield return new WaitForSeconds(.5f);
       
    }

    private void GenerarBomba()
    {
        BombaCoolDown = false;
        StartCoroutine(nuevaBomba());
        StartCoroutine( coolDown());
    }

    IEnumerator BombaMejoraEsperaSet()
    {

        yield return new WaitForSeconds(BombaMejoraEspera);

    }


    private void Deal(GameObject prefab, Vector2 pos)
    {
        Debug.Log("New position for topo : " + pos);
        if(pos != new Vector2(100,100))
        {
            EliminarPosiciones(pos);
            // Vector2 pos = new Vector2(1,5);
            float posX = pos[0];
            float posY = pos[1];

            var TopoInstancia = Instantiate<GameObject>(prefab);
            TopoInstancia.transform.position = new Vector3(posX, posY, 0.0f);
            TopoInstancia.transform.parent = gameObject.transform;
        }


    }

    // UI

    void StartInGameTime()
    {
        if(TiempoDePartidaReverso)
        {
            float minutes = Mathf.Floor(TiempoDeLaPartida / 60);
            float seconds = TiempoDeLaPartida%60;
            TiempoDePartidaTexto.SetText(string.Format("{0}:{1}", minutes, Mathf.Floor(seconds)));
        }
        else
        {
            TiempoDePartidaTexto.SetText("0");
        }
    }

     string TimeToFormat(float tiempo)
    {
        if(TiempoDePartidaReverso)
        {
            tiempo = (TiempoDeLaPartida + TiempoEsperaInicio)-tiempo;
        }
        else{
            tiempo = tiempo -TiempoEsperaInicio +.1f;
        }

        float minutes = Mathf.Floor(tiempo / 60);
        float seconds = tiempo%60;
        return string.Format("{0}:{1}", minutes, Mathf.Floor(seconds));
    }
    private void SetInGameTime(float tiempo)
    {
        TiempoDePartidaTexto.SetText(TimeToFormat(tiempo));
        // if(TiempoDePartidaReverso)
        // {
        //     // tiempo = (TiempoDeLaPartida + TiempoEsperaInicio)-tiempo;
        //     // float minutes = Mathf.Floor(tiempo / 60);
        //     // float seconds = tiempo%60;
        //     // // TiempoDePartidaTexto.SetText(""+Mathf.Ceil((TiempoDeLaPartida + TiempoEsperaInicio)-tiempo));
        //     // TiempoDePartidaTexto.SetText(string.Format("{0}:{1}", minutes, Mathf.Floor(seconds)));
        //     TiempoDePartidaTexto.SetText(TimeToFormat(tiempo));
        // }
        // else
        // {
        //     float minutes = Mathf.Floor(tiempo / 60);
        //     float seconds = tiempo%60;
        //     // TiempoDePartidaTexto.SetText(""+Mathf.Ceil(tiempo));
        //     TiempoDePartidaTexto.SetText(string.Format("{0}:{1}", minutes, Mathf.Floor(seconds)));
        // }
        
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
        PartidaTerminada = true;
        playerInstance._GameData.gameData._dineroP+=Money;
        Debug.Log("dinero " + Money);
        Debug.Log("dineroUsuario " + playerInstance._GameData.gameData._dineroP);
        if(IsBattle)TiempoDeLaPartida = TimeNormalizado;
                  // public void GuardarClassicGame(    PlayerGameData _g,           string GameType,       string _jugador,                      int _puntuacion,     float _tiempo, float _dinero)
        playerInstance._GameData.GuardarClassicGame(playerInstance._GameData.gameData, TipoDePartida, playerInstance._GameData.gameData._username, PuntuacionPartida, TiempoDeLaPartida, Money);
        
        

        // TimeNormalizado
        juegoHaTerminado = true;
        Debug.Log("JUEGO TERMINADO");
        paused=true;
        MenuDeFin.SetActive(true);
    }
    IEnumerator AcelerarTiempo()
    {
        IsAccelerating = true;
        Debug.Log("Acelerador: NEW CORROTIEN"+_Acelerador);
        yield return new WaitForSeconds(4f);
        if (_Acelerador >= TimeBeteenSpawn/1.1){_Acelerador = _Acelerador;}
        else{
            _Acelerador+=Acelerador;
        } 
        
        IsAccelerating = false;
    }



    void OnGUI()
    {
        TimeNormalizado = Time.time - TiempoRealDeInicioDelClassicGameMode;
        if(juegoHaTerminado)return;


        
        if(TimeNormalizado >= TiempoDeLaPartida+TiempoEsperaInicio & !IsBattle)
        {
            JuegoTerminado();
        }
        if(IsBattle){
            if(Vida <=0)
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
        // DEBUG 
        if (Input.GetMouseButtonDown(2))
        {
            if(BombaCoolDown & IsBattle)GenerarBomba();
            Debug.Log("Pressed middle click.");
        }
        // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
        
        // AL PAUSAR EL JUEGO
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            if (!PartidaTerminada )JuegoPausado();
        }


        // GENERATE NEW TOPO
        _Timer -= Time.deltaTime;
        // if (_Acelerador >= TimeBeteenSpawn) _Acelerador -= Acelerador; 
        if (_Timer <= 0f)
        {
            Deal(DiglettSelectionProbabilityes(), seleccionaAgujero(RandomPosition));
            _Timer = TimeBeteenSpawn - _Acelerador;
            if (!IsAccelerating)StartCoroutine(AcelerarTiempo());
            
        }
    }
}

