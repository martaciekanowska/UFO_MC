using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Text scoreText;
    public Text winText;
    Rigidbody2D rb2d;
    public float speed = 0;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(MoveHorizontal, MoveVertical);
        rb2d.AddForce(movement * speed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.PlaySFX("Bounce");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            count++;
            Destroy(collision.gameObject);
            UpdateScoreText();
            AudioManager.instance.PlaySFX("Coin"); 
        }
    }

    // Update is called once per frame
    void UpdateScoreText()
    {
        scoreText.text = "Wynik: " + count;
        if (count == 3)
        {
            winText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            AudioManager.instance.PlaySFX("Win");
            StartCoroutine(StopTime());
            
        }
    }

 
    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Level02");
    }
}
