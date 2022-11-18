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

    // Get current animation being played
    public string CurrentAnim()
    {
        string current_animation = this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return current_animation;
    }

    //Triggers Animations
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

    public void ShowOptionsAnim()
    {

        Debug.Log("OptionsOpened");

        animator.SetTrigger("showOptions");
    }

    public void CloseOptionsAnim()
    {
        Debug.Log("OptionsClosed");
        animator.SetTrigger("closeOptions");
    }

    public void ShowCustomAnim()
    {

        Debug.Log("CustomOpened");

        animator.SetTrigger("showCustom");
    }

    public void CloseCustomAnim()
    {
        Debug.Log("CustomClosed");
        animator.SetTrigger("closeCustom");
    }
}
