using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;
    public GameObject t_final;
    public GameObject t_marker;
    [Header("Final tutorial")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public GameObject spawnPath;
    public Button rubbishBin;
    private float timer1 = 0;
    private float timer2 = 0;
    public List<GameObject> enemiesForTutorial;
    private GameObject[] allPrisms;
    private bool triggerInfinitiveWave = false;
    private bool triggerFourthWave = false;

    void Start()
    {
        InvokeRepeating("GetNearbyPrisms", 0, 0.8f);
        rubbishBin.interactable = false;
    }

    void GetNearbyPrisms()
    {
        allPrisms = GameObject.FindGameObjectsWithTag("Prism");
    }
    void Update()
    {
        //t1
        timer1 += Time.deltaTime;
        if(timer1 > 6f)
        {
            t1.SetActive(false);
        }
        else
        {
            t1.SetActive(true);
        }

        //t2
        if(timer1 > 6f && enemiesForTutorial.Count > 0)
        {
            t2.SetActive(true);
        }
        else
        {
            t2.SetActive(false);
        }
        if(enemiesForTutorial.Count > 0)
        {
            for (var i = enemiesForTutorial.Count - 1; i > -1; i--)
            {
                if (enemiesForTutorial[i] == null)
                    enemiesForTutorial.RemoveAt(i);
            }
        }
        //t3
        if(enemiesForTutorial.Count == 0 && allPrisms.Length < 3)
        {
            t3.SetActive(true);
            t_marker.SetActive(true);
        }
        else
        {
            t3.SetActive(false);
            t_marker.SetActive(false);
        }

        //t4
        if(allPrisms.Length >= 3)
        {
            if(!triggerInfinitiveWave)
            {
                StartCoroutine(InfinitiveWave());
            }
            triggerInfinitiveWave = true;
            triggerFourthWave = true;
        }
        if(triggerFourthWave)
        {
            rubbishBin.interactable = true;
            timer2 += Time.deltaTime;
            if (timer2 < 18)
            {
                t4.SetActive(true);
            }
            else
            {
                t4.SetActive(false);
                t_final.SetActive(true);
            }
        }
    }

    IEnumerator InfinitiveWave()
    {
        while (true)
        {
            for(int i = 0; i < 6; i++)
            {
                GameObject e = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                e.GetComponent<EnemyPath>().path = spawnPath;
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(16);
        }
    }
}
