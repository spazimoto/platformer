using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public Text score;
    public Text win;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    Animator anim;
    private bool facingRight = true;
    public AudioSource music;
    public AudioClip bgm;
    public AudioClip winner;
    public int x;
    public int y;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        music = GetComponent<AudioSource>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        music.Play();
        music.clip = bgm;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 1); //if the player is pressing the D or the A key, set the animation state to walk
        }
        else
        {
            anim.SetInteger("State", 0); //else set the animation state to idle
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 5)
        {
            transform.position = new Vector2(x, y); // the player gets translated to the next stage
        }
        if (scoreValue == 10)
        {
            win.text = "You win! Game created by Morgan DeMunck.";
            if (music.clip == bgm)
            {
                music.Stop();
                music.clip = winner;
                music.Play();
            }

        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (livesValue == 0)
        {
            Destroy(this);
            win.text = "You lose! Restart and try again.";
            anim.SetInteger("State", 3);
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }
    }
}