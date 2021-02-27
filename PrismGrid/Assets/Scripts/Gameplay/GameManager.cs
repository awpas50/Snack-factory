using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public static bool gameEnded = false;
    public static bool win = false;
    public static bool lose = false;
    public int waves = 1;
    public int TotalWave = 11;
    public int live = 20;

    private GameObject player;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI lifeText;
    public GameObject beginning_panel;
    public GameObject death_panel;
    public GameObject win_panel;
    public GameObject lose_panel;

    public GameObject teleportPoint1;
    public GameObject teleportPoint2;

    private float respawnTimer = 0;
    public float waitTime;

    private bool dead = false;
    private bool respawnTrigger = false;

    private float t = 0;
    private float t11 = 0;
    private int[] enemyNumOnEachWave = new int[11] { 5, 5, 6, 8, 8, 8, 12, 10, 10, 10, 20 };

    private GameObject[] enemies;
    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        i = this;
    }

    private void Start()
    {
        win = false;
        lose = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<PlayerMovement>().enabled)
        {
            player.GetComponent<PlayerMovement>().enabled = false;

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
