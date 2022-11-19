using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TheGame : MonoBehaviour
{
    public int toposDeLaPartida;
    public string ActualUser;
    public GameDataManager _GameData;
    

    // public void SetUserVariable(string Key, string value)
    // {
    //     string name = GetUserName();
    //     PlayerPrefs.SetString(name+"."+Key, value); // 4

    // }


    public string SetName(string name)
    {
        Debug.Log("SET  NAME TO: "+name);
        PlayerPrefs.SetString("username", name); // 4
        PlayerPrefs.Save(); // 5
        return name;
    }


    public void SetLastName(string _lastname)
    {
        PlayerPrefs.SetString("LastUser", _lastname); 
        PlayerPrefs.Save(); 
    }



    // public void GuardarPartidaJSON(string j, int p, int t, bool modo1)
    // {
    //         // string jugador;
    //         // int puntuacion;
    //         // int tiempo;
    //         Debug.Log("GuardarPartidaAJson");
    //         if(modo1)_GameData.gameData.PartidasContraReloj.Add(new Partidas(j, p, t));
    //         if(!modo1)_GameData.gameData.PartidasClasicas.Add(new Partidas(j, p, t));
    //         Debug.Log("QUE SE GUARDA?"+_GameData.gameData.PartidasClasicas[1].jugador);
    //         _GameData.writeFile();
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed primary button.");

        if (Input.GetMouseButtonDown(1)){
            Debug.Log("Pressed secondary button.");
            // SceneManager.LoadScene("LeaderBoard");
        }
        
        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");


        // if (Input.GetKeyDown("j")){
        //     _GameData.DANGERDELETEFILE();
        //     Debug.Log("JJJJ");
        //     }
    }


}












    // public string usuario;
    // private string usuarioKey = "usuario";

        // Debug.Log(Application.persistentDataPath);
        // // Check if the key exists. If not, we never saved the hit count before.
        // if (PlayerPrefs.HasKey(usuarioKey)) // 2
        // {
        //     // Read the hit count from the PlayerPrefs.
        //     usuario = PlayerPrefs.GetString(usuarioKey); // 3
        // }
        // else
        // {
            
        //     PlayerPrefs.SetString(usuarioKey, usuario); // 4
        //     PlayerPrefs.Save(); // 5

        // }




//  public int toposDeLaPartida;
//     public string ActualUser;
//     string LastUser;

    
//     // [SerializeField] private int PartidasClasicasJugadas;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//         ActualUser = GetUserName();
//         if (ActualUser !="" & ActualUser != "Nombre: ")
//         {
//             ActualUser = SetName(ActualUser);
//             LastUser = ActualUser;
//         }   
//     }

//     public string GetUserName()
//     {
//         string username = PlayerPrefs.GetString("username");
//         return username;
//     }

//     public void OnLoggInAutomatico()
//     {
//         SetName(LastUser);
//     }

//     public string SetName(string name)
//     {
//         Debug.Log("SET  NAME TO: "+name);
//         PlayerPrefs.SetString("username", name); // 4
//         PlayerPrefs.Save(); // 5
//         return name;
//     }

//     public void GuardarNumeroDePartidas(int partidasJugadas)
//     {
//         PlayerPrefs.SetString(ActualUser+".partidas",""+partidasJugadas); // 4
//         PlayerPrefs.Save(); // 5
//     }

//     public void storeData(int value)
//     {
//         toposDeLaPartida = value;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }