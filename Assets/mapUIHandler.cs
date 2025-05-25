using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapUIHandler : MonoBehaviour
{
    public GameObject mapPanel;
    public List<MapButton> buttons;

    public GameObject arrowToHall;
    public GameObject arrowToStore;
    public GameObject arrowToWastes;

    private int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        mapPanel.SetActive(false);
    }

    public void OpenMap() {
        mapPanel.SetActive(true);

        foreach (var button in buttons) {
            button.UpdateIndicator();
        }

        CheckLevel();

        arrowToHall.SetActive(level >= 1);
        arrowToStore.SetActive(level >= 1);
        arrowToWastes.SetActive(level >= 2);
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(level >= 1);
        buttons[2].gameObject.SetActive(level >= 1);
        buttons[3].gameObject.SetActive(level >= 2);
    }

    public void CloseMap() {
        mapPanel.SetActive(false);
    }

    public void NavigateTo(string location) {
        LocationManager.Instance.ChangeLocation(location);
        mapPanel.SetActive(false);
    }

    public void CheckLevel() {
        if (LocationManager.Instance.CheckLocationCompleted("lab")) {
            level = 1;

            if (LocationManager.Instance.CheckLocationCompleted("hall") && LocationManager.Instance.CheckLocationCompleted("store")) {
                level = 2;
            }
        } else {
            level = 0;
        }
    }
}
