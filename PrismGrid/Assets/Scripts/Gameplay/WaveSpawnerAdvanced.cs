using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerAdvanced : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;

    private int _enemyCount;
    private float _timeBetweenEnemies;

    public GameObject portal1;
    public GameObject portal2;
    public GameObject portal3;

    private float t = 0;

    [SerializeField] private bool callWave0 = false;
    [SerializeField] private bool callWave1 = false;
    [SerializeField] private bool callWave2 = false;
    [SerializeField] private bool callWave3 = false;
    [SerializeField] private bool callWave4 = false;
    [SerializeField] private bool callWave5 = false;
    [SerializeField] private bool callWave6 = false;
    
    void Update()
    {
        t += Time.deltaTime;
        if(t > 210 && !callWave6)
        {
            Instantiate(enemy2, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy2, portal2.transform.position, Quaternion.identity);
            Instantiate(enemy2, portal3.transform.position, Quaternion.identity);
            callWave6 = true;
        }
        else if (t > 180 && !callWave5)
        {
            Instantiate(enemy1, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal2.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            callWave5 = true;
        }
        else if (t > 150 && !callWave4)
        {
            Instantiate(enemy2, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal2.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            callWave4 = true;
        }
        else if (t > 120 && !callWave3)
        {
            Instantiate(enemy2, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal2.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            callWave3 = true;
        }
        else if (t > 90 && !callWave2)
        {
            Instantiate(enemy2, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            callWave2 = true;
        }
        else if (t > 60 && !callWave1)
        {
            Instantiate(enemy2, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal2.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            callWave1 = true;
        }
        else if (t > 30 && !callWave0)
        {
            Instantiate(enemy1, portal1.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal2.transform.position, Quaternion.identity);
            Instantiate(enemy1, portal3.transform.position, Quaternion.identity);
            callWave0 = true;
            
        }
        else
        {
            return;
        }
    }

    //IEnumerator SpawnEnemies(int whichWave)
    //{
    //    // [i]: number of waves
    //    // [j]: determine which type of enemy to spawn
    //    while (!GameManager.roundEnd)
    //    {
    //        _enemyCount = waves[whichWave].enemyList.Length;
    //        _timeBetweenEnemies = waves[whichWave].timeBetweenEnemies;

    //        for (int j = 0; j < waves[whichWave].enemyList.Length; j++)
    //        {
    //            if (waves[whichWave].enemyList[j] != null)
    //            {
    //                GameObject e = Instantiate(waves[whichWave].enemyList[j], spawnPoint.position, spawnPoint.rotation);
    //                e.GetComponent<EnemyPath>().path = gameObject;
    //            }
    //            yield return new WaitForSeconds(waves[whichWave].timeBetweenEnemies);
    //        }
    //        yield return new WaitForSeconds(timeBetweenWaves);
    //    }
    //}
}
