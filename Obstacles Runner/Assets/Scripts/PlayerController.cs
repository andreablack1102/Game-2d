using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public Text grapeText;
    public HealthBar healthBar;
    public Text scoreText;
    private int grapeFind = 7;

    public AudioSource backgroundAudio;
    public AudioSource jumpAudio;
    public AudioSource collideAudio;
    public AudioClip dieAudio;
    public AudioClip collectGrapeAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        backgroundAudio.Play();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        grapeText.text =  grapeFind.ToString();
        scoreText.text = "Score:" + Scoring.totalScore;


    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            jumpAudio.Play();
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
        

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        if (Health.totalHealth == 0f)
        {
            AudioSource.PlayClipAtPoint(dieAudio, transform.position);
            playerAnimation.SetBool("Die", true);
            player.velocity = new Vector2(0, 0);
            FindObjectOfType<GameOver>().GameOver1();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            if (Health.totalHealth != 0f)
            {
                collideAudio.Play();
                healthBar.Damage(0.15f);
            }               
            transform.position = respawnPoint;
        }        
        else if (collision.tag == "NextLevel" && grapeFind==0)
        {
            int Numrd = UnityEngine.Random.Range(30, 100);
            Scoring.totalScore = Scoring.totalScore + Numrd;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // Can also use SceneManager.LoadScene(1); to load a specific scene
            respawnPoint = transform.position;
        }        
        else if (collision.tag == "Grape")
        {
            if(Health.totalHealth != 0f)
            {
                AudioSource.PlayClipAtPoint(collectGrapeAudio, transform.position);
                Scoring.totalScore = Scoring.totalScore + 3;
                grapeFind -= 1;
                grapeText.text = grapeFind.ToString();
                scoreText.text = "Score:" + Scoring.totalScore;
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "Obstacle")
        {
            if (Health.totalHealth != 0f)
            {
                collideAudio.Play();
                healthBar.Damage(0.15f);
            }
        }
        
        //else if (collision.tag == "PreviousLevel")
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //    respawnPoint = transform.position;
        //}
        
        //else if (collision.tag == "Checkpoint")
        //{
        //    respawnPoint = transform.position;
        //}


    }
    
    
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Obstacle")
    //    {
    //        healthBar.Damage(0.02f);
    //    }
    //}
    //public void NewGame()
    //{

    //    Scoring.totalScore = 0;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    //}
    

}
