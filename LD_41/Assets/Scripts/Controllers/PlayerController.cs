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

    private float currentSpeed = 0.5f;
    public float acceleration = 1.5f;
    public int initialHealth = 1;

    
	// Use this for initialization
	void Start () {
        transform.position = initialPos;
        lastPosition = transform.position;
        currentHealth = initialHealth;

        //Always start moving
        clickCounter = 1;
        clickRate = 1;
        
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
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, currentSpeed);
        height += Mathf.RoundToInt(Vector2.Distance(transform.position, lastPosition) * 100);
        lastPosition = transform.position;
    }

    private void changeSpeed()
    {
        //Relate currentSpeed with clickRate linearly y=mx+n //y=currentSpeed, x=clickRate, m=0.3, n=1
        //currentSpeed = clickRate * 0.3f + 1;
        currentSpeed = clickRate * 0.3f * acceleration;

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
            clickCounter = 1;
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
