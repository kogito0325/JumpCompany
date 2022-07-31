using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform ButtonScale;
    Vector3 defaultScale;

    public void Start()
    {
        defaultScale = ButtonScale.localScale;
    }

    public Image BeSprite;
    public Sprite AfSprite;
    public Image BeSprite2;
    public Sprite AfSprite2;

    public void ChangeImage()
    {
        BeSprite.sprite = AfSprite;
        BeSprite2.sprite = AfSprite2;
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
