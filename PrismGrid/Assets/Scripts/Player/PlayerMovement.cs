using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movement;
    Vector2 mousePos;

    [Header("Animation")]
    public SpriteRenderer spriteRendererRef;
    public Sprite idleSprite;
    public SpriteAnimation spriteAnimation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (movement.y > 0)
        {
            spriteRendererRef.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (movement.y < 0)
        {
            spriteRendererRef.transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else if (movement.x < 0)
        {
            spriteRendererRef.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        else if (movement.x > 0)
        {
            spriteRendererRef.transform.localEulerAngles = new Vector3(0, 0, 270);
        }

        //Animation
        if (movement.x == 0 && movement.y == 0)
        {
            spriteRendererRef.sprite = idleSprite;
            spriteAnimation.isPaused = true;
        }
        else
        {
            spriteAnimation.isPaused = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
    }
}
