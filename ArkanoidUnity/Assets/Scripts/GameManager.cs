using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    [SerializeField] GameObject ball;
    [SerializeField] AudioClip bgMusic, endMusic;
    public bool gameStarted = false, gamePause = false, ballOut=false;

    public int lives = 3;
    public bool isGameOver = false, isWinner = false, waiting = false;

    void Awake()
    {
        if (!sharedInstance) sharedInstance = this;
        StartCoroutine("PrepareGame");
        PrepareGame();

        this.GetComponent<AudioSource>().Stop();
        this.GetComponent<AudioSource>().loop=true;
        this.GetComponent<AudioSource>().clip = bgMusic;
        this.GetComponent<AudioSource>().Play();

        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted && !gamePause&&!isGameOver)
        {
            //We start the game
            gameStarted = true;
        }else if (Input.GetKeyDown(KeyCode.Space) && gameStarted && !isGameOver)
        {
            gamePause = !gamePause;
        }
        if (ballOut && !gamePause && gameStarted && !isGameOver)
        {
            lives--;
            if (lives > 0)
            {
                isGameOver = false;
                Instantiate(ball);
                ballOut = false;
                gameStarted = false;

            }
            else if (lives <= 0)
            {
                isGameOver = true;
                this.GetComponent<AudioSource>().Stop();
                this.GetComponent<AudioSource>().clip = endMusic;
                this.GetComponent<AudioSource>().loop = false;
                this.GetComponent<AudioSource>().Play();
            }
        } else if (isGameOver&&isWinner&&!waiting)
        {
            waiting = true;
            this.GetComponent<AudioSource>().Stop();
            this.GetComponent<AudioSource>().clip = endMusic;
            this.GetComponent<AudioSource>().loop = false;
            this.GetComponent<AudioSource>().Play();
        }
        if (isGameOver&&Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator PrepareGame()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Instantiate(ball);
        SpawnPieces.sharedInstance.spawnRows(9);
    }
}
