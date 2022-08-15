using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BtnType
{
    Start,
    Setup,
    BGMmute,
    SEmute,
    Arrow,
    Reset,
    Back,
    Exit,
    Quit

}



public class UIBtnManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BtnType currentType;
    public Transform ButtonScale;
    Vector3 defaultScale;
    
    public AudioSource BGMsource;
    public AudioSource SEsource;
    
    public Scrollbar Scrollbar;
    
    public CanvasGroup MainGroup;
    public CanvasGroup BeforeGroup;

    public Sprite[] BGM;
    public Sprite[] SE;
    public int BGMMuteIndex = 1;
    public int SEMuteIndex = 1;

    public GameObject nowBGM;
    public GameObject nowSE;

    public ChangeImgManager changeImgManager;


    public void ChangeBGMMuteSprite()
    {
        if (nowBGM != null)
            nowBGM.GetComponent<Image>().sprite = BGM[BGMMuteIndex];
    }
    public void ChangeSEMuteSprite()
    {
        if (nowSE != null)
            nowSE.GetComponent<Image>().sprite = SE[SEMuteIndex];
    }
    public void Start()
    {
        if (ButtonScale != null)
            defaultScale = ButtonScale.localScale;
        ChangeBGMMuteSprite();
        ChangeSEMuteSprite();
    }

    public void SetBGMVolume(float volume)
    {
        BGMsource.volume = volume;
    }

    public void SetSEvolume(float volume)
    {
        SEsource.volume = volume;
    }

    public void OnSEAudio()
    {
        SEsource.Play();
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BtnType.Start:
                CanvasGroupOn(BeforeGroup);
                CanvasGroupOff(MainGroup);
                break;

            case BtnType.Setup:
                CanvasGroupOn(BeforeGroup);
                CanvasGroupOff(MainGroup);
                break;

            case BtnType.BGMmute:
                if(BGMsource.volume > 0)
                {
                    PlayerPrefs.SetFloat("BGM", BGMsource.volume);
                    BGMsource.volume = 0;
                    Scrollbar.value = BGMsource.volume;
                    BGMMuteIndex = 0;
                }
                else
                {
                    BGMsource.volume = PlayerPrefs.GetFloat("BGM");
                    Scrollbar.value = BGMsource.volume;
                    BGMMuteIndex += 1;
                }
                ChangeBGMMuteSprite();
                break;

            case BtnType.SEmute:
                if (SEsource.volume > 0)
                {
                    PlayerPrefs.SetFloat("SE", SEsource.volume);
                    SEsource.volume = 0;
                    Scrollbar.value = SEsource.volume;
                    SEMuteIndex = 0;
                }
                else
                {
                    SEsource.volume = PlayerPrefs.GetFloat("SE");
                    Scrollbar.value = SEsource.volume;
                    SEMuteIndex += 1;
                }
                ChangeSEMuteSprite();
                break;

            case BtnType.Back:
                CanvasGroupOn(MainGroup);
                CanvasGroupOff(BeforeGroup);
                break;

            case BtnType.Reset:
                PlayerPrefs.DeleteAll();
                DataManager.instance.scores = new int[] { 0, 0, 0, 0, 0 };
                
                DataManager.instance.soundVolume = 0.5f;
                DataManager.instance.bgmVolume = 0.5f;

                BGMsource.volume = 0.5f;
                SEsource.volume = 0.5f;

                changeImgManager.characterIndex = 0;
                changeImgManager.ChangeCharacter();
                break;

            case BtnType.Arrow:
                break;

            case BtnType.Exit:
                Application.Quit();
                break;

            case BtnType.Quit:
                SceneManager.LoadScene(0);
                break;

        }
    }
    
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonScale != null)
            ButtonScale.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ButtonScale != null)
            ButtonScale.localScale = defaultScale;
    }
}
