using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text weightValueText;
    public Image livesValue;
    public Image gameOver;
    public Player player;
    public Sprite[] livesSprites;

    public AudioClip gameOverSound;
    public AudioClip dieSound;

    private int lives = 3;
    private AudioSource audioSource;
    private Factory[] factories;


    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        factories = FindObjectsOfType<Factory>();
	}
	
	// Update is called once per frame
	void Update () {
        weightValueText.text = "" + player.Weight;

        if(Input.GetKeyDown(KeyCode.R))
        {
            Init();
        }
    }

    public void Init()
    {
        // Initialize the lives
        lives = 3;
        livesValue.sprite = livesSprites[lives - 1];

        // Initialize player
        player.Init();

        // Initialize the factories
        foreach (var factory in factories)
        {
            factory.enabled = true;
            factory.Init();
        }

        // Unpause the game
        Time.timeScale = 1;

        // Hide game over
        gameOver.gameObject.SetActive(false);
    }

    public void KillPlayer()
    {
        lives--;

        // Check if player is dead
        if(lives == 0)
        {
            // Disable all factories
            foreach (var factory in factories)
                factory.enabled = false;

            // Game over
            audioSource.clip = gameOverSound;
            audioSource.Play();
            gameOver.gameObject.SetActive(true);

            // Pause game
            Time.timeScale = 0;
        }
        else
        {
            // Just lost a life

            audioSource.clip = dieSound;
            audioSource.Play();
            livesValue.sprite = livesSprites[lives - 1];
        }

        player.transform.position = Vector3.zero;
    }
}
