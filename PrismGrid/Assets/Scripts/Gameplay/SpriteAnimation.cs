using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] spriteList;
    public float loopTime;
    public bool isPaused = false;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteList[0];
        StartCoroutine(PlaySpriteAnimation());
    }

    IEnumerator PlaySpriteAnimation()
    {
        while (true)
        {
            while (isPaused)
            {
                yield return null;
            }
            for (int i = 0; i < spriteList.Length; i++)
            {
                yield return new WaitForSeconds(loopTime);
                GetComponent<SpriteRenderer>().sprite = spriteList[i];
            }
        }
    }
}
