using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    public GameObject Desk;
    public GameObject Bag;

    Vector3 Deskpos = new Vector3(2, 0, 0);
    private void Start()
    {
        if (Instantiate(Desk))
        {
            Instantiate(Bag, Deskpos, Quaternion.identity);
        }
    }
}
