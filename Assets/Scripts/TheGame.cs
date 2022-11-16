using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheGame : MonoBehaviour
{


    public int toposDeLaPartida;
    // [SerializeField] private int PartidasClasicasJugadas;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void SetName(string name)
    {
        Debug.Log("SET  NAME TO: "+name);
        PlayerPrefs.SetString("username", name); // 4
        PlayerPrefs.Save(); // 5
    }

    public void storeData(int value)
    {
        toposDeLaPartida = value;
    }

    // Update is called once per frame
    void Update()
    {
        
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