using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class recorderNotif : MonoBehaviour
{
    public RectTransform notif;
    public float duration = 1f;
    public float offset = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 targetPos = notif.anchoredPosition - new Vector2(offset, 0);

        notif.DOAnchorPos(targetPos, 0.5f).SetEase(Ease.OutBack);

        StartCoroutine(WaitToDespawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitToDespawn() {
        yield return new WaitForSeconds(2f);

        notif.DOAnchorPos(notif.anchoredPosition + new Vector2(500, 0), 0.5f).SetEase(Ease.OutBack);
    }
}
