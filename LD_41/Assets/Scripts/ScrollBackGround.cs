using UnityEngine;
using System.Collections;

public class ScrollBackGround : MonoBehaviour {

    public float scrollingSpeed = 0.5f;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 offSet = new Vector2(0, Time.time * scrollingSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offSet;
	}
}
