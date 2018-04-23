using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {

	[SerializeField]
	GameObject sprite1,sprite2;

	[SerializeField]
	float timeToChangeSprite;

	// Use this for initialization
	void Start () {
		Invoke ("ChangeSprite",timeToChangeSprite);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeSprite()
	{
		sprite1.SetActive (false);
		sprite2.SetActive (true);
        AudioController.instance.Play("Canon");
		Invoke ("LoadGameplayLevel", timeToChangeSprite);
	}

	void LoadGameplayLevel()
    {
		SceneManager.LoadScene ("Main");
	}
}
