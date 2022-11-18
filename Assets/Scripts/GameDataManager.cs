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

    // Create a GameData field.
    public GameData gameData = new GameData();

    void Awake()
    {
        // Update the path once the persistent path exists.
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }

    void Start()
    {
        readFile();
        
        
    }

    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            gameData = JsonUtility.FromJson<GameData>(fileContents);
            Debug.Log("MOSTRAR CONTENIDO"+fileContents);
        }
    }

    public void MostrarVariable()
    {
        string fileContents = File.ReadAllText(saveFile);
        gameData = JsonUtility.FromJson<GameData>(fileContents);
        Debug.Log("QueSeGuardaGameManager"+gameData.PartidasClasicas[1]);
    }

    public void MostrarTextoDebug(){
        
        string fileContents = File.ReadAllText(saveFile);
        Debug.Log("MOSTRAR CONTENIDO"+fileContents);
    }

    public void writeFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    public void LeaderBoardShow(GameObject rowPrefab, List<Transform> rowsParent)
    {
        Debug.Log("leaderboard");
        string fileContents = File.ReadAllText(saveFile);
        gameData = JsonUtility.FromJson<GameData>(fileContents);

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