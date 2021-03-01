using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //+50% speed buff
        if(other.gameObject.tag == "Enemy")
        {
            AudioManager.instance.Play(SoundList.SpeedBoosted);
            other.gameObject.GetComponent<EnemyPath>().speed_buffed = 1.5f;
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
