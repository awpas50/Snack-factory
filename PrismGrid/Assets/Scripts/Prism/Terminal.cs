using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject laser;
    public float radius;
    private GameObject[] allPrisms;
    void Start()
    {
        InvokeRepeating("GetNearbyTerminal", 0, 0.5f);
    }

    void Update()
    {
        if(allPrisms.Length > 0)
        {
            CreateLaser();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void GetNearbyTerminal()
    {
        allPrisms = GameObject.FindGameObjectsWithTag("Prism");
    }

    void CreateLaser()
    {
        for (int i = 0; i < allPrisms.Length; i++)
        {
            bool connected = false;
            for(int j = 0; j < allPrisms[i].GetComponent<Prism>().connectedTo.Count; j++ )
            {
                if(allPrisms[i].GetComponent<Prism>().connectedTo.Contains(gameObject))
                {
                    connected = true; //end function
                }
            }
            if(!connected)
            {
                allPrisms[i].GetComponent<Prism>().connectedTo.Add(gameObject);
                GameObject o = Instantiate(laser, transform.position, Quaternion.identity);
                Laser laser_script = o.GetComponent<Laser>();
                laser_script.startPoint = gameObject;
                laser_script.endPoint = allPrisms[i];
                connected = true;
            }
        }
    }
}
