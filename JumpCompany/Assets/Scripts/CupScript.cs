using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    public Rigidbody rigid;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "GameManager")
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Rigidbody>().AddForce(Vector3.down);
            }

            Destroy(gameObject);
        }
    }
}
