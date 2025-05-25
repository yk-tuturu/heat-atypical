using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpritePair
{
    public string key;
    public GameObject sprite;
}

public class DialogueSpriteManager : MonoBehaviour
{
    public List<SpritePair> spriteList;

    private Dictionary<string, GameObject> spriteDict = new Dictionary<string, GameObject>();

    private string activeSpriteName;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spritePair in spriteList) {
            spriteDict[spritePair.key] = spritePair.sprite;
            spritePair.sprite.SetActive(false);
        }
    }

    public void ShowSprite(string name) {
        if (name == activeSpriteName) return;
        if (spriteDict[name] == null) {
            Debug.Log("sprite not found!");
            return;
        } 

        if (activeSpriteName != null) {
            spriteDict[activeSpriteName]?.SetActive(false);
        }
        
        spriteDict[name]?.SetActive(true);
        activeSpriteName = name;
    }

    public void HideAllSprites() {
        foreach (GameObject sprite in spriteDict.Values) {
            sprite.SetActive(false);
        }
        activeSpriteName = "";
    }
}
