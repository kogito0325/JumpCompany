using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters;

    void Awake()
    {
        foreach (var character in characters)
        {
            character.SetActive(false);
        }

        characters[PlayerPrefs.GetInt("character")].SetActive(true);

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        // 디버그

    }
}
