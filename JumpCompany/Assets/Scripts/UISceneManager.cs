using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    public GameObject clearScreen;

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
        
    }
}
