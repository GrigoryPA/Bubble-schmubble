using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Vector2 size;
    public bool isSelected;
    public bool isEmpty => spriteRenderer.sprite == null;
    public bool isMoving = false;
    public bool isNeedNewSprite = false;
}
