using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;

    public float JumpPower;
    public float MoveSpeed;

    private bool IsJumping;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        IsJumping = false;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");

        transform.Translate((new Vector3(h, 0, 0) * MoveSpeed));

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsJumping)
            {
                IsJumping = true;
                rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }

            else
            {
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
        }
    }
}