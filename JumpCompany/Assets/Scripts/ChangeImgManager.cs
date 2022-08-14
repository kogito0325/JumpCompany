using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeImgManager : MonoBehaviour
{
    public Sprite[] recordImages;
    public Sprite[] characters;
    public GameObject[] record;
    public Sprite[] numbers;
    public GameObject selectBtn;

    public int characterIndex = 0;
    public int[] goals;


    public GameObject nowRecord;
    public GameObject nowCharacter;

    public void Start()
    {
        ChangeCharacter();
        goals = new int[] {5, 10, 20, 30, 40};
    }

    public void ChangeCharacter()
    {
        nowRecord.GetComponent<Image>().sprite = recordImages[characterIndex];
        nowCharacter.GetComponent<Image>().sprite = characters[characterIndex];
        if (characterIndex > 0 && DataManager.instance.scores[characterIndex - 1] <= goals[characterIndex - 1])
        {
            nowCharacter.GetComponent<Image>().color = Color.black;
            selectBtn.SetActive(false);
        }
        else
        {
            nowCharacter.GetComponent<Image>().color = Color.white;
            selectBtn.SetActive(true);
        }
        ChangeRecord();
    }

    public void SetNextIndex()
    {
        if (characterIndex + 1 >= characters.Length)
            characterIndex = 0;
        else
            characterIndex += 1;
        ChangeCharacter();
    }

    public void SetPreviousIndex()
    {
        if (characterIndex <= 0)
            characterIndex = characters.Length - 1;
        else
            characterIndex -= 1;
        ChangeCharacter();
    }

    public void ChangeRecord()
    {
        int scoreNum = DataManager.instance.scores[characterIndex];
        if (scoreNum > goals[characterIndex])
        {
            record[0].SetActive(true);
            record[1].SetActive(false);
            record[2].SetActive(false);
            record[3].SetActive(true);
            record[4].SetActive(true);
            record[5].SetActive(true);

            record[3].GetComponent<Image>().sprite = numbers[scoreNum / 100];
            if (scoreNum < 100)
                record[3].SetActive(false);

            scoreNum = scoreNum - ((scoreNum / 100) * 100);
            record[4].GetComponent<Image>().sprite = numbers[scoreNum / 10];
            if (scoreNum < 10)
                record[4].SetActive(false);

            scoreNum = scoreNum - ((scoreNum / 10) * 10);
            record[5].GetComponent<Image>().sprite = numbers[scoreNum];
        }

        else
        {
            int goalNum = goals[characterIndex];
            record[0].SetActive(false);
            record[1].SetActive(true);
            record[2].SetActive(true);
            record[3].SetActive(true);
            record[4].SetActive(true);
            record[5].SetActive(true);

            record[1].GetComponent<Image>().sprite = numbers[scoreNum / 10];
            if (scoreNum < 10)
                record[1].SetActive(false);

            scoreNum = scoreNum - ((scoreNum / 10) * 10);
            record[2].GetComponent<Image>().sprite = numbers[scoreNum];

            record[3].GetComponent<Image>().sprite = numbers[10];

            record[4].GetComponent<Image>().sprite = numbers[goalNum / 10];
            if (goalNum < 10)
                record[4].SetActive(false);

            goalNum = goalNum - ((goalNum / 10) * 10);
            record[5].GetComponent<Image>().sprite = numbers[goalNum];

        }
        // 승진성공 - record[0]
        // 왼쪽 십의자리 - record[1]
        // 왼쪽 일의자리 - record[2]
        // 슬래시 - record[3]
        // 오른쪽 십의자리 - record[4]
        // 오른쪽 일의자리 - record[5]
    }

    public void SelectCharacter()
    {
        DataManager.instance.playerIndex = characterIndex;
        SceneManager.LoadScene(1);
    }
}
