using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumperController : MonoBehaviour
{
    [Range(0, 10)]
    public float speed;
    
    private Rigidbody2D rb2d;
    private float moveInputHorizontal;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(speed * moveInputHorizontal, rb2d.velocity.y);
    }
}
