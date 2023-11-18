using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfomButton : Interactable
{
    [SerializeField] private Platform _platform;
    public override void Interact()
    {
        FindAnyObjectByType<PlayerMovement>().interactTutorial = true;
        _platform.StartMoving();
    }
}
