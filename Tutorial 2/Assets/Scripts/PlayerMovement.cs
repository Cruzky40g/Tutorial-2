using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public int lives = 3;

    public Text score;
    public GameObject winText;
    public GameObject loseText;
    public Text livesText;
    public Animator anim;

    private int scoreValue = 0;
    private bool newLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if(hozMovement != 0)
        {
            anim.SetBool("Walk", true);
        }
        if(hozMovement < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (hozMovement > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (hozMovement == 0)
        {
            anim.SetBool("Walk", true);
        }
        if (vertMovement > 0 && rd2d.velocity.y > 0)
        {
            anim.SetBool("Jump", true);
        }
        if (rd2d.velocity == Vector2.zero)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Jump", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue > 10)
            {
                winText.SetActive(true);
            }
            if (scoreValue > 5 && newLevel == false)
            {
                newLevel = true;
                transform.position = new Vector3(21, 1.2f, 0);
                GameObject.Find("Main Camera").transform.position = new Vector3(22, 0, -10);
            }
        }
        if(collision.collider.tag == "Respawn")
        {
            loseText.SetActive(true);
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            lives--;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetBool("Jump", false);
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
