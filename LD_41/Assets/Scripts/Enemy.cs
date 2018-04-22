using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float rotationOffset = 50f;
    public bool rotationFlag = true;
    public bool destroyDelayed = true;
    public float rotationSpeed = 1f;
    Vector3 randRotation;

    // Use this for initialization
    void Start () {

        if (destroyDelayed)
        {
            Destroy(this.gameObject, 3f);
        }

    }
	
	// Update is called once per frame
	void Update () {

        EnemyRotation();

    }

    void EnemyRotation()
    {
        if (rotationFlag)
        {
            transform.Rotate(Vector3.forward * -90 * Time.deltaTime * rotationSpeed);
        }

    }
}
