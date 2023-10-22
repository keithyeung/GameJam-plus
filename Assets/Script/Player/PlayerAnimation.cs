using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnimator;

    public GameObject normalSprite;
    public GameObject climbingSprite;


    private void Start()
    {
        SwitchingSpritesToNormal();
    }

    public void WalkingAni()
    {
        playerAnimator.SetBool("isRunning",true);
    }

    public void PickUpAni()
    {
        playerAnimator.SetBool("isPickingUp", true);
    }

    public void RestartAni()
    {
        playerAnimator.SetBool("RESTART", true);
    }

    public void ThrowAni()
    {
        playerAnimator.SetBool("isThrowing",true);
    }

    public void Idle()
    {
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isPickingUp", false);
        playerAnimator.SetBool("RESTART", false);
        playerAnimator.SetBool("isThrowing", false);
    }

    public void SwitchingSpritesToNormal()
    {
        climbingSprite.SetActive(false);
        normalSprite.SetActive(true);
    }
    public void SwitchingSpritesToClimbing()
    {
        normalSprite.SetActive(false);
        climbingSprite.SetActive(true);
    }
}
