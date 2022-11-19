using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Math;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    BackGroundMovement BackGround;


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
            BackGround = GameObject.FindGameObjectWithTag("BackGround").GetComponent<BackGroundMovement>();

            WelcomePlayer();
            MoneyCountText();
            SoldOutCheck();
        }
    }

    void Update()
    {

        //Key Detection in Lobby
        if (currentScene == 2 && Input.GetKeyDown(KeyCode.Escape))
        {
            string current_animation = camara.CurrentAnim();
            if (current_animation == "idleCamera"){
                camara.ShowOptionsAnim();
                BackGround.ShowOptionsAnim();}
            else if (current_animation == "idleOptions"){
                camara.CloseOptionsAnim();
                BackGround.CloseOptionsAnim();}
        }

        if (currentScene == 2 && (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            string current_animation = camara.CurrentAnim();
            if (current_animation == "idleCamera"){
                camara.ShowOptionsAnim();
                BackGround.ShowOptionsAnim();}
            else if (current_animation == "idleCustom"){
                camara.CloseCustomAnim();
                BackGround.CloseCustomAnim();}
        }

        if (currentScene == 2 && (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            string current_animation = camara.CurrentAnim();
            if (current_animation == "idleCamera"){
                camara.ShowCustomAnim();
                BackGround.ShowCustomAnim();}
            else if (current_animation == "idleOptions"){
                camara.CloseOptionsAnim();
                BackGround.CloseOptionsAnim();}
        }

        if (currentScene == 2 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            string current_animation = camara.CurrentAnim();
            if (current_animation == "idleCamera"){
                camara.ShowShopAnim();
                BackGround.ShowShopAnim();}
            else if (current_animation == "idleLevels"){
                camara.CloseLevelsAnim();
                BackGround.CloseLevelsAnim();}
        }

        if (currentScene == 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            string current_animation = camara.CurrentAnim();
            //Debug.Log("1"+current_animation);
            if (current_animation == "idleCamera"){
                camara.ShowLevelsAnim();
                BackGround.ShowLevelsAnim();}
            else if (current_animation == "idleShop"){
                camara.CloseShopAnim();
                BackGround.CloseShopAnim();}
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
        BackGround.StartLevel();
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
        BackGround.ShowShopAnim();

    }

    public void CloseShop()
    {
        Debug.Log("Shop Closed");
        camara.CloseShopAnim();
        BackGround.CloseShopAnim();
    }

    public void ShowLevels()
    {

        Debug.Log("Levels Opened");
        camara.ShowLevelsAnim();
        BackGround.ShowLevelsAnim();
    }

    public void CloseLevels()
    {
        Debug.Log("Levels Closed");
        camara.CloseLevelsAnim();
        BackGround.CloseLevelsAnim();
    }

    public void ShowOptions()
    {

        Debug.Log("Options Opened");
        camara.ShowOptionsAnim();
        BackGround.ShowOptionsAnim();
    }

    public void CloseOptions()
    {
        Debug.Log("Options Closed");
        camara.CloseOptionsAnim();
        BackGround.CloseOptionsAnim();
    }

    //Shop
    // + Button
    public void ExtraMoney()
    {
        StartCoroutine(PopUpClick(Extramoney, "It's Free to Play ... at least for now."));
        playerInstance._GameData.gameData._dineroP += 1000000;
        playerInstance._GameData.writeFile(playerInstance._GameData.gameData._username, playerInstance._GameData.gameData);
        MoneyCountText();
    }


    //Update text with money count
    public float CurrentMoney()
    {
        Debug.Log("Obtaining Money...");
        return  playerInstance._GameData.gameData._dineroP;
    }
    public void MoneyCountText()
    {
        TextMeshProUGUI Content = GameObject.Find($"/Shop/Money/MoneyCount").GetComponent<TextMeshProUGUI>();
        float currentMoney = CurrentMoney();

        Debug.Log("Current Money" + currentMoney);
        Content.SetText($"{Round(currentMoney,2)}");
    }

    //SoldOutCheck
    public void SoldOutCheck()
    {

        //go to Objects in Power Ups
        GameObject PowerUpsObjects = ShopMenu.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject;

        //go item by item

        if (playerInstance._GameData.gameData._NivelTorreta == 1)
        {
            PowerUpsObjects.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };
        if (playerInstance._GameData.gameData._NivelTorreta == 2)
        {
            PowerUpsObjects.gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };
        if (playerInstance._GameData.gameData._NivelTorreta == 3)
        {
            PowerUpsObjects.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };

        //go to Objects in Cursors
        GameObject CursorsObjects = ShopMenu.transform.GetChild(4).gameObject.transform.GetChild(4).gameObject;

        //go item by item
        if (playerInstance._GameData.gameData._pointer1 == true)
        {
            CursorsObjects.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };
        if (playerInstance._GameData.gameData._pointer2 == true)
        {
            CursorsObjects.gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };
        if (playerInstance._GameData.gameData._Map3 == true)
        {
            CursorsObjects.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };
        if (playerInstance._GameData.gameData._Map4 == true)
        {
            CursorsObjects.gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
        };


        GameObject CursorButtons = PointersMenu.transform.GetChild(1).gameObject;
        if (playerInstance._GameData.gameData._pointer1 == true)
        {
            CursorButtons.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        };
        if (playerInstance._GameData.gameData._pointer2 == true)
        {
            CursorButtons.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        };

        Debug.Log("Esta guardado2? "+playerInstance._GameData.gameData._Map3);
        GameObject MapButtons = MapsMenu.transform.GetChild(4).gameObject;
        Debug.Log("Esta guardado? "+playerInstance._GameData.gameData._Map3);
        if (playerInstance._GameData.gameData._Map3 == true)
        {
            MapButtons.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        };
        if (playerInstance._GameData.gameData._Map4 == true)
        {
            MapButtons.gameObject.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        };

    }


    //Buy item
    public void BuyItem()
    {
        GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
        string selectedGameName = selectedGameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
        string selectedGameObjectText = selectedGameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;

        if (selectedGameObjectText != "Sold Out") {
            float itemCost = float.Parse(selectedGameObjectText);
            Debug.Log(itemCost);

            if (playerInstance._GameData.gameData._dineroP < itemCost)
            {
                StartCoroutine(PopUpClick(Extramoney, "You are too poor to buy it"));
            }
            else
            {
                float moneyLeft = playerInstance._GameData.gameData._dineroP - itemCost;

                //Update Data
                playerInstance._GameData.gameData._dineroP = moneyLeft;
                playerInstance._GameData.writeFile(playerInstance._GameData.gameData._username, playerInstance._GameData.gameData);


                if (selectedGameName == "CLASSIC")
                {
                    playerInstance._GameData.gameData._pointer1 = true;
                    PointersMenu.transform.GetChild(1).gameObject.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

                };
                if (selectedGameName == "LIGHTSABER")
                {
                    playerInstance._GameData.gameData._pointer2 = true;
                    PointersMenu.transform.GetChild(1).gameObject.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                };
                if (selectedGameName == "DIRT MAP")
                {
                    playerInstance._GameData.gameData._Map3 = true;
                    MapsMenu.transform.GetChild(4).gameObject.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                };
                if (selectedGameName == "COLOSSEUM MAP")
                {
                    playerInstance._GameData.gameData._Map4 = true;
                    MapsMenu.transform.GetChild(4).gameObject.gameObject.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                };



                if (selectedGameName == "Torreta Nv.1")
                {
                    playerInstance._GameData.gameData._NivelTorreta = 1;
                };
                if (selectedGameName == "Torreta Nv.2")
                {
                    playerInstance._GameData.gameData._NivelTorreta = 2;
                };
                if (selectedGameName == "Torreta Nv.3")
                {
                    playerInstance._GameData.gameData._NivelTorreta = 3;
                };
                playerInstance._GameData.writeFile(playerInstance._GameData.gameData._username, playerInstance._GameData.gameData);



                selectedGameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Sold Out";
                MoneyCountText();
            };
        }
        else
        {
            StartCoroutine(PopUpClick(Extramoney, "You already own this item"));
        }
    }




    //Customization Buttons
    public void ShowMaps()
    {
        GameObject.Find("/Customization/Buttons/MapButton").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/selector");
        GameObject.Find("/Customization/Buttons/PointerButton").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/selectordesactivado");
        MapsMenu.SetActive(true);
        PointersMenu.SetActive(false);
        if (playerInstance._GameData.gameData._Map3 == true)
        {
            MapsMenu.transform.GetChild(4).gameObject.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        };
        if (playerInstance._GameData.gameData._Map4 == true)
        {
            MapsMenu.transform.GetChild(4).gameObject.gameObject.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        };
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

        if ((number == 3 && !playerInstance._GameData.gameData._pointer2) || (number == 1 && !playerInstance._GameData.gameData._pointer1))
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

        if ((Map == 3 && !playerInstance._GameData.gameData._Map3) || (Map == 4 && !playerInstance._GameData.gameData._Map4))
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
