using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;
    GameDataManager playerInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        // OnLoadLeaderboard();
        playerInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataManager>();
        playerInstance.LeaderBoardShow(rowPrefab,rowsParent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoadLeaderboard()
    {
        // foreach (var item in NoSe)
        // {
        //     GameObject newGo = Instantiate(rowPrefab,rowsParent);
        //     Text[] texts = newGo.GetComponentsInChildren<Text>();
        //     texts[0].text = item.Position.ToString();
        //     texts[1].text;
        // }


        for(int i = 0; i < 5; i++)
        {
            GameObject newGo = Instantiate(rowPrefab,rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = "item.Position.ToString();";
            texts[1].text = "CArA";
        }
    }
}
