using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TheGame : MonoBehaviour
{
    public GameDataManager _GameData;
    public AudioSource Musica;
    

    void Start()
    {
        _GameData.readFile(PlayerPrefs.GetString("LastUser"));
    }


    void LogOut()
    {
        PlayerPrefs.SetInt("AutoLogIn",0);
    }

        public void activarmusica()
    {
        Musica.Play();
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