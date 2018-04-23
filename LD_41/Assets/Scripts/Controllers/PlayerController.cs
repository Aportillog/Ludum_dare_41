using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Clicker variables
    private int clickCounter;
    private float clickRate;
    private float clickCounterTime = 0.3f;

    //Game Variables
    private int height;

    //Player variables
    private Vector3 lastPosition;
    public Vector2 initialPos;
    private float currentSpeed = 0.5f;
    public float acceleration = 1.5f;
    public bool isInmortal = false;

    private Animator animator;


	[SerializeField]
	float maxY,minY;
	[SerializeField]
	float gravity = -3.0f;

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
		if (transform.position.y >= maxY)
			transform.position = new Vector3(transform.position.x,maxY,transform.position.z);

		if (transform.position.y <= minY)
			transform.position = new Vector3(transform.position.x,minY,transform.position.z);
        //Change player acceleration depending on direction
        changeGravity();
        //Change speed depending on the click rate
        changeSpeed();
        //Add a click to the count
        countClicks();
    }

    private void movePlayer()
    {
		this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gravity + currentSpeed);

        //Current traveled distance
        Vector3 direction = getDirection();
        if(direction.y > 0)
        {
            height += Mathf.RoundToInt(Vector2.Distance(transform.position, lastPosition) * 10);
        }
        else
        {
            height += (2 * Mathf.RoundToInt(clickRate));
        }
        
        lastPosition = transform.position;
    }

    private void changeSpeed()
    {
        //Relate currentSpeed with clickRate linearly y=mx+n //y=currentSpeed, x=clickRate, m=0.3, n=1
        //currentSpeed = clickRate * 0.3f + 1;
        currentSpeed = clickRate * 0.3f * acceleration;

    }
    private void changeGravity()
    {
        if ((clickRate>4) && (clickRate < 6))
        {
            //acceleration = 1.7f;
            gravity = 0.7f;
        }
        else if(clickRate >= 6)
        {
            gravity = 1.2f;
        }
        else
        {
            //acceleration = 0.1f;
            gravity = -4;
        }
    }

    private Vector3 getDirection()
    {
        return (transform.position - lastPosition).normalized;
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
            GameObject.Find("Flames").GetComponent<ParticleSystem>().Stop();
            GameObject.Find("Smoke").GetComponent<ParticleSystem>().Stop();
            animator.SetTrigger("playerDeath");
            GameController.instance.setGameOver();
        }
    }


}
