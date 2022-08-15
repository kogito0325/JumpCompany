using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    public GameObject clearScreen;
    public Soundmanager soundManager;

    public AudioClip clear;
    public AudioClip click;

    private void Awake()
    {
        
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("clear") == 1)
        {
            clearScreen.SetActive(true);
        }
        PlayerPrefs.SetInt("clear", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
