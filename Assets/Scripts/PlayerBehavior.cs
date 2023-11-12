using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
    public float speed;
    private float horizontalScreenLimit = 13.5f;
    private float verticalScreenLimit = 4f;
	private GameObject gM;

    // Start is called before the first frame update
    void Start()
    {
		gM = GameObject.Find("GameManager");
		speed = 8f;
    }

    // Update is called once per frame; if your computer runs at 60 fps
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * speed);
		
		//If x position is greater than horizontal screen limit, stop there.
        if (transform.position.x > horizontalScreenLimit)
        {
            transform.position = new Vector3(horizontalScreenLimit, transform.position.y, 0);
        }
		//If x position is less than horizontal screen limit, stop there.
        else if (transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(-horizontalScreenLimit, transform.position.y, 0);
        }
		//If y position is greater than vertical screen limit, stop there.
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
		//If y position is less than vertical screen limit, stop there.
        else if (transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }
    }

    void Shooting()
    {
        //if I press SPACE, create a bullet
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //create a bullet
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
	
	public void LoseLife()
    {
		int lives = gM.GetComponent<GameManager>().lifeCount;
		TextMeshProUGUI count = gM.GetComponent<GameManager>().livesText;
		
        lives--;
		count.text = "Lives: " + lives;
		gM.GetComponent<GameManager>().lifeCount = lives;
		
        if (lives <= 0) 
        {
            //Game Over
			GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}