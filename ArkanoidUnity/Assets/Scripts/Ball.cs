using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 nextDirection;
    [SerializeField] float speed = 5;
    [SerializeField] AudioClip hit;
    Transform spawnPos;
    private bool isMoving=false;
    public int timesBouncedOnBorders = 0;

    void Start()
    {
        if (spawnPos==null)
        {
            spawnPos = GameObject.FindGameObjectWithTag("Respawn").transform;
            this.transform.position = spawnPos.position;
        }
        timesBouncedOnBorders = 0;
        startPos = this.transform.position;
        //Calculate the starting direction randomly
        float x = Random.Range(-0.1f,0.1f);
        nextDirection = new Vector2(x,Vector2.up.y).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.sharedInstance.isGameOver)
        {
            if (this.transform.position.y < -0.5f)//If the ball left the space in game
            {
                GameManager.sharedInstance.ballOut = true;
                Destroy(this.gameObject);
            }

            if (!GameManager.sharedInstance.gameStarted)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.transform.position = spawnPos.position;
            }
            else if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePause && !isMoving)
            {
                this.GetComponent<Rigidbody2D>().velocity = nextDirection * speed;
                isMoving = true;
            }
            else if (GameManager.sharedInstance.gameStarted && GameManager.sharedInstance.gamePause && isMoving)
            {
                nextDirection = this.GetComponent<Rigidbody2D>().velocity / speed;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isMoving = false;
            }
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        this.GetComponent<AudioSource>().clip = hit;
        this.GetComponent<AudioSource>().Play();
        if (other.gameObject.tag == "Player")
        {
            float x = hitFactor(this.transform.position, other.transform.position, other.collider.bounds.size.x);
            float y = GetComponent<Rigidbody2D>().velocity.y / speed;
            nextDirection = new Vector2(x, y).normalized;
            this.GetComponent<Rigidbody2D>().velocity = nextDirection * speed;
            timesBouncedOnBorders = 0;
        } else if (other.gameObject.tag=="Border")
        {
            timesBouncedOnBorders++;
            if (timesBouncedOnBorders>=4)
            {
                float x = GetComponent<Rigidbody2D>().velocity.x / speed;
                float y = Vector2.up.y;
                nextDirection = new Vector2(x, y).normalized;
                this.GetComponent<Rigidbody2D>().velocity = nextDirection * speed;
                timesBouncedOnBorders = 0;
            }
        }
        else{
            timesBouncedOnBorders = 0;
        }
    }

    private float hitFactor(Vector2 ballPos,Vector2 playerPos,float playerWidth)
    {
        return (ballPos.x - playerPos.x) / playerWidth;
    }

}
