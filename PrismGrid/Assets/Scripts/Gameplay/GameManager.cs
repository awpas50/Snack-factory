using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public static bool gameEnded = false;
    public static bool win = false;
    public static bool lose = false;
    public int waves = 1;
    public int TotalWave = 16;
    public int currency = 10;
    public int live = 20;

    public TextMeshProUGUI currencyText;
    private GameObject player;
    private GameObject[] enemies;

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
    }

    private void Start()
    {
        
        win = false;
        lose = false;
        player = GameObject.FindGameObjectWithTag("Player");
        //if (!player.GetComponent<PlayerMovement>().enabled)
        //{
        //    player.GetComponent<PlayerMovement>().enabled = true;
        //}
        StartCoroutine(AddMoney());
    }

    IEnumerator AddMoney()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            currency++;
        }
        
    }
    private void Update()
    {
        currencyText.text = "Currency: " + currency;
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
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
