using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayerController : MonoBehaviour
{
    [Range(0, 50)]
    public float jumpForce = 10f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb2d.AddForce(Vector2.up * jumpForce);
        }
    }
}
