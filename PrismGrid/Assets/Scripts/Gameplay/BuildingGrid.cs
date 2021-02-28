using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private Vector3 topRightCorner;
    [SerializeField] private Vector3 bottomLeftCorner;

    public Color correctColor;
    public Color incorrectColor;

    [HideInInspector] public bool canPlaceBuilding = false;

    [SerializeField] private SpriteRenderer buildingSprite;
    [SerializeField] private SpriteRenderer circle;

    private int collisionCount = 0;
    [SerializeField] private bool selectingLargeBuilding = false;
    void FixedUpdate()
    {
        if(!selectingLargeBuilding)
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
        else
        {
            pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.x = Mathf.Clamp(pos.x, bottomLeftCorner.x, topRightCorner.x);
            pos.y = Mathf.Clamp(pos.y, bottomLeftCorner.y, topRightCorner.y);
            pos.x = Mathf.Round(pos.x) + 0.5f;
            pos.y = Mathf.Round(pos.y) - 0.5f;
            pos.z = 0;
            transform.position = pos;
        }
    }

    private void Update()
    {
        if(GameManager.i.currency < GameManager.i.selectedBuildingCost)
        {
            IncorrectColor();
        }
        if (GameManager.i.currency >= GameManager.i.selectedBuildingCost && IsNotColliding)
        {
            CorrectColor();
        }
    }
    public bool IsNotColliding
    {
        get { return collisionCount == 0; }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //9:Prism
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            IncorrectColor();
            canPlaceBuilding = false;
        }
        //11:NoBuildingZone
        else if (other.gameObject.layer == 11)
        {
            IncorrectColor();
            canPlaceBuilding = false;
        }
        collisionCount++;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //9:Prism 12:Terminal
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            IncorrectColor();
            canPlaceBuilding = false;
        }
        //11:NoBuildingZone
        else if (other.gameObject.layer == 11)
        {
            IncorrectColor();
            canPlaceBuilding = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //9:Prism
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            CorrectColor();
            canPlaceBuilding = true;
        }
        else if (other.gameObject.layer == 11)
        {
            CorrectColor();
            canPlaceBuilding = true;
        }
        collisionCount--;
    }

    void CorrectColor()
    {
        buildingSprite.color = correctColor;
        circle.color = correctColor;
    }
    void IncorrectColor()
    {
        buildingSprite.color = incorrectColor;
        circle.color = incorrectColor;
    }
}
