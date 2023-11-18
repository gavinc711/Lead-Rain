using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject explosionPrefab;
	public GameObject gM;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Player")
        {
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