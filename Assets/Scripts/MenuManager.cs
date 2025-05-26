using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    public enum MenuType {
        Dialogue,
        Recorder,
        Map,
        None
    }

    public MenuType currentMenu = MenuType.None;

    public event Action openDialogueMenu;
    public event Action openRecorderMenu;
    public event Action openMap;
    public event Action closeRecorderMenu;
    public event Action closeDialogueMenu;
    public event Action closeMap;

    public void OpenRecorderMenu() {
        openRecorderMenu?.Invoke();
        currentMenu = MenuType.Recorder;
    }

    public void OpenDialogueMenu() {
        openDialogueMenu?.Invoke();
        currentMenu = MenuType.Dialogue;
    }

    public void OpenMap() {
        openMap?.Invoke();
        currentMenu = MenuType.Map;
    }

    public void CloseRecorderMenu() {
        closeRecorderMenu?.Invoke();
        if (DialogueManager.Instance.currentlyInDialogue) {
            currentMenu = MenuType.Dialogue;
        } else {
            currentMenu = MenuType.None;
        }
    }

    public void CloseDialogueMenu() {
        closeDialogueMenu?.Invoke();
        currentMenu = MenuType.None;
    }

    public void CloseMap() {
        closeMap?.Invoke();
        currentMenu = MenuType.None;
    }

}
