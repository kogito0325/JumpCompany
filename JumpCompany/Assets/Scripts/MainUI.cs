using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BtnType
{
    Start,
    Setup,
    BGMmute,
    SEmute,
    Arrow,
    Reset,
    Back,
    Exit
}



public class MainUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    public int BGMMuteIndex = 0;
    public int SEMuteIndex = 0;

    public GameObject nowBGM;
    public GameObject nowSE;


    public void ChangeBGMMuteSprite()
    {
        nowBGM.GetComponent<Image>().sprite = BGM[BGMMuteIndex];
    }
    public void ChangeSEMuteSprite()
    {
        nowSE.GetComponent<Image>().sprite = SE[SEMuteIndex];
    }
    public void Start()
    {
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
                break;

            case BtnType.Arrow:
                break;

            case BtnType.Exit:
                Application.Quit();
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
        ButtonScale.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale;
    }
}
