using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionGrid : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private Vector3 topRightCorner;
    [SerializeField] private Vector3 bottomLeftCorner;

    [HideInInspector] public bool canDestruct = false;
    public GameObject hitObjectRef;
    private int collisionCount = 0;
    void FixedUpdate()
    {
        pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.x = Mathf.Clamp(pos.x, bottomLeftCorner.x, topRightCorner.x);
        pos.y = Mathf.Clamp(pos.y, bottomLeftCorner.y, topRightCorner.y);
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        pos.z = 0;
        transform.position = pos;
    }

   
    public bool IsNotColliding
    {
        get { return collisionCount == 0; }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsNotColliding && canDestruct)
            {
                //sell building
                Destroy(hitObjectRef);
                //9:Prism 12:Terminal
                if(hitObjectRef.layer == 9)
                {
                    GameManager.i.currency += 3;
                }
                if (hitObjectRef.layer == 12)
                {
                    GameManager.i.currency += 10;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //9:Prism 12:Terminal
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            canDestruct = true;
            hitObjectRef = other.gameObject;
        }
        collisionCount++;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //9:Prism 12:Terminal
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            canDestruct = true;
            hitObjectRef = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //9:Prism
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            canDestruct = false;
            hitObjectRef = other.gameObject;
        }
        collisionCount--;
    }
}
