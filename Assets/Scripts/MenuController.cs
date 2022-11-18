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
    public GameObject MapsMenu;
    public GameObject PointersMenu;
    public TextMeshProUGUI WelcomeText;
    public Sprite[] sprites;

    private int currentScene;
    CameraMovement camara;


    private void Start()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(currentScene);
        if (currentScene == 2) { 
            camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            WelcomePlayer();
        }
    }

    void Update()
    {

        //Key Detection in Lobby
        if (currentScene == 2 && Input.GetKeyDown(KeyCode.Escape))
        {
            string current_animation = camara.CurrentAnim();
            //Debug.Log(current_animation);
            if (current_animation == "idleCamera")
                camara.ShowOptionsAnim();
            else if (current_animation == "idleOptions")
                camara.CloseOptionsAnim();
        }

        if (currentScene == 2 && (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            string current_animation = camara.CurrentAnim();
            //Debug.Log(current_animation);
            if (current_animation == "idleCamera")
                camara.ShowOptionsAnim();
            else if (current_animation == "idleCustom")
                camara.CloseCustomAnim();
        }

        if (currentScene == 2 && (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            string current_animation = camara.CurrentAnim();
            //Debug.Log(current_animation);
            if (current_animation == "idleCamera")
                camara.ShowCustomAnim();
            else if (current_animation == "idleOptions")
                camara.CloseOptionsAnim();
        }

        if (currentScene == 2 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            string current_animation = camara.CurrentAnim();
            //Debug.Log(current_animation);
            if (current_animation == "idleCamera")
                camara.ShowShopAnim();
            else if (current_animation == "idleLevels")
                camara.CloseLevelsAnim();
        }

        if (currentScene == 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            string current_animation = camara.CurrentAnim();
            //Debug.Log("1"+current_animation);
            if (current_animation == "idleCamera")
                camara.ShowLevelsAnim();
            else if (current_animation == "idleShop")
                camara.CloseShopAnim();
        }
    }

    //Welcome Text
    public void WelcomePlayer()
    {
        string username = PlayerPrefs.GetString("username");
        char firstLetter;
        if (username[0].Equals(" "))
            firstLetter = username[1];
        else
            firstLetter = username[0];
        WelcomeText.SetText("<color=black>Welcome</color=black> <b>" +  char.ToUpper(firstLetter) + "</b>");
    }

    //Quit Game
    public void CloseGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();

    }

    //Gamemodes
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


    public void RandomGame()
    {
        int scene = currentScene + Random.Range(0, 2) + 1;
        Debug.Log("Scena a la que se mueve: " + scene);
        SceneManager.LoadScene(scene);
    }

    //Animations Shop and Modes(Levels)

    public void ShowShop()
    {

        Debug.Log("Shop Opened");
        camara.ShowShopAnim();
    }

    public void CloseShop()
    {
        Debug.Log("Shop Closed");
        camara.CloseShopAnim();
    }

    public void ShowLevels()
    {

        Debug.Log("Levels Opened");
        camara.ShowLevelsAnim();
    }

    public void CloseLevels()
    {
        Debug.Log("Levels Closed");
        camara.CloseLevelsAnim();
    }

    public void ShowOptions()
    {

        Debug.Log("Options Opened");
        camara.ShowOptionsAnim();
    }

    public void CloseOptions()
    {
        Debug.Log("Options Closed");
        camara.CloseOptionsAnim();
    }

    //Customization Buttons
    public void ShowMaps()
    {
        MapsMenu.SetActive(true);
        PointersMenu.SetActive(false);
    }

    public void ShowPointers()
    {
        PointersMenu.SetActive(true);
        MapsMenu.SetActive(false);
    }

    //Pointers
    public void ChangePointer(int number)
    {
        string imagePointer = GameObject.Find($"/Customization/PointerSelection/Pointer{number}/Image").GetComponent<Image>().sprite.ToString().Split(' ')[0];
        Debug.Log(imagePointer);
        //PlayerPrefs.SetString("pointer", imagePointer);

        //Change Pointer

    }

    // Map Changer

    public void ChangeMap(int Map)
    {
        GameObject mapButton = GameObject.Find($"/Customization/MapSelection/Map{Map}/Sprite");
        mapButton.SetActive(true);
        Debug.Log("Map "+Map);
        GameObject.FindGameObjectWithTag("Map").GetComponent<Image>().sprite = sprites[Map];
    }
}
