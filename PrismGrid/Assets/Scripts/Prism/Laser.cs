using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public bool hasEnergy = true;
    [HideInInspector] public GameObject startPoint;
    [HideInInspector] public GameObject endPoint;
    [HideInInspector] public float offsetX;
    [HideInInspector] public float offsetY;

    float rechargeTimer = 0;
    float chargeTime = 4;
    LineRenderer lr;
    EdgeCollider2D edgeCollider;

    public GameObject laserEffect;
    private GameObject laserEffectRef;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, startPoint.transform.position);
        lr.SetPosition(1, endPoint.transform.position);

        laserEffectRef = Instantiate(laserEffect, endPoint.transform.position, Quaternion.identity);
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.offset = new Vector2(offsetX, offsetY);
        edgeCollider.points = new Vector2[2] { new Vector2(startPoint.transform.position.x, startPoint.transform.position.y),
                                                                new Vector2(endPoint.transform.position.x, endPoint.transform.position.y)};
    }

    private void Update()
    {
        if(!hasEnergy)
        {
            rechargeTimer += Time.deltaTime;
            if(rechargeTimer >= chargeTime)
            {
                rechargeTimer = 0;
                lr.enabled = true;
                edgeCollider.enabled = true;
                hasEnergy = true;
            }
        }
        
        // self destruct if the laser has no connection anymore
        if(!startPoint || !endPoint)
        {
            Destroy(laserEffectRef);
            Destroy(gameObject);
        }

        if (!hasEnergy)
        {
            laserEffectRef.SetActive(false);
        }
        else
        {
            laserEffectRef.SetActive(true);
        }
    }
}
