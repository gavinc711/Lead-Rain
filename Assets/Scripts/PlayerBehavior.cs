using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    //1. access (public or private)
    //2. type (int, float, bool)
    //3. name (naming conventions) they always start with a letter that is not capitalized, but you can have multiple words with no spaces and following words could be capitalized
    //4. OPTIONAL: you can give it a value
    //borders: 8.5 and 6.5

    public float speed;
    public float horizontalInput;
    public float verticalInput;
    public float horizontalScreenLimit;
    public float verticalScreenLimitPositive;
	public float verticalScreenLimitNegative;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        horizontalScreenLimit = 13.5f;
        verticalScreenLimitPositive = 1f;
		verticalScreenLimitNegative = -4f;
    }

    // Update is called once per frame; if your computer runs at 60 fps
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
		
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
        if (transform.position.y > verticalScreenLimitPositive)
        {
            transform.position = new Vector3(transform.position.x, verticalScreenLimitPositive, 0);
        }
		//If y position is less than vertical screen limit, stop there.
        else if (transform.position.y < verticalScreenLimitNegative)
        {
            transform.position = new Vector3(transform.position.x, verticalScreenLimitNegative, 0);
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
}