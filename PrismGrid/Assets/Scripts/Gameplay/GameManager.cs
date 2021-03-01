using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]
    public static GameManager i;
    public static bool gameEnded = false;
    public static bool roundEnd = false;
    public static bool lose = false;
    public int waves = 1;
    public int TotalWave = 16;
    public int currency = 10;
    public int score = 0;
    private int finalScore = 0;
    public int live = 20;
    public float roundTime;
    private float roundTime_initial;

    [Header("UI")]
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeRemainingText;
    public GameObject rightPanel;
    public GameObject bgPanel;

    private WaveSpawner waveSpawner;
    private GameObject player;
    private GameObject[] enemies;

    [Header("Do not edit")]
    [SerializeField] private BuildingGrid buildingGrid1;
    [SerializeField] private BuildingGrid buildingGrid2;
    public GameObject selectedBuilding;
    public int selectedBuildingCost;
    [SerializeField] private Vector3 constructingPosition;
    [SerializeField] private BuildingBlueprint building1;
    [SerializeField] private BuildingBlueprint building2;

    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        i = this;
        roundTime_initial = roundTime;
    }

    private void Start()
    {
        //DEBUG
        roundEnd = false;
        player = GameObject.FindGameObjectWithTag("Player");
        waveSpawner = FindObjectOfType<WaveSpawner>();
        waveSpawner.enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;

        Time.timeScale = 1;
        roundTime = roundTime_initial;
        rightPanel.SetActive(false);
        bgPanel.SetActive(false);

        
    }

    private void Update()
    {
        currencyText.text = currency.ToString();

        roundTime -= Time.deltaTime;
        string minutes = Mathf.Floor(roundTime / 60).ToString("00");
        string seconds = (roundTime % 60).ToString("00");

        timeRemainingText.text = "Time remaining: " + string.Format("{0}:{1}", minutes, seconds);
        scoreText.text = "Score: " + score;

        if(roundTime <= 0)
        {
            if(!roundEnd)
            {
                finalScore = score;
            }
            roundEnd = true;
        }

        if(roundEnd)
        {
            waveSpawner.enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        if (selectedBuilding == building1.model)
        {
            buildingGrid1.gameObject.SetActive(true);
            buildingGrid2.gameObject.SetActive(false);
        }
        else if (selectedBuilding == building2.model)
        {
            buildingGrid1.gameObject.SetActive(false);
            buildingGrid2.gameObject.SetActive(true);
        }
        else
        {
            buildingGrid1.gameObject.SetActive(false);
            buildingGrid2.gameObject.SetActive(false);
        }
        //building1
        if (Input.GetMouseButtonDown(0) && buildingGrid1.gameObject.activeSelf)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else if (buildingGrid1.canPlaceBuilding && currency >= selectedBuildingCost)
            {
                //AudioManager.instance.Play(SoundList.CorrectSpot);
                Instantiate(selectedBuilding, buildingGrid1.transform.position, Quaternion.identity);
                currency -= selectedBuildingCost;
            }
            else if (!buildingGrid1.canPlaceBuilding && currency >= selectedBuildingCost)
            {
                //AudioManager.instance.Play(SoundList.WrongSpot);
            }
        }
        //building2
        if (Input.GetMouseButtonDown(0) && buildingGrid2.gameObject.activeSelf)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else if (buildingGrid2.canPlaceBuilding && currency >= selectedBuildingCost)
            {
                //AudioManager.instance.Play(SoundList.CorrectSpot);
                Instantiate(selectedBuilding, buildingGrid2.transform.position, Quaternion.identity);
                currency -= selectedBuildingCost;
            }
            else if (!buildingGrid2.canPlaceBuilding && currency >= selectedBuildingCost)
            {
                //AudioManager.instance.Play(SoundList.WrongSpot);
            }
        }
        if (Input.GetMouseButtonDown(1) && selectedBuilding)
        {
            selectedBuilding = null;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        rightPanel.SetActive(true);
        bgPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        rightPanel.SetActive(false);
        bgPanel.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void SelectBuilding1()
    {
        if(selectedBuilding == building1.model)
        {
            selectedBuilding = null;
        }
        else if(currency >= building1.cost)
        {
            selectedBuilding = building1.model;
            selectedBuildingCost = building1.cost;
        }
    }
    public void SelectBuilding2()
    {
        if (selectedBuilding == building2.model)
        {
            selectedBuilding = null;
        }
        else if (currency >= building2.cost)
        {
            selectedBuilding = building2.model;
            selectedBuildingCost = building2.cost;
        }
    }
}
