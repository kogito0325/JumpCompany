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

    public AudioSource audioSource;
    public AudioSource settingAudio;

    public Sprite[] tutorials;
    public GameObject tutorialScreen;
    public CanvasGroup settingScreen; 

    public static GameManager instance;

    public int floorNumber = 1;
    public int previousScore;
    public int score;
    public int goal;
    public bool goalCut;
    public int floorHeight = 14;
    public int bestScore;
    public int tutorialIndex;
    public bool tutorialExist;

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

        audioSource.volume = DataManager.instance.bgmVolume;
        settingAudio.volume = DataManager.instance.soundVolume;

    }

    public void Start()
    {
        previousScore = 0;
        score = 0;
        goalCut = false;
        tutorialIndex = 0;
        tutorialExist = DataManager.instance.scores[0] <= 5 ? true : false;
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
        // 튜토리얼
        if (DataManager.instance.scores[0] <= 5)
            Tutorial();
        
        // ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingScreen.interactable == false)
            {
                settingScreen.alpha = 1;
                settingScreen.blocksRaycasts = true;
                settingScreen.interactable = true;
            }
            else
            {
                settingScreen.alpha = 0;
                settingScreen.blocksRaycasts = false;
                settingScreen.interactable = false;
            }
        }

        // 점수계산
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

    public void Tutorial()
    {
        if (tutorialExist)
        {
            tutorialScreen.SetActive(true);
            Image tutorialScreenImage = tutorialScreen.GetComponent<Image>();
            tutorialScreenImage.sprite = tutorials[tutorialIndex];
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                tutorialIndex++;
                if (tutorialIndex >= tutorials.Length)
                {
                    tutorialIndex = tutorials.Length - 1;
                    tutorialScreen.SetActive(false);
                    tutorialExist = false;
                }
            }
        }
    }

    public void SetBGMVolume(float volume)
    {
        DataManager.instance.bgmVolume = volume;
        audioSource.volume = volume;
    }

    public void SetSEVolume(float volume)
    {
        DataManager.instance.soundVolume = volume;
        settingAudio.volume = volume;
        nowChracter.GetComponent<AudioSource>().volume = volume;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(0);
    }
}
