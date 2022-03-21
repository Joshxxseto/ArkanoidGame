using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private int lives = 1;
    private Color color;

    public void extraLives(int i)
    {
        this.lives = i;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            lives--;
            if (lives <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                //As the pieces recives a hit, it's color is going to change ultil tur white
                color = this.GetComponent<SpriteRenderer>().color;
                float H, S, V;
                Color.RGBToHSV(color, out H, out S, out V);
                S = S / 1.5f;
                this.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(H, S, V);
            }
        }

    }
}
