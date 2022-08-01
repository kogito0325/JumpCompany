using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider capsuleCollider;
    
    public Sprite[] jumpSprites;
    public GameObject arrow;

    public float JumpPower;
    public float JumpDistance;
    public float MoveSpeed;

    private bool IsJumping;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();


        IsJumping = false;
    }

    void Start()
    {

    }

    void Update()
    {
        Move();
        Jump();
        if (IsJumping && rigid.velocity.y < 0)
        {
            spriteRenderer.sprite = jumpSprites[2];
            capsuleCollider.isTrigger = false;
        }
    }

    void Move()
    {
        if (!IsJumping)
        {
            float h = Input.GetAxisRaw("Horizontal");
            // float v = Input.GetAxis("Vertical");

            transform.Translate((new Vector3(h, 0, 0) * MoveSpeed * Time.deltaTime));

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                anim.SetBool("isWalk", true);
            else
                anim.SetBool("isWalk", false);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                spriteRenderer.flipX = true;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                spriteRenderer.flipX = false;

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
                anim.SetBool("isWalk", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsJumping)
        {
            IsJumping = true;

            anim.enabled = false;
            arrow.SetActive(true);
            spriteRenderer.sprite = jumpSprites[0];
        }


        if (Input.GetKey(KeyCode.Space) && IsJumping)
        {
            if (JumpPower < 100)
                JumpPower += 100 * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (JumpDistance > -100)
                    JumpDistance -= 100 * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (JumpDistance < 100)
                    JumpDistance += 100 * Time.deltaTime;
            }

            arrow.transform.localScale = new Vector3(JumpPower/100, arrow.transform.localScale.y);
            arrow.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90 - JumpDistance * 45 / 100);
        }

        if (Input.GetKeyUp(KeyCode.Space) && IsJumping)
        {
            spriteRenderer.sprite = jumpSprites[1];
            if (JumpDistance < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;

            rigid.AddForce(new Vector3(JumpDistance, JumpPower), ForceMode.Impulse);
            capsuleCollider.isTrigger = true;

            JumpDistance = 0;
            JumpPower = 0;

            arrow.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsJumping && collision.transform.tag == "Ground")
        {
            IsJumping = false;
            anim.enabled = true;
        }
    }
}