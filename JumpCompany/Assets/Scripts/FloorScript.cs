using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameObject[] settableObjects;
    public GameObject[] obstacles;
    public GameObject[] walls;
    public GameObject floor;
    public Transform setPosition;

    public int last_pos_x_index;
    public int floorNumber;


    int[] x_line1 = new int[3] { -6, 0, 6 };
    int[] x_line2 = new int[3] { -7, -1, 5 };

    private void Awake()
    {
        floorNumber = GameManager.instance == null ? 1 : GameManager.instance.floorNumber;
        GetComponent<FloorScript>().enabled = true;
    }

    void Start()
    {
        // 오브젝트 생성 - 4개
        last_pos_x_index = PlayerPrefs.GetInt("posX");
        for (int i = 0; i < 4; i++)
        {
            int objIndex = Random.Range(0, settableObjects.Length);
            Vector3 objVector = SetObjectsPosition(i);
            Instantiate(settableObjects[objIndex], objVector, Quaternion.Euler(0, 90, 0), setPosition);

            // 책상 위 장애물 생성 로직
            int probability = Random.Range(0, 3);
            if (objIndex < 2)
            {
                // 장애물 생성 - 종이
                if (probability == 0)
                    Instantiate(obstacles[1], objVector + new Vector3(Random.Range(0, 2f), 1.7f, 0), Quaternion.Euler(0, 30, 0), setPosition);

                // 장애물 생성 - 가방
                else if (probability == 1)
                    Instantiate(obstacles[2], objVector + new Vector3(Random.Range(0, 2f), 1.3f, 0), Quaternion.Euler(0, 30, 0), setPosition);

            }
        }
        PlayerPrefs.SetInt("posX", last_pos_x_index);

        // 장애물 생성 - 컵
        if (Random.Range(0, 5) < 5)
            Instantiate(obstacles[0], new Vector3(Random.Range(-4f, 4f), setPosition.position.y, setPosition.position.z), Quaternion.identity, setPosition);

        
        


    }

    void Update()
    {
        if (GameManager.instance.score > floorNumber + 5)
        {
            Destroy(gameObject);
        }
    }

    Vector3 SetObjectsPosition(int i)
    {
        int pos_x_index;
        do
        {
            pos_x_index = Random.Range(0, x_line1.Length);
        } while (last_pos_x_index == pos_x_index);

        float x_pos = i % 2 == 0 ? x_line1[pos_x_index] : x_line2[pos_x_index];
        float y_pos = i * 3.5f + setPosition.position.y;

        last_pos_x_index = pos_x_index;

        return new Vector3(x_pos, y_pos, setPosition.position.z);
    }

}
