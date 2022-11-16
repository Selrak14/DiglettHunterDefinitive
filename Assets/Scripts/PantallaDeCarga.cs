using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDeCarga : MonoBehaviour
{
    public float transitionTime = 1f;
    public Animator transition;
    private int actualScene;
    public AudioSource CamionCrash;

    // Start is called before the first frame update
    void Start()
    {
        actualScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(PasarASiguientePantalla());
        Debug.Log(actualScene);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) & actualScene == 0)
        {
            LoadNextLevel();
        } 
    }

    public void PlayCamion(AudioSource Camion)
    {
        Camion.Play();
    }
    public void PlayCamionCrash(AudioSource Audio)
    {
        Audio.Play();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        
        // transition
        transition.SetTrigger("Start");

        // WAIT
        yield return new WaitForSeconds(.1f);

        SceneManager.LoadScene(levelIndex);

    }

    IEnumerator PasarASiguientePantalla()
    {
        Debug.Log("NO SE ACTIVA EL DESNEGRO");
        
        // IMAGEN DEL CAMION 
        yield return new WaitForSeconds(5f);
        // FUNDIODO A NEGRO
        transition.SetTrigger("Start");

        // CUANDO NEGRO CRASH
        yield return new WaitForSeconds(2.5f);
        PlayCamionCrash(CamionCrash);

        // CARGAR NUEVO MUNDO
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LoggIn");
    }
}
