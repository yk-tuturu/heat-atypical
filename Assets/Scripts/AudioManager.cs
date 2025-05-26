using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public List<AudioClip> sfxList;
    public List<AudioClip> bgmList;

    public Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
            
        else {
            Destroy(gameObject);
        }
            
        
        foreach (var clip in sfxList) {
            soundDict[clip.name] = clip;
        }

        foreach (var clip in bgmList) {
            soundDict[clip.name] = clip;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySFX(string name) {
        if (!soundDict.ContainsKey(name)) {
            Debug.Log("audio clip not found!");
            return;
        }

        sfxSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        sfxSource.PlayOneShot(soundDict[name]);
    }

    public void PlayBGM(string name) {
        if (!soundDict.ContainsKey(name)) {
            Debug.Log("audio clip not found!");
            return;
        }

        bgmSource.clip = soundDict[name];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SetBGMVolume(float vol) {
        bgmSource.volume = vol;
    }
}
