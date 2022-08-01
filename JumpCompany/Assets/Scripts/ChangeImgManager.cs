using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeImgManager : MonoBehaviour
{
    public Sprite[] records;
    public Sprite[] characters;
    public int characterIndex = 0;

    public GameObject nowRecord;
    public GameObject nowCharacter;

    public void Start()
    {
        ChangeCharacter();
    }

    public void ChangeCharacter()
    {
        nowRecord.GetComponent<Image>().sprite = records[characterIndex];
        nowCharacter.GetComponent<Image>().sprite = characters[characterIndex];
    }

    public void SetNextIndex()
    {
        if (characterIndex < characters.Length - 1)
            characterIndex += 1;
        ChangeCharacter();
    }

    public void SetPreviousIndex()
    {
        if (characterIndex > 0)
            characterIndex -= 1;
        ChangeCharacter();
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("character", characterIndex);
        SceneManager.LoadScene(1);
    }
}
