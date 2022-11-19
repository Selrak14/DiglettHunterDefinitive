using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Math;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Debug variables
    private GameObject DebugObject;
    private TheGame playerInstance;

    public GameObject ShopMenu;
    public GameObject LevelsMenu;
    public GameObject MapsMenu;
    public GameObject PointersMenu;
    public GameObject ConfirmationPopup;
    public GameObject Extramoney;
    public GameObject Warning;
    public TextMeshProUGUI WelcomeText;
    CursorMode cursorMode;

    private int currentScene;
    CameraMovement camara;


    private void Start()
    {
        // COMPROBAR QUE ESTAS JUGANDO DE VERDAD I NO DESDE EL EDITOR
        if(GameObject.FindGameObjectsWithTag("GameController").Length == 0)
        {
            Debug.Log("NO HAY THE GAME");
            // someObject doesn't exist
            DebugObject = new GameObject("TheGameGeneradoPorCodigo");
            DebugObject.AddComponent<TheGame>();
            playerInstance = DebugObject.GetComponent<TheGame>();  
        }
        else
        {
            Debug.Log("REUTILIZAR THE GAME");
            playerInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheGame>();
        }
        // // //

        currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(currentScene);
        if (currentScene == 2) { 
            camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            WelcomePlayer();
            MoneyCountText();
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
        string username = playerInstance._GameData.gameData._username;
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

    //Shop
    // + Button
    public void ExtraMoney()
    {
        StartCoroutine(PopUpClick(Extramoney));
    }


    //Update text with money count
    public float CurrentMoney()
    {
        Debug.Log("Obtaining Money...");
        return  playerInstance._GameData.gameData._dineroP;
    }
    public void MoneyCountText()
    {
        Debug.Log("hOLA");
        TextMeshProUGUI Content = GameObject.Find($"/Shop/Money/MoneyCount").GetComponent<TextMeshProUGUI>();
        float currentMoney = CurrentMoney();

        Debug.Log("Current Money" + currentMoney);
        Content.SetText($"{Round(currentMoney,2)}");
    }


    //Customization Buttons
    public void ShowMaps()
    {
        GameObject.Find("/Customization/Buttons/MapButton").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/selector");
        GameObject.Find("/Customization/Buttons/PointerButton").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/selectordesactivado");
        MapsMenu.SetActive(true);
        PointersMenu.SetActive(false);
    }

    public void ShowPointers()
    {
        GameObject.Find("/Customization/Buttons/PointerButton").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/selector");
        GameObject.Find("/Customization/Buttons/MapButton").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/selectordesactivado");
        PointersMenu.SetActive(true);
        MapsMenu.SetActive(false);
    }


    //LogOut Button
    public void LogOut()
    {
        PlayerPrefs.SetInt("AutoLogIn", 0);
        PlayerPrefs.Save();
        LoadGame("LoggIn");
    }

    //Cursor
    public void ChangePointer(int number)
    {
        bool isLocked = true;

        if ((number == 3 && isLocked) || (number == 1 && isLocked))
        {
            StartCoroutine(PopUpClick(Warning, "Buy it at the store"));
        }
        else if (number == 2)
        {
            StartCoroutine(PopUpClick(Warning, "Available in the next update!!"));
        }
        else {

        //Save pointer
        string imagePointer = GameObject.Find($"/Customization/PointerSelectionWindow/Buttons/Pointer{number}/Image").GetComponent<Image>().sprite.ToString().Split(' ')[0];
        Debug.Log(imagePointer);

        if (imagePointer != playerInstance._GameData.gameData._PointerCustom) { 

            //save value
            playerInstance._GameData.gameData._PointerCustom = imagePointer;
            playerInstance._GameData.writeFile(playerInstance._GameData.gameData._username, playerInstance._GameData.gameData);

            //change cursor
            Texture2D cursorTexture = Resources.Load<Texture2D>($"Pointer/{imagePointer}");
            cursorMode = CursorMode.Auto;
            Vector2 hotSpot = Vector2.zero;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

            //show popup
            StartCoroutine(PopUpClick(ConfirmationPopup));

            }
            else
            {
                Debug.Log("Cursor already in use");
            }
        }
    }

    // Map Changer


    IEnumerator PopUpClick(GameObject PopUp, string text=null)
    {
        PopUp.SetActive(true);
        if (text != null)
            PopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText($"{text}");
        yield return new WaitForSeconds(1);
        PopUp.SetActive(false);
    }
    public void ChangeMap(int Map)
    {

        bool isLocked = true;

        if ((Map == 3 && isLocked) || (Map == 4 && isLocked))
        {
            StartCoroutine(PopUpClick(Warning, "Buy it at the store"));
        }
        else
        {
            StartCoroutine(PopUpClick(ConfirmationPopup));

            Debug.Log("Map " + Map);
            Sprite imageSelection = GameObject.Find($"/Customization/MapSelectionWindow/Buttons/Map{Map}").GetComponent<Image>().sprite;
            GameObject.FindGameObjectWithTag("Map").GetComponent<Image>().sprite = imageSelection;

            // Guardar Nombre en preferencias
            string imageMap = imageSelection.ToString().Split(' ')[0];
            playerInstance._GameData.gameData._MapaSkinCustom = imageMap;
            playerInstance._GameData.writeFile(playerInstance._GameData.gameData._username, playerInstance._GameData.gameData);
        }
    }
}
