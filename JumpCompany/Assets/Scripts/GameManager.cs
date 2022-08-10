using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject floor;
    public GameObject nowCharacter;
    public Text scoreText;

    public int floorNumber = 1;
    public int previousScore = 0;
    public int score = 0;
    public int floorHeight = 14;

    void Awake()
    {
        foreach (var character in characters)
        {
            character.SetActive(false);
        }

        characters[PlayerPrefs.GetInt("character")].SetActive(true);
        nowCharacter = characters[PlayerPrefs.GetInt("character")];

    }
    // Start is called before the first frame update
    void Start()
    {
        previousScore = 0;
        score = 0;
        UpdateScoreText();
        for (int i = 1; i <= 4; i++)
        {
            floorNumber++;
            Instantiate(floor, new Vector3(0, floorHeight * (score + i), -7), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // °ÔÀÓ Á¾·á
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        CheckNextFloor();
    }

    void BuildNewFloor()
    {
        floorNumber++;
        if ((floorNumber - 1) % 5 == 0)
        {
            floor.GetComponent<FloorScript>().floor.SetActive(true);
        }
        Instantiate(floor, new Vector3(0, floorHeight * (score + 4), -7), Quaternion.identity);
        floor.GetComponent<FloorScript>().floor.SetActive(false);
    }

    void CheckNextFloor()
    {
        if (score > previousScore)
        {
            previousScore = score;
            switch (PlayerPrefs.GetInt("character"))
            {
                case 0:
                    PlayerPrefs.SetInt("chracter_0", score);
                    break;

                case 1:
                    PlayerPrefs.SetInt("chracter_1", score);
                    break;

                case 2:
                    PlayerPrefs.SetInt("chracter_2", score);
                    break;

                case 3:
                    PlayerPrefs.SetInt("chracter_3", score);
                    break;

                case 4:
                    PlayerPrefs.SetInt("chracter_4", score);
                    break;
            }
            BuildNewFloor();
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString() + "Ãþ";
    }
}
