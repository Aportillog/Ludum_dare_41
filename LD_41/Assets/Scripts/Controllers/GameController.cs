﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    //Player variables
    private Vector2 playerInitPos;
    private PlayerController pjCtrlScript;
    public GameObject player;
    

    //Time variables
    private float restartTime;
    public float startWait;
    public float spawnWait;
    public float restartGameDelay = 5f;

    //EnemySpawners variables
    private int[] spawnXValues;
    public float spawnLimit_y_max;
    public float spawnLimit_y_min;
    public int spawnLimit_x_right;
    public int spawnLimit_x_left;
    public GameObject enemySpawner;

    //Spawnable objects
    [SerializeField]
    public GameObject[] spawnableObjects; //All the spawnable objects referenced at unity
    private Dictionary<int, GameObject> spawnObjectsDic; //Dictionary with all spawnable objects

    //UI variables
    public int heightScore;
    public Text heightValueTxt;

    //Settings variables
    public float scrollingSpeed = 0.5f;

    //Gameplay variables
    public bool isGameOver = false;

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
        isGameOver = false;

        //Player variables
        pjCtrlScript = player.GetComponent<PlayerController>();
        playerInitPos = pjCtrlScript.getInitialPos();

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

        //Start spawning enemies
        StartCoroutine(SpawnEnemies());

    }

    private void Update()
    {
        updateScore();
        gameOver();
    }

    private void updateScore()
    {
        //Get player height
        heightScore = pjCtrlScript.getHeight();
        //Show updated score
        heightValueTxt.text = heightScore.ToString();
    }

    private void gameOver()
    {
        if (pjCtrlScript.getCurrentHealth() <= 0)
        {
            isGameOver = true;
            restartTime += Time.deltaTime;
            if (restartTime > restartGameDelay)
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }

        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            //Add offset calculated from enemySpawner
            float offset = enemySpawner.transform.position.y;
            //This x spawn limit should be always const
            int randXIndex = Random.Range(0, spawnXValues.Length);
            Vector2 spawnPosition = new Vector2(spawnXValues[randXIndex], Random.Range(spawnLimit_y_max + offset, spawnLimit_y_min + offset));
            Quaternion spawnRotation = Quaternion.identity;


            //Try to get the object from the dictionary
            GameObject temp = spawnObjectsDic[Random.Range(0, spawnObjectsDic.Count)];
            GameObject clone = (GameObject)Instantiate(temp, spawnPosition, Quaternion.identity);

            //Give a velocity to the clone
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-spawnXValues[randXIndex], 0);


            yield return new WaitForSeconds(spawnWait);
        }
    }

}
