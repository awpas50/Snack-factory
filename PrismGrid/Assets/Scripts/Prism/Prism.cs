using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Prism : MonoBehaviour
{
    public List<GameObject> connectedTo;
    public Sprite close;
    public Sprite open;
    public Light2D light2D;
    [SerializeField] private GameObject circle;

    private void Update()
    {
        if(connectedTo.Count == 0)
        {
            GetComponent<SpriteRenderer>().sprite = close;
            light2D.enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = open;
            light2D.enabled = true;
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
