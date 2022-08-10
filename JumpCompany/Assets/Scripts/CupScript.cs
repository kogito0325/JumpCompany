using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    public GameObject Cup;
    Rigidbody rigid;


    Vector3 pos = new Vector3(0, 10, 0);
    private void Start()
    {
        FallCup();
    }
    void FallCup()
    {
        Instantiate(Cup, pos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "FloorGround")
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FloorGround")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
