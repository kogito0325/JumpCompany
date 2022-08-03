using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject floor;
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
        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        // 디버그
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
            UpdateScoreText();
            previousScore = score;
            BuildNewFloor();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString() + "층";
    }
}
