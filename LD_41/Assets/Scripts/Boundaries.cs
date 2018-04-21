using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        //Destroy everything that enter the trigger
        Destroy(other.gameObject);
    }
}
