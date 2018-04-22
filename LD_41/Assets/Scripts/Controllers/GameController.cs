using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {

    public static GameController instance;

    //Player variables
    private Vector2 playerInitPos;
    private PlayerController pjCtrlScript;
    private GameObject player;
    

    //Time variables
    private float restartTime;
    public float startWait;
    public float gameOverWait;
    public float spawnWait;
    public float restartGameDelay = 5f;

    //EnemySpawners variables
    private int[] spawnXValues;
    public float spawnLimit_y_max;
    public float spawnLimit_y_min;
    public int spawnLimit_x_right;
    public int spawnLimit_x_left;
    private GameObject enemySpawner;

    //Spawnable objects
    [SerializeField]
    public GameObject[] spawnableObjects; //All the spawnable objects referenced at unity
    private Dictionary<int, GameObject> spawnObjectsDic; //Dictionary with all spawnable objects

    //UI variables
    public int heightScore;
    private Text heightValueTxt;

    //Settings variables
    public float scrollingSpeed = 0.5f;

    //Gameplay variables
    public bool isGameOver = false;

    //Scene variables
    private string sceneLoaded;

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

    // called first
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded: " + scene.name);
        //Debug.Log(mode);

        sceneLoaded = scene.name;

        //Restart game variables
        isGameOver = false;

        if (scene.name == "Main")
        {
            setupMainLevel();
        }
    }

    private void Start()
    {

        //Dictionary code for managing spawnable objects
        //Add all objects to a dictionary to instantly spawn the desired object, without looping every time the player builds something
        spawnObjectsDic = new Dictionary<int, GameObject>();

        for (int i = 0; i < spawnableObjects.Length; i++)
        {
            spawnObjectsDic.Add(i, spawnableObjects[i]);
        }

        spawnXValues = new int[2];
        spawnXValues[0] = spawnLimit_x_right;
        spawnXValues[1] = spawnLimit_x_left;

        //setupMainLevel();

    }

    private void Update()
    {
        if(sceneLoaded == "Main")
        {
            updateScore();
        }
    }

    private void updateScore()
    {
        //Get player height
        heightScore = pjCtrlScript.getHeight();
        //Show updated score
        heightValueTxt.text = heightScore.ToString() + " m";
    }

    public void setScore(int score)
    {
        this.heightScore = score;
    }

    public void setGameOver()
    {
        isGameOver = true;
        StartCoroutine(gameOver());
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(startWait);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startWait);

        while (sceneLoaded == "Main")
        {
            //Add offset calculated from enemySpawner
            float offset = enemySpawner.transform.position.y;
            //This x spawn limit should be always const
            int randXIndex = UnityEngine.Random.Range(0, spawnXValues.Length);
            Vector2 spawnPosition = new Vector2(spawnXValues[randXIndex], UnityEngine.Random.Range(spawnLimit_y_max + offset, spawnLimit_y_min + offset));
            Quaternion spawnRotation = Quaternion.identity;


            //Try to get the object from the dictionary
            GameObject temp = spawnObjectsDic[UnityEngine.Random.Range(0, spawnObjectsDic.Count)];
            GameObject clone = (GameObject)Instantiate(temp, spawnPosition, Quaternion.identity);

            //Give a velocity to the clone
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-spawnXValues[randXIndex], 0);


            yield return new WaitForSeconds(spawnWait);
        }
    }

    private void setupMainLevel()
    {
        //Score (height)
        heightValueTxt = GameObject.Find("Height_value").GetComponent<Text>();

        //Game variables
        heightScore = 0;
        isGameOver = false;
        //Player
        player = GameObject.FindGameObjectWithTag("Player");
        pjCtrlScript = player.GetComponent<PlayerController>();
        playerInitPos = pjCtrlScript.getInitialPos();
        //EnemySpawner
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");

        //Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator waitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
