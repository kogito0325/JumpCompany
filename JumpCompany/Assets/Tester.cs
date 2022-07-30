using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tester : MonoBehaviour
{
    Vector2 MousePos;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(MousePos);
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Input.mousePosition;
        Debug.Log(MousePos);
    }
}
