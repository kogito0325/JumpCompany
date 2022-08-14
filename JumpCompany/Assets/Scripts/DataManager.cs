using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int playerIndex;
    public int[] scores;
    public float bgmVolume;
    public float soundVolume;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        Initialize();
    }

    private void Start()
    {
        
    }

    private void Initialize()
    {
        scores = new int[] 
        {
            PlayerPrefs.GetInt("Lee", 0), 
            PlayerPrefs.GetInt("Ha", 0), 
            PlayerPrefs.GetInt("Jo", 0), 
            PlayerPrefs.GetInt("Nam", 0), 
            PlayerPrefs.GetInt("Son", 0)
        };

        bgmVolume = PlayerPrefs.GetFloat("BGM");
        soundVolume = PlayerPrefs.GetFloat("SE");
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Lee", scores[0]);
        PlayerPrefs.SetInt("Ha", scores[1]);
        PlayerPrefs.SetInt("Jo", scores[2]);
        PlayerPrefs.SetInt("Nam", scores[3]);
        PlayerPrefs.SetInt("Son", scores[4]);

        PlayerPrefs.SetFloat("BGM", bgmVolume);
        PlayerPrefs.SetFloat("SE", soundVolume);

        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
