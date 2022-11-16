using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject ShopMenu;
    public GameObject LevelsMenu;

    private int currentScene;
    CameraMovement camara;


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentScene);
        if (currentScene == 2)
            camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
    }



    public void ExitGame(string level)
    {
        Debug.Log("Nivel al que moverse: "+level);
        SceneManager.LoadScene(level);
    }

    public void LoadGame(string level)
    {
        Debug.Log("Nivel al que moverse: "+level);
        camara.StartLevel();
        SceneManager.LoadScene(level);
    }


    public void ShowShop()
    {

        Debug.Log("Shop Opened");
        camara.ShowShopAnim();
        //ShopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        Debug.Log("Shop Closed");
        camara.CloseShopAnim();
        //ShopMenu.SetActive(false);
    }

    public void ShowLevels()
    {

        Debug.Log("Levels Opened");
        camara.ShowLevelsAnim();
        //LevelsMenu.SetActive(true);
    }

    public void CloseLevels()
    {
        Debug.Log("Levels Closed");
        camara.CloseLevelsAnim();
        //LevelsMenu.SetActive(false);
    }
}
