using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Vector2 size;
    public bool isSelected;
    public bool isEmpty => spriteRenderer == null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
