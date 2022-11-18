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

        if(PlayerPrefs.GetString("LastUser") =="")
        {
            loginToggle.isOn = false;
            Debug.Log("ES MI PRIMERITO DIA");
        }
        else
        {
            name.text = PlayerPrefs.GetString("LastUser");
            Debug.Log("NO SOY NUEVO" + playerInstance.GetUserVariable("AutomaticLogIn"));
            loginToggle.isOn = (playerInstance.GetUserVariable("AutomaticLogIn") == "1") ? true : false;
            Debug.Log("ENTRAR? "+loginToggle.isOn);
        }

        
		if(loginToggle.isOn)
        {
            Debug.Log("ENTRA AUTOMATICO");
            playerInstance.OnLoggInAutomatico();
            LevelLoad();
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveUserName(){
            string username = name.text;
            playerInstance.SetName(username);
            if(loginToggle.isOn) PlayerPrefs.SetString(username+".AutomaticLogIn",""+1);
            playerInstance.SetLastName(username);

            StartCoroutine(SubirNombre());
    }

    IEnumerator SubirNombre()
    {
        // WAIT
        Subida.SetTrigger("Levantar");
        yield return new WaitForSeconds(1f);
        // Subida.SetTrigger("Levantar");
        LevelLoad();

    }

    public void LevelLoad()
    {
        SceneManager.LoadScene("Lobby");
    }
}
