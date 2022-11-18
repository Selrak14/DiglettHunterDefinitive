using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapChanger : MonoBehaviour, IPointerEnterHandler
{
    public MenuController MenuController;
    public int Map;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Map").GetComponent<Image>().sprite = MenuController.sprites[Map];
    }
}
