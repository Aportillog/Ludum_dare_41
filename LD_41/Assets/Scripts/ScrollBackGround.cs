using UnityEngine;
using System.Collections;

public class ScrollBackGround : MonoBehaviour {

    private float scrollingSpeed;
	// Use this for initialization
	void Start () {
        scrollingSpeed = GameController.instance.scrollingSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        scrollingSpeed = GameController.instance.scrollingSpeed;
        Vector2 offSet = new Vector2(0, Time.time * scrollingSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offSet;
	}
}
