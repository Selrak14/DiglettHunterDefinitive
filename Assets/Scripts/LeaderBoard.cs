using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoard : MonoBehaviour
{
    
    public GameObject rowPrefab;
    // public Transform rowsParent;


    // public List<GameObject> rowPrefab = new List<GameObject>();
    public List<Transform> rowsParent = new List<Transform>();
    public List<GameObject> _Tablas = new List<GameObject>();
    // public Transform rowsParent;

    GameDataManager DataManagerInstance;
    // public GameObject tablaClassic;
    
    // Start is called before the first frame update
    void Start()
    {
        // OnLoadLeaderboard();
        DataManagerInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataManager>();
        DataManagerInstance.LeaderBoardShow(rowPrefab,rowsParent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoadLeaderboard()
    {

    }

    void HideAll(){
        _Tablas[0].SetActive(false);
        _Tablas[1].SetActive(false);
        _Tablas[2].SetActive(false);
    }

    public void SetLeaderboardVisibility(string leaderboardName)
    {
        HideAll();
        Debug.Log("MOSTRAR TABLA");
        if(leaderboardName == "Classic")_Tablas[0].SetActive(true);
        if(leaderboardName == "Reloj")_Tablas[1].SetActive(true);
        if(leaderboardName == "Batalla")_Tablas[2].SetActive(true);
        
    }
}
