using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JumperGame
{
    public class JumperController : MonoBehaviour
    {
        [Range(0, 10)]
        public float speed;

        private Rigidbody2D rb2d;
        private float moveInputHorizontal;
        private float leftBorder;
        private float rightBorder;
        //private float centerLine;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.Sleep();
        }

        private void OnEnable()
        {
            //Инициализировать необходимые переменные
            leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 10)).x;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            moveInputHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(speed * moveInputHorizontal, rb2d.velocity.y);

            if (transform.position.x > rightBorder)
            {
                transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
            }
            if (transform.position.x < leftBorder)
            {
                transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
            }
        }

        public void RevivePlayer()
        {
            transform.position = transform.position + Vector3.up * Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight, 10)).y * 0.5f;
        }
    }
}
