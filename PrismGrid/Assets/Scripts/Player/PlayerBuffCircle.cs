using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffCircle : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //+40% speed buff
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPath>().speed_buffed = 1.4f;
            other.gameObject.GetComponent<EnemyPath>().speedBoostIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //remove the +40% speed buff
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPath>().speed_buffed = 1;
            other.gameObject.GetComponent<EnemyPath>().speedBoostIcon.enabled = false;
        }
    }
}
