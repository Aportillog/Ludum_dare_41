using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float rotationOffset = 50f;
    public bool rotationFlag = true;
    public bool destroyDelayed = true;
    Vector3 randRotation;

    // Use this for initialization
    void Start () {

        CalculateRotation();
        if (destroyDelayed)
        {
            Destroy(this.gameObject, 5f);
        }

    }
	
	// Update is called once per frame
	void Update () {

        EnemyRotation();

    }

    void CalculateRotation()
    {
        randRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randRotation.z = 0;
    }

    void EnemyRotation()
    {
        if (rotationFlag)
        {
            //transform.Rotate(randRotation * Time.deltaTime);
            transform.Rotate(Vector3.forward * -90 * Time.deltaTime);
        }

    }
}
