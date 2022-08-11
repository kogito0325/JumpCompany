using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters;
    public Material[] backGrounds;
    public GameObject floor;
    public GameObject bossFloor;
    public GameObject[] scoreText;
    public Sprite[] numbers;

    public static GameManager instance;

    public int floorNumber = 1;
    public int previousScore;
    public int score;
    public int goal;
    public bool goalCut;
    public int floorHeight = 14;

    void Awake()
    {
        instance = this;

        foreach (var character in characters)
        {
            character.SetActive(false);
        }

        characters[PlayerPrefs.GetInt("character")].SetActive(true);
        goal = PlayerPrefs.GetInt("character") == 0 ? 5 : PlayerPrefs.GetInt("character") * 10;

    }

    void Start()
    {
        previousScore = 0;
        score = 0;
        goalCut = false;
        UpdateScoreText();
        for (int i = 1; i <= 2; i++)
        {
            floorNumber++;
            foreach (var wall in floor.GetComponent<FloorScript>().walls)
            {
                wall.SetActive(false);
            }
            floor.GetComponent<FloorScript>().walls[(floorNumber - 1) % 5].SetActive(true);
            Instantiate(floor, new Vector3(0, floorHeight * (score + i), -7), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        CheckNextFloor();
    }

    void BuildNewFloor()
    {
        floorNumber++;
        if (floorNumber - 1 == goal)
        {
            goalCut = true;
            Instantiate(bossFloor, new Vector3(0, floorHeight * (score + 2), -7), Quaternion.identity);
        }

        if (!goalCut)
        {
            if ((floorNumber - 1) % 5 == 0)
            {
                floor.GetComponent<FloorScript>().floor.SetActive(true);
            }
            foreach (var wall in floor.GetComponent<FloorScript>().walls)
            {
                wall.SetActive(false);
            }
            floor.GetComponent<FloorScript>().walls[(floorNumber - 1) % 5].SetActive(true);
            Instantiate(floor, new Vector3(0, floorHeight * (score + 2), -7), Quaternion.identity);
            floor.GetComponent<FloorScript>().floor.SetActive(false);
        }
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
            Debug.Log(PlayerPrefs.GetInt("chracter_" + PlayerPrefs.GetInt("character").ToString()));
            BuildNewFloor();
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        int scoreNum = score;
        if (score >= goal)
            scoreNum = goal;

        foreach (var s in scoreText)
        {
            s.SetActive(false);
        }
        scoreText[0].GetComponent<Image>().sprite = numbers[scoreNum / 100];
        scoreText[1].GetComponent<Image>().sprite = numbers[(scoreNum - scoreNum / 100 * 100) / 10];
        scoreText[2].GetComponent<Image>().sprite = numbers[scoreNum - (scoreNum - scoreNum / 100 * 100) / 10 * 10];

        if (scoreNum >= 0)
        {
            scoreText[3].SetActive(true);
            scoreText[2].SetActive(true);
        }
        if (scoreNum >= 10)
            scoreText[1].SetActive(true);
        if (scoreNum >= 100)
            scoreText[0].SetActive(true);
    }
}
