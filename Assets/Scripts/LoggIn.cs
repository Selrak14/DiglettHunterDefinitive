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
    
	public void Start()
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
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getValue(){
            string username = name.text;
            playerInstance.SetName(username);
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
