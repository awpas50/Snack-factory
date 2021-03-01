using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject laser;
    public float radius;
    public GameObject[] allPrisms;
    public List<GameObject> allPrismsInRange;
    [SerializeField] private GameObject circle;

    void Start()
    {
        InvokeRepeating("GetNearbyPrisms", 0, 0.8f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void GetNearbyPrisms()
    {
        allPrisms = GameObject.FindGameObjectsWithTag("Prism");
        allPrismsInRange.Clear();
        if (allPrisms.Length > 0)
        {
            for (int i = 0; i < allPrisms.Length; i++)
            {
                if (Vector3.Distance(transform.position, allPrisms[i].transform.position) <= radius)
                {
                    allPrismsInRange.Add(allPrisms[i]);
                }
            }
        }
        if (allPrismsInRange.Count > 0)
        {
            CreateLaser();
        }
    }

    void CreateLaser()
    {
        for (int i = 0; i < allPrismsInRange.Count; i++)
        {
            bool connected = false;
            for(int j = 0; j < allPrismsInRange[i].GetComponent<Prism>().connectedTo.Count; j++ )
            {
                if(allPrismsInRange[i].GetComponent<Prism>().connectedTo.Contains(gameObject))
                {
                    connected = true; //end function
                }
            }
            if(!connected)
            {
                allPrismsInRange[i].GetComponent<Prism>().connectedTo.Add(gameObject);
                GameObject o = Instantiate(laser, transform.position, Quaternion.identity);
                Laser laser_script = o.GetComponent<Laser>();
                laser_script.startPoint = gameObject;
                laser_script.endPoint = allPrismsInRange[i];
                laser_script.offsetX = -transform.position.x;
                laser_script.offsetY = -transform.position.y;
                connected = true;
            }
        }
    }

    private void OnMouseOver()
    {
        circle.SetActive(true);
    }
    private void OnMouseExit()
    {
        circle.SetActive(false);
    }
}
