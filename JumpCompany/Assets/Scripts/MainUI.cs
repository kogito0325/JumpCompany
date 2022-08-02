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

    public void Start()
    {
        defaultScale = ButtonScale.localScale;
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
                }
                else
                {
                    BGMsource.volume = PlayerPrefs.GetFloat("BGM");
                    Scrollbar.value = BGMsource.volume;
                }
                break;

            case BtnType.SEmute:
                if (SEsource.volume > 0)
                {
                    PlayerPrefs.SetFloat("SE", SEsource.volume);
                    SEsource.volume = 0;
                    Scrollbar.value = SEsource.volume;
                }
                else
                {
                    SEsource.volume = PlayerPrefs.GetFloat("SE");
                    Scrollbar.value = SEsource.volume;
                }
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
