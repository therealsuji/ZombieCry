using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool playerIsDead = false;
    public new Camera camera;
    public GameObject player;
    public Canvas playerHealthBarCanvas;
    public TextMeshProUGUI waveNo;
    public TextMeshProUGUI scoreText;
    public int currentScore;

    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthBarCanvas = GameObject.FindGameObjectWithTag("playerHealthCanvas").GetComponent<Canvas>();
        waveNo = GameObject.FindGameObjectWithTag("waveTxt").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindGameObjectWithTag("scoreTxt").GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        scoreText.text = currentScore.ToString();
    }
}
