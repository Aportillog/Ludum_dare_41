using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private int clickCounter;
    private float clickRate;
    private float clickCounterTime = 0.5f;
    private int height;
    private int currentHealth;

    private Vector2 lastPosition;
    public Vector2 initialPos;

    public float currentSpeed = 0.5f;
    public float maxSpeed = 2f;
    public float minSpeed = 0.5f;
    public  GameObject player;
    public int initialHealth = 1;

    
	// Use this for initialization
	void Start () {
        transform.position = initialPos;
        lastPosition = transform.position;
        currentHealth = initialHealth;

        clickCounter = 0;
        clickRate = 0;
        
        //Start the click counter
        StartCoroutine(calculateClickRate(clickCounterTime));
    }

    private void FixedUpdate()
    {
        movePlayer();
    }
    void Update () {
        //Change speed depending on the click rate
        changeSpeed();
        //Add a click to the count
        countClicks();
    }

    private void movePlayer()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, currentSpeed);
        height += Mathf.RoundToInt(Vector2.Distance(transform.position, lastPosition) * 100);
        lastPosition = transform.position;
    }

    private void changeSpeed()
    {
        if (clickRate < 2)
        {
            currentSpeed = 0.5f;
        }
        else if ((clickRate >= 2) && (clickRate < 4))
        {
            currentSpeed = 0.7f;
        }
        else if ((clickRate >= 4) && (clickRate < 5))
        {
            currentSpeed = 1f;
        }
        else if ((clickRate >= 5) && (clickRate < 6))
        {
            currentSpeed = 1.5f;
        }
        else if ((clickRate >= 6) && (clickRate < 7))
        {
            currentSpeed = 2f;
        }
        else if ((clickRate >= 7) && (clickRate < 8))
        {
            currentSpeed = 7f;
        }
        else if (clickRate >= 8)
        {
            currentSpeed = 10f;
        }

    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    private void countClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCounter++;
        }
    }

    IEnumerator calculateClickRate(float time)
    {
        while (true)
        {
            //Restart click counter
            clickCounter = 0;
            yield return new WaitForSeconds(time);

            clickRate = clickCounter / time;
            //Debug.Log("Click Rate: " + clickRate);

        }
    }

    public Vector2 getInitialPos()
    {
        return initialPos;
    }

    public int getHeight()
    {
        return height;
    }

}
