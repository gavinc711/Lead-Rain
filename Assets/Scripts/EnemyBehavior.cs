using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject explosionPrefab;
	public GameObject gM;
=======

    public GameObject explosionPrefab;
>>>>>>> 88c8a73503714f31ac41a3a4e89aa566c3c880a6

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        gM = GameObject.Find("GameManager");
=======
        
>>>>>>> 88c8a73503714f31ac41a3a4e89aa566c3c880a6
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Player")
        {
<<<<<<< HEAD
			if (whatIHit.GetComponent<PlayerBehavior>().shield.activeInHierarchy == true)
			{
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				whatIHit.GetComponent<PlayerBehavior>().shield.SetActive(false);
				gM.GetComponent<GameManager>().PowerupChange("No Powerup");
				Destroy(this.gameObject);
			}
			else 
			{
				whatIHit.GetComponent<PlayerBehavior>().LoseLife();
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
=======
			whatIHit.GetComponent<PlayerBehavior>().LoseLife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
>>>>>>> 88c8a73503714f31ac41a3a4e89aa566c3c880a6
        }
        else if (whatIHit.tag == "Weapon")
        {
			GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(2);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }
}