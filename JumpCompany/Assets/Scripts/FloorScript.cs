using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameObject[] settableObjects;
    public GameObject floor;
    public Transform setPosition;
    public int last_pos_x_index;

    int[] x_line1 = new int[3] { -6, 0, 6 };
    int[] x_line2 = new int[3] { -7, -1, 5 };

    private void Awake()
    {

    }

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(settableObjects[Random.Range(0, settableObjects.Length)], SetObjectsPosition(i), Quaternion.Euler(0, 90, 0), setPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 SetObjectsPosition(int i)
    {
        int pos_x_index = Random.Range(0, x_line1.Length);

        float x_pos = i % 2 == 0 ? x_line1[pos_x_index] : x_line2[pos_x_index];
        float y_pos = i * 3.5f + setPosition.position.y;

        return new Vector3(x_pos, y_pos, setPosition.position.z);
    }
}
