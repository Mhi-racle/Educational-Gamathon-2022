using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private int enemyCount;
    public int upperBoundary = 30;
    public int lowerBoundary = -30;
    private GameObject player;
    public static int missingNumber;
    public int numberOfWaves = 0;
    public int waveCount = 1;
    public int[] ballsToSpawn;
    public Transform ballsInSceneParent; //a transform that will keep track of the balls in the screen by housing them as children
    // public waveDisplay waveDisplay;
    public TextMeshProUGUI questionText; //dsiplay the question on the screen
    public TextMeshProUGUI waveDisplay;
    public GameManager gameManager;

    public GameObject pauseButton;

    public float enemySpeedIncrement;
    public int levelSpeedIncrement = 5;
  
    void Start()
    {
       
        player = GameObject.Find("Player");
        questionText.gameObject.SetActive(false);
        waveDisplay.gameObject.SetActive(false);
        pauseButton.SetActive(false);
        enemySpeedIncrement = 0;

        //instantiates the boosters
        // for (int i = 0; i < numberOfBoosters; i++)
        // {
        //     Instantiate(healthBooster, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), healthBooster.transform.rotation);
        // }

    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.isActive)
        {
            return;
        }
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;

        if (ballsInSceneParent.childCount == 0 && numberOfWaves > 0)
        {
            pauseButton.SetActive(true);
            waveDisplay.gameObject.SetActive(true);
            SpawnWave();
            
            numberOfWaves--;
            if (numberOfWaves == 1)
            {
                waveDisplay.text = "1 wave left";
            }
            else
            {
                waveDisplay.text = numberOfWaves + "  waves left";
            }


            enemySpeedIncrement = levelSpeedIncrement; //sets the new speed to 15
        }
        else if (ballsInSceneParent.childCount == 0 && numberOfWaves <= 0)
        {
            Debug.Log("Conquered");
            waveDisplay.text = "Waves done";
            gameManager.Win();
        }
    }

    void SpawnWave()
    {
        //generates the right hand side of the equation
        int RHS = Random.Range(1, 20);

        int[] ballsToSpawn = GameLogicAddition(RHS); //sends it to the game logic function for addition and gets an array containing 3 numbers that sum up to get the right hand side

        GenerateSpawn(enemyPrefabs[ballsToSpawn[1] - 1], ballsInSceneParent); //it spawns the first object in the scene, that will be the answer
        ballsInSceneParent.GetChild(0).gameObject.tag = "Answer";
        //sets the question text active and gives it the message to display
        questionText.gameObject.SetActive(true);
        questionText.text = "Eliminate X " + "\n" + ballsToSpawn[0] + " + X + " + ballsToSpawn[2] + " = " + RHS;

        //it the spawns 2 other random balls to add with the answer so as to make the game interactive
        for (int i = 0; i < 2; i++)
        {
            int randomNumber = Random.Range(1, 20);

            if (randomNumber == ballsToSpawn[1]) //checks if the random ball generated is not the same as the one already in the scene
            {
                randomNumber = Random.Range(1, 20);
                GenerateSpawn(enemyPrefabs[randomNumber], ballsInSceneParent);
            }
            else
            {
                GenerateSpawn(enemyPrefabs[randomNumber], ballsInSceneParent);
            }

        }

    }

    int GenerateRandom(int upperBoundary, int lowerBoundary)
    {
        return (Random.Range(Random.Range(lowerBoundary, upperBoundary), Random.Range(lowerBoundary, upperBoundary)));
    }

    void GenerateSpawn(GameObject enemyPrefab, Transform parent)
    {

        Vector3 pos = new Vector3(GenerateRandom(lowerBoundary, upperBoundary), 1, GenerateRandom(lowerBoundary, upperBoundary));
        // Instantiate(gatePrefab, pos, gatePrefab.transform.rotation);
        GameObject ball = Instantiate(enemyPrefab, pos, enemyPrefab.transform.rotation);
        ball.transform.parent = parent;
        ball.GetComponent<Enemy>().speed += enemySpeedIncrement;
        upperBoundary -= 5;
        lowerBoundary += 5;
    }



    int[] GameLogicAddition(int RHS)
    {
        int[] numbers = new int[3];

        //generates 3 randoms that sum up to the number at the right hand side
        int x = Random.Range(1, RHS);
        int y = RHS - x;
        numbers[0] = y;

        int q = Random.Range(1, x);
        int z = x - q;
        numbers[1] = z;
        numbers[2] = q;

        return numbers;

    }



}

