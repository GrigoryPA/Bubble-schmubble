using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public bool isSelected;
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public bool isNeedNewSprite = false;
    [HideInInspector]
    public Vector2 size;

    private void Awake()
    {
        size = GetComponent<BoxCollider2D>().size;
    }
}
