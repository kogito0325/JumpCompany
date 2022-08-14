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
    public GameObject nowChracter;
    public GameObject crown;

    public static GameManager instance;

    public int floorNumber = 1;
    public int previousScore;
    public int score;
    public int goal;
    public bool goalCut;
    public int floorHeight = 14;
    public int bestScore;

    public void Awake()
    {
        instance = this;

        foreach (var character in characters)
        {
            character.SetActive(false);
        }

        characters[DataManager.instance.playerIndex].SetActive(true);
        nowChracter = characters[DataManager.instance.playerIndex];

        goal = DataManager.instance.playerIndex == 0 ? 5 : DataManager.instance.playerIndex * 10;
        
        bestScore = DataManager.instance.scores[DataManager.instance.playerIndex];

    }

    public void Start()
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
    public void Update()
    {
        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneChange();
        }
        score = (int)nowChracter.transform.position.y / floorHeight + 1;
        CheckNextFloor();

    }

    public void BuildNewFloor()
    {
        floorNumber++;
        if (floorNumber - 1 == goal)
        {
            if (bestScore > goal)
            {
                goalCut = false;
            }
            else
            {
                Instantiate(bossFloor, new Vector3(0, floorHeight * (score + 2), -7), Quaternion.identity);
                goalCut = true;
            }
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

    public void CheckNextFloor()
    {
        if (score > previousScore)
        {
            previousScore = score;

            if (bestScore < score)
                DataManager.instance.scores[DataManager.instance.playerIndex] = score;
            BuildNewFloor();
        }
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        int scoreNum = score;

        foreach (var s in scoreText)
        {
            s.SetActive(false);
        }
        scoreText[0].GetComponent<Image>().sprite = numbers[scoreNum / 100];

        scoreNum = scoreNum - ((scoreNum / 100) * 100);
        scoreText[1].GetComponent<Image>().sprite = numbers[scoreNum / 10];

        scoreNum = scoreNum - ((scoreNum / 10) * 10);
        scoreText[2].GetComponent<Image>().sprite = numbers[scoreNum];

        if (score >= 0)
        {
            scoreText[3].SetActive(true);
            scoreText[2].SetActive(true);
        }
        if (score >= 10)
            scoreText[1].SetActive(true);
        if (score >= 100)
            scoreText[0].SetActive(true);
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(0);
    }
}
