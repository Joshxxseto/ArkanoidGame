using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPieces : MonoBehaviour
{
    public static SpawnPieces sharedInstance;
    private static Vector2 nextPos;
    [SerializeField] GameObject[] pieces;

    public int childCount;

    void Awake()
    {
        if (!sharedInstance)
        {
            sharedInstance = this;
        }
    }
    private void Update()
    {
        if (GameManager.sharedInstance.gameStarted&&!GameManager.sharedInstance.isGameOver)
        {
            childCount = this.transform.childCount;
            if (childCount == 0)
            {
                Debug.Log("GameOver");
                GameManager.sharedInstance.isWinner = true;
                GameManager.sharedInstance.isGameOver = true;
            }
        }
    }

    public void spawnRows(int numberOfRows)
    {
        nextPos = this.transform.position;
        //Here, we are going to spawn a given number of rows full of pieces
        for (int i=0;i<numberOfRows;i++)
        {
            //Now we'e gonna asing the number of hits that the piece can afford for each row
            //1-3 row=1hits  4-6=2hits 6-9=3hits 
            int lives = (i + 3) / 3;
            //WichPiece is going to be spawned
            int r = Random.Range(0, pieces.Length);
            for (int x=0;x<9;x++)
            {
                //The with of the pieces is of 1 unit
                if (x != 0)
                {
                    //Positive Side
                    nextPos.x = x;
                    Instantiate(pieces[r], nextPos, Quaternion.identity,this.transform).GetComponent<Piece>().extraLives(lives);
                    //Negative Side
                    nextPos.x = -x;
                    Instantiate(pieces[r], nextPos, Quaternion.identity, this.transform).GetComponent<Piece>().extraLives(lives);
                }
                else
                {
                    //the 0 case
                    nextPos.x = 0;
                    Instantiate(pieces[r],nextPos, Quaternion.identity,this.transform).GetComponent<Piece>().extraLives(lives);
                }
            }
            //After spawning the row whe should go to the next one
            nextPos.y += 0.5f;//0.5f is the heigt of the pice
        }
        childCount = this.transform.childCount;
    }
}
