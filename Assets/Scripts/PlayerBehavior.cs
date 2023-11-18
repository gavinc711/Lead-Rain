using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
	public GameObject thruster;
	public GameObject shield;
	public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public float speed;
	public int lives;
	private GameObject gM;
    private float horizontalScreenLimit = 13.5f;
    private float verticalScreenLimit = 4f;
	private bool betterWeapon;
 
    // Start is called before the first frame update
    void Start()
    {
		gM = GameObject.Find("GameManager");
		speed = 6f;
		lives = 3;
		betterWeapon = false;
		thruster.SetActive(false);
		shield.SetActive(false);
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
        if(Input.GetKeyDown(KeyCode.Space) && !betterWeapon)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        } 
		else if(Input.GetKeyDown(KeyCode.Space) && betterWeapon)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -45f)); ;
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 45f));
        }
    }
	
	public void LoseLife()
    {
        lives--;
		gM.GetComponent<GameManager>().LivesChange(lives);
		
        if (lives <= 0) 
        {
            //Game Over
			gM.GetComponent<GameManager>().GameOver();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
	
	private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.name)
        {
            case "Coin(Clone)":
                //I picked a coin!
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                gM.GetComponent<GameManager>().EarnScore(1);
                Destroy(collision.gameObject);
                break;
            case "Health(Clone)":
                //I picked a health!
                AudioSource.PlayClipAtPoint(healthSound, transform.position);
                if (lives >= 3)
                {
                    gM.GetComponent<GameManager>().EarnScore(1);
                } else if (lives < 3)
                {
                    lives++;
                    gM.GetComponent<GameManager>().LivesChange(lives);
                }
                Destroy(collision.gameObject);
                break;
            case "Power Up(Clone)":
                //I picked a powerup!
                AudioSource.PlayClipAtPoint(powerupSound, transform.position);
                Destroy(collision.gameObject);
                int tempInt;
                tempInt = Random.Range(1, 4);
                if (tempInt == 1)
                {
                    speed = 10f;
                    StartCoroutine("SpeedPowerDown");
                    gM.GetComponent<GameManager>().PowerupChange("Speed");
                    thruster.SetActive(true);
                } else if (tempInt == 2)
                {
                    betterWeapon = true;
                    StartCoroutine("WeaponPowerDown");
                    gM.GetComponent<GameManager>().PowerupChange("Weapon");
                } else if (tempInt == 3)
                {
                    //Shield Powerup
                    gM.GetComponent<GameManager>().PowerupChange("Shield");
					shield.SetActive(true);
                }
                break;
        }
    }
	
	IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(20f);
        AudioSource.PlayClipAtPoint(powerdownSound, transform.position);
        speed = 6f;
        thruster.SetActive(false);
        gM.GetComponent<GameManager>().PowerupChange("No Powerup");
    }
	
	 IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(20f);
        AudioSource.PlayClipAtPoint(powerdownSound, transform.position);
        betterWeapon = false;
        gM.GetComponent<GameManager>().PowerupChange("No Powerup");
    }
}
