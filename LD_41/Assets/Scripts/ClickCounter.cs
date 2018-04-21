using UnityEngine;
using System.Collections;

public class ClickCounter : MonoBehaviour {

    public int clickCounter;
	void Start () {
        clickCounter = 0;
    }

    private void Update()
    {
        clickCount();
    }

    private void clickCount()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCounter++;
        }

        Debug.Log("ClickCounter: " + clickCounter);
    }

    IEnumerator countClicks(float time)
    {
        while (true)
        {
            //Restart click counter
            clickCounter = 0;
            yield return new WaitForSeconds(time);

        }
    }
}
