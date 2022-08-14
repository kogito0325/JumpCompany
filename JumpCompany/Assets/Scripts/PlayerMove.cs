using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider boxCollider;
    AudioSource audioSource;
    
    public GameManager manager;
    public Sprite[] jumpSprites;
    public GameObject arrow;

    public float jumpPower;
    public float jumpDistance;
    public float moveSpeed;

    private bool isJumping;
    private bool jumpReady;

    public AudioClip audioJump;
    public AudioClip audioClear;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();

        isJumping = false;
        jumpReady = false;
    }

    void Start()
    {
        Debug.Log(DataManager.instance.scores[DataManager.instance.playerIndex]);
    }

    void Update()
    {
        // 이동
        Move();

        // 점프
        Jump();

        // 점프 중 하강하는 모션
        if (isJumping && rigid.velocity.y < 0)
        {
            spriteRenderer.sprite = jumpSprites[2];
            boxCollider.isTrigger = false;
        }
    }

    void PlaySound(string action)
    {
        audioSource.volume = DataManager.instance.soundVolume;
        switch (action)
        {
            case "Jump":
                audioSource.clip = audioJump;
                break;

            case "Clear":
                audioSource.clip = audioClear;
                break;
        }
        audioSource.Play();
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
        // 점프 1/3 - 점프 준비 시작
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            jumpReady = true;

            anim.enabled = false;
            arrow.SetActive(true);
            spriteRenderer.sprite = jumpSprites[0];
        }

        // 점프 2/3 - 점프 준비
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

        // 점프 3/3 - 점프 준비 끝, 점프 완료
        if (Input.GetKeyUp(KeyCode.Space) && jumpReady)
        {
            // 점프력이 낮을 때 점프 방지
            if (jumpPower > 20)
            {
                spriteRenderer.sprite = jumpSprites[1];
                if (jumpDistance < 0)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;

                rigid.AddForce(new Vector3(jumpDistance, jumpPower), ForceMode.Impulse);
                PlaySound("Jump");
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

        else if (collision.transform.tag == "Goal")
        {
            rigid.velocity = Vector3.zero;
            boxCollider.enabled = false;
            rigid.useGravity = false;
            spriteRenderer.flipX = true;
            anim.enabled = true;

            GetComponent<PlayerMove>().enabled = false;
            
            StartCoroutine("Wait");
        } 
    }

    IEnumerator Wait()
    {
        rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.0f);
        transform.localScale = transform.localScale * 1.5f;
        transform.position = new Vector3(0, GameManager.instance.goal * 14 + 3.8f, 0);
        Instantiate(manager.crown, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        yield return new WaitForSeconds(3.0f);
        PlaySound("Clear");
        yield return new WaitForSeconds(3.0f);
        PlayerPrefs.SetInt("clear", 1);
        manager.SceneChange();
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