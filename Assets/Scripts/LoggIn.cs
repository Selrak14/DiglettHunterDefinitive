using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;
using UnityEngine.SceneManagement;

public class LoggIn : MonoBehaviour
{
    TheGame playerInstance;
	public TMP_InputField name;
    public Animator Subida;
    private GameObject DebugObject;
    public float WaitingABit = 1f;
    public Toggle loginToggle;
    
	public void Start()
	{
        // if(PlayerPrefs.GetInt("AutomaticLogIn") == 1)
        // {
        //     loginToggle.isOn = true;
        // }
        // else

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

        // NUNCA SE INICIO EL JUEGO
        if(PlayerPrefs.GetString("LastUser","") =="")
        {
            loginToggle.isOn = false;
            Debug.Log("ES MI PRIMERITO DIA");
        }
        else
        {
            // CARGAR ULTIMO NOMBRE
            name.text = PlayerPrefs.GetString("LastUser");
            Debug.Log("NO SOY NUEVO" + PlayerPrefs.GetInt("AutoLogIn"));
            loginToggle.isOn = (PlayerPrefs.GetInt("AutoLogIn") == 1) ? true : false;
            Debug.Log("ENTRAR? "+loginToggle.isOn);
        }

        // SI EL ULTIMO USUARIO DEBE ENTRAR AUTOMATICO 
		if(loginToggle.isOn)
        {
            Debug.Log("ENTRA AUTOMATICO");
            
            LevelLoad();
        }
	}


    public void GetInputName()
    {
        Debug.Log("Nombre Guardado");
        playerInstance._GameData.readFile(name.text);
        StartCoroutine(SubirNombre());
    }

    IEnumerator SubirNombre()
    {
        // WAIT
        Subida.SetTrigger("Levantar");
        yield return new WaitForSeconds(1f);
        if(loginToggle.isOn) PlayerPrefs.SetInt("AutoLogIn", 1);
        PlayerPrefs.SetString("LastUser", name.text);
        PlayerPrefs.Save();
        LevelLoad();
    }

    public void LevelLoad()
    {
        SceneManager.LoadScene("Lobby");
    }
}
