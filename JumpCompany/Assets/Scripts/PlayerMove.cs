using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider boxCollider;
    
    public GameManager manager;
    public Sprite[] jumpSprites;
    public GameObject arrow;

    public float jumpPower;
    public float jumpDistance;
    public float moveSpeed;

    private bool isJumping;
    private bool jumpReady;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();


        isJumping = false;
        jumpReady = false;
    }

    void Start()
    {

    }

    void Update()
    {
        Move();
        Jump();
        if (isJumping && rigid.velocity.y < 0)
        {
            spriteRenderer.sprite = jumpSprites[2];
            boxCollider.isTrigger = false;
        }
        manager.score = (int)transform.position.y / manager.floorHeight + 1;
    }

    void Move()
    {
        if (!isJumping && !jumpReady)
        {
            float h = Input.GetAxisRaw("Horizontal");
            

            transform.Translate((new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime));

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                anim.SetBool("isWalk", true);
            else
                anim.SetBool("isWalk", false);

            if (Input.GetKey(KeyCode.LeftArrow))
                spriteRenderer.flipX = true;
            else if (Input.GetKey(KeyCode.RightArrow))
                spriteRenderer.flipX = false;

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
                anim.SetBool("isWalk", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            jumpReady = true;

            anim.enabled = false;
            arrow.SetActive(true);
            spriteRenderer.sprite = jumpSprites[0];
        }


        if (Input.GetKey(KeyCode.Space) && jumpReady)
        {
            if (jumpPower < 100)
                jumpPower += 100 * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (jumpDistance > -100)
                    jumpDistance -= 100 * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (jumpDistance < 100)
                    jumpDistance += 100 * Time.deltaTime;
            }

            arrow.transform.localScale = new Vector3(jumpPower/100, arrow.transform.localScale.y);
            arrow.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90 - jumpDistance * 45 / 100);
        }

        if (Input.GetKeyUp(KeyCode.Space) && jumpReady)
        {
            if (jumpPower > 20)
            {
                spriteRenderer.sprite = jumpSprites[1];
                if (jumpDistance < 0)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;

                rigid.AddForce(new Vector3(jumpDistance, jumpPower), ForceMode.Impulse);
                boxCollider.isTrigger = true;
                isJumping = true;
            }
            else
                anim.enabled = true;

            jumpReady = false;

            jumpDistance = 0;
            jumpPower = 0;

            arrow.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((isJumping && collision.transform.tag == "Ground") || (isJumping && collision.transform.tag == "FloorGround"))
        {
            isJumping = false;
            anim.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (boxCollider.isTrigger && other.transform.tag == "Wall")
            boxCollider.isTrigger = false;
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.transform.tag == "FloorGround" && rigid.velocity.y > 0)
        {
            other.GetComponent<BoxCollider>().isTrigger = false;

        }
    }
}