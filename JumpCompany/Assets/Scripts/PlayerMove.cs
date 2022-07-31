using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public float JumpPower;
    public float MoveSpeed;

    private bool IsJumping;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        IsJumping = false;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxis("Vertical");

        transform.Translate((new Vector3(h, 0, 0) * MoveSpeed));
        Debug.Log(Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            spriteRenderer.flipX = true;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            spriteRenderer.flipX = false;

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