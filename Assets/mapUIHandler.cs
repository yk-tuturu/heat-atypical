using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapUIHandler : MonoBehaviour
{
    public GameObject mapPanel;
    // Start is called before the first frame update
    void Start()
    {
        mapPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMap() {
        mapPanel.SetActive(true);
    }

    public void CloseMap() {
        mapPanel.SetActive(false);
    }

    public void NavigateTo(string location) {
        LocationManager.Instance.ChangeLocation(location);
        mapPanel.SetActive(false);
    }
}
