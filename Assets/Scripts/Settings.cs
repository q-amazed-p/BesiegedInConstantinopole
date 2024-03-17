using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public AudioSource audioSource;

    [Range(0, 1)]
    public float MasterVolume;

    [Range(0, 1)]
    public float MusicVolume;

    [Range(0, 1)]
    public float SfxVolume;

    void OnValidate() 
    {
        audioSource.volume = MasterVolume * MusicVolume;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += ConnectSettings;
    }

    void ConnectSettings(Scene _a, LoadSceneMode _b)
    {
        audioSource = FindAnyObjectByType<AudioSource>();
        audioSource.volume = MasterVolume * MusicVolume;
    }

}
