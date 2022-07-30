using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BtnType
{
    Start,
    Setup,
    Sound,
    Back,
    Exit
}



public class MainUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BtnType currentType;
    public Transform ButtonScale;
    Vector3 defaultScale;

    public CanvasGroup MainGroup;
    public CanvasGroup BeforeGroup;

    public void Start()
    {
        defaultScale = ButtonScale.localScale;
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

            case BtnType.Sound:
                break;

            case BtnType.Back:
                CanvasGroupOn(MainGroup);
                CanvasGroupOff(BeforeGroup);
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
