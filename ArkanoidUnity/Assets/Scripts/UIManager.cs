using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image live1, live2, live3;
    public GameObject gameOver,winGame,start;

    private void Start()
    {
        this.gameOver.SetActive(false);
        this.winGame.SetActive(false);
        this.start.SetActive(true);
        live1.enabled = true;
        live3.enabled = true;
        live2.enabled = true;
    }

    void Update()
    {
        if (GameManager.sharedInstance.gameStarted)
        {
            this.start.SetActive(false);
        }
        if (GameManager.sharedInstance.lives<3)
        {
            live3.enabled = false;
        }
        if (GameManager.sharedInstance.lives < 2)
        {
            live2.enabled = false;
        }
        if (GameManager.sharedInstance.lives < 1)
        {
            live1.enabled = false;
        }
        if (GameManager.sharedInstance.isGameOver)
        {
            showGameOver();
        }
    }

    void showGameOver()
    {
        if (!GameManager.sharedInstance.isWinner)
        {
            this.gameOver.SetActive(true);
        }
        else
        {
            this.winGame.SetActive(true);
        }
        
    }
}
