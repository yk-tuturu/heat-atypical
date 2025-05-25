using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public string location;
    public GameObject alertIndicator;
    public GameObject doneIndicator;
    // Start is called before the first frame update
    void Start()
    {
        alertIndicator.SetActive(false);
        doneIndicator.SetActive(false);

        UpdateIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateIndicator() {
        if (location == "lab") {
            Debug.Log(LocationManager.Instance.CheckLocationCompleted(location));
        }
        if (LocationManager.Instance.CheckLocationCompleted(location)) {
            doneIndicator.SetActive(true);
            alertIndicator.SetActive(false);
        } else {
            alertIndicator.SetActive(true);
            doneIndicator.SetActive(false);
        }
    }
}
