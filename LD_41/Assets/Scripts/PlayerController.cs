using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private int clickCounter;
    private float clickRate;
    private float clickCounterTime = 0.5f;

    public float currentSpeed = 0.5f;
    public float maxSpeed = 2f;
    public float minSpeed = 0.5f;
    public  GameObject player;

	// Use this for initialization
	void Start () {
        clickCounter = 0;
        clickRate = 0;
        
        //Start the click counter
        StartCoroutine(calculateClickRate(clickCounterTime));
    }

    private void FixedUpdate()
    {
        //Add a click to the count
        countClicks();

        movePlayer();
    }
    void Update () {
        //Change speed depending on the click rate
        changeSpeed();
	}

    private void movePlayer()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, currentSpeed);
    }

    private void changeSpeed()
    {
        //Debug.Log("ClickRate: " + clickRate);
        if(clickRate<3)
        {
            currentSpeed = 0.5f;
        }
        else if((clickRate>3) && (clickRate<5))
        {
            currentSpeed = 0.7f;
        }
        else if((clickRate >= 5) && (clickRate < 7))
        {
            currentSpeed = 1.3f;
        }
        else if (clickRate >= 7)
        {
            currentSpeed = 2f;
        }

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

}
