using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartLevel()
    {

        Debug.Log("StartingLevel");
        animator.SetTrigger("startLevel");
    }

    public void ShowShopAnim()
    {

        Debug.Log("ShopOpened");

        animator.SetTrigger("showShop");
    }

    public void CloseShopAnim()
    {
        Debug.Log("ShopClosed");
        animator.SetTrigger("closeShop");
    }

    public void ShowLevelsAnim()
    {

        Debug.Log("LevelsOpened");
        animator.SetTrigger("showLevels");
    }

    public void CloseLevelsAnim()
    {
        Debug.Log("Levelslosed");
        animator.SetTrigger("closeLevels");
    }
}
