using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private Vector2 playerInitPos;
    private PlayerController pjCtrlScript; 

    public static GameManager instance;
    public GameObject player;
    public Text heightValueTxt;

    public int heightScore;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //Make GameController inmortal
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        heightScore = 0;

        //Player stuff
        pjCtrlScript = player.GetComponent<PlayerController>();
        playerInitPos = pjCtrlScript.getInitialPos();

    }

    private void Update()
    {
        updateScore();
    }

    private void updateScore()
    {
        //Get player height
        heightScore = pjCtrlScript.getHeight();
        //Show updated score
        heightValueTxt.text = heightScore.ToString();
    }
}
