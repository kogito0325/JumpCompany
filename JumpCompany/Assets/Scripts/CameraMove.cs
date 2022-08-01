using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public GameObject[] characters;

    private void Start()
    {
        target = characters[PlayerPrefs.GetInt("character")].transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);
    }
}
