using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapersScript : MonoBehaviour
{
    public GameObject Drawer;
    public GameObject Papers;

    Vector3 pos = new Vector3(3,0,0);
    private void Start()
    {
        if(Instantiate(Drawer))
        {
            Instantiate(Papers, pos, Quaternion.identity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
