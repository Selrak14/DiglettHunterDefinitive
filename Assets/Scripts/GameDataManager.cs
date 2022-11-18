// Add System.IO to work with files!
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameDataManager : MonoBehaviour
{
    // Create a field for the save file.
    string saveFile;
    public string LastUser = "";
    public int AutoLogIn = 0;

    // Create a GameData field.
    public PlayerGameData gameData; // = new PlayerGameData();

    void Awake()
    {
        // Update the path once the persistent path exists.
        // saveFile = Application.persistentDataPath + "/gamedata.json";
    }

    void Start()
    {
        LastUser = PlayerPrefs.GetString("LastUser","");
        AutoLogIn = PlayerPrefs.GetInt("AutoLogIn",0);
        if(LastUser != "" & AutoLogIn == 1) {
            saveFile = Application.persistentDataPath + "/"+LastUser+".json";
            readFile(LastUser); // SE CREA SI O SI UN GAME OBJECT I UN JSON
        }
    }

    public void DANGERDELETEFILE()
    {
        File.Delete(Application.persistentDataPath + "/gamedata.json"); 
    }

    public void readFile(string name)
    {
        string saveFile = Application.persistentDataPath + "/"+name+".json";
        Debug.Log("Path del archivo: "+saveFile);
        // Does the file exist?
        if (File.Exists(saveFile))
        {

            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            gameData = JsonUtility.FromJson<PlayerGameData>(fileContents);
            Debug.Log("Cargar JSON");
        }
        else{
            gameData = new PlayerGameData(); // EN CASO DE NO EXISTIR
            gameData._username = name;
            Debug.Log("GUARDAR JSON");
        }
        // Guardar para futuras sesiones
        PlayerPrefs.SetString("LastUser",name);
        writeFile(name, gameData);
    }



    public void writeFile(string _name, PlayerGameData _gameData)
    {
        string _saveFile = Application.persistentDataPath + "/"+_name+".json";
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(_gameData);

        // Write JSON to file.
        File.WriteAllText(_saveFile, jsonString);
    }

    public void GuardarClassicGame(string GameType, string _jugador, int _puntuacion, int _tiempo, int _dinero)
    {
        
    }

    public void LeaderBoardShow(GameObject rowPrefab, List<Transform> rowsParent)
    {
        Debug.Log("leaderboard");
        string fileContents = File.ReadAllText(saveFile);
        gameData = JsonUtility.FromJson<PlayerGameData>(fileContents);

        // TODAS LAS LISTAS 
        List<Partidas> SortedListClassic = gameData.PartidasClasicas.OrderByDescending(o=>o.puntuacion).ToList();
        List<Partidas> SortedListReloj = gameData.PartidasContraReloj.OrderByDescending(o=>o.puntuacion).ToList();
        List<Partidas> SortedListBatalla = gameData.PartidasBatalla.OrderByDescending(o=>o.puntuacion).ToList();

        foreach (var item in SortedListClassic)
        {
            GameObject newGo = Instantiate(rowPrefab,rowsParent[0]);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = item.jugador;
            texts[1].text = item.puntuacion.ToString();
        }
        foreach (var item in SortedListReloj)
        {
            GameObject newGo = Instantiate(rowPrefab,rowsParent[1]);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = item.jugador;
            texts[1].text = item.puntuacion.ToString();
        }
        foreach (var item in SortedListBatalla)
        {
            GameObject newGo = Instantiate(rowPrefab,rowsParent[2]);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = item.jugador;
            texts[1].text = item.puntuacion.ToString();
        }



        Debug.Log("Partidas Jugadas "+ gameData.PartidasClasicas.Count);
    }

}