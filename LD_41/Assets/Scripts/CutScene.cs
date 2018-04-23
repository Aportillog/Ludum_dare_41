using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {

	[SerializeField]
	GameObject sprite1,sprite2;

	[SerializeField]
	float timeToChangeSprite;

    private bool skip = false;

	// Use this for initialization
	void Start () {
		Invoke ("ChangeSprite",timeToChangeSprite);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            skip = true;
            LoadGameplayLevel();
        }
	}

	void ChangeSprite()
	{
		sprite1.SetActive (false);
		sprite2.SetActive (true);
        AudioController.instance.Play("Canon");
        if (!skip)
        {
            Invoke("LoadGameplayLevel", timeToChangeSprite);
        }
	}

	void LoadGameplayLevel()
    {
		SceneManager.LoadScene ("Main");
	}
}
