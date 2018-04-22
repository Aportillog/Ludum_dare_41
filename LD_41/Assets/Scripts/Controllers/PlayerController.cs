using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Clicker variables
    private int clickCounter;
    private float clickRate;
    private float clickCounterTime = 0.5f;

    //Game Variables
    private int height;

    //Player variables
    private Vector2 lastPosition;
    public Vector2 initialPos;
    private float currentSpeed = 0.5f;
    public float acceleration = 1.5f;
    public bool isInmortal = false;

    private Animator animator;


    // Use this for initialization
    void Start () {
        //Setup player
        transform.position = initialPos;
        lastPosition = transform.position;
        animator = GetComponent<Animator>();

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
        //Current traveled distance
        height += Mathf.RoundToInt(Vector2.Distance(transform.position, lastPosition) * 100);
        lastPosition = transform.position;
    }

    private void changeSpeed()
    {
        //Relate currentSpeed with clickRate linearly y=mx+n //y=currentSpeed, x=clickRate, m=0.3, n=1
        //currentSpeed = clickRate * 0.3f + 1;
        currentSpeed = clickRate * 0.3f * acceleration;

    }

    public int getHeight()
    {
        return height;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Enemy")
        {
            killPlayer();
        }
    }

    public Vector2 getInitialPos()
    {
        return initialPos;
    }

    private void killPlayer()
    {
        if(!isInmortal)
        {
            animator.SetTrigger("playerDeath");
            GameController.instance.setGameOver();
        }
    }

}
