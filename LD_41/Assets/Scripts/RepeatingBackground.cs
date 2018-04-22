using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour {

    private BoxCollider2D bgCollider;
    private float bgVerticalLength;       //A float to store the y-axis length of the collider2D attached to the background GameObject.

    //Awake is called before Start.
    private void Awake()
    {
        //Get and store a reference to the collider2D attached to background.
        bgCollider = GetComponent<BoxCollider2D>();
        //Store the size of the collider along the y axis (its length in units).
        bgVerticalLength = bgCollider.size.y;
    }

    //Update runs once per frame
    private void Update()
    {
        //Check if the difference along the y axis between the main Camera and the position of the object this is attached to is greater than bgVerticalLength.
        if (transform.position.y < -bgVerticalLength)
        {
            //If true, this means this object is no longer visible and we can safely move it forward to be re-used.
            RepositionBackground();
        }
    }

    //Moves the object this script is attached to the top in order to create our looping background effect.
    private void RepositionBackground()
    {
        //This is how far to the top we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        Vector2 bgOffSet = new Vector2(0, bgVerticalLength * 2f);

        //Move this object from it's position offscreen, downside the player, to the new position off-camera upside of the player.
        transform.position = (Vector2)transform.position + bgOffSet;
    }
}
