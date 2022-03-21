using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] float speed = 10;
    void Start()
    {
        startPosition = this.transform.position;
    }

    void FixedUpdate()
    {
            if (GameManager.sharedInstance.gamePause|| GameManager.sharedInstance.isGameOver)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            }
            else
            {
                float direction = Input.GetAxis("Horizontal");
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            }
    }
}
