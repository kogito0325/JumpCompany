using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapersScript : MonoBehaviour
{
    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
