using UnityEngine;
using System.Collections;

public class TargetFollower : MonoBehaviour {

    private Vector3 targetPos;

    public GameObject target;
    public float interpVelocity;
    public Vector3 offset;
    private void Start()
    {
        targetPos = this.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 posNoZ = transform.position;

        posNoZ.z = target.transform.position.z;

        Vector3 targetDirection = (target.transform.position - posNoZ);

        interpVelocity = targetDirection.magnitude * 5f;

        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
    }
}
