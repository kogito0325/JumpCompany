using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapersScript : MonoBehaviour
{
    public GameObject[] papers;
    public int powerX;

    private void Start()
    {
     
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            powerX = collision.gameObject.GetComponent<Rigidbody>().velocity.x < 0 ? -10 : 10;
            Vector3 power = new Vector3(powerX, 0, 0);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(power * 20, ForceMode.Impulse);
            foreach (GameObject paper in papers)
            {
                paper.GetComponent<Rigidbody>().AddForce(new Vector3(powerX, Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                paper.GetComponent<BoxCollider>().isTrigger = true;
                Destroy(paper, 3f);
            }
        }
    }
}
