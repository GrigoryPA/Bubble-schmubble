using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumperGame
{
    public enum PlatformType
    {
        Simple,
        Force,
        Moving
    }

    public class Platform : MonoBehaviour
    {
        public PlatformType type;
        [Header("For all platforms:")]
        public float jumpForce = 600f;
        [Header("For moving platform:")]
        [Range(0, 1)]
        public float speed;

        private Vector2 size;

        private void OnEnable()
        {
            size = GetComponent<BoxCollider2D>().size;
        }

        private void FixedUpdate()
        {
            switch (type)
            {
                case PlatformType.Moving:
                    if ((transform.position.x <= (Map.leftBorder + size.x/2) && speed < 0) 
                        || (transform.position.x >= (Map.rightBorder - size.x / 2) && speed > 0))
                    {
                        speed *= -1;
                    }
                    else
                    {
                        transform.position = transform.position + Vector3.right * speed;
                    }
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb2d.velocity.y <= 0)
                {
                    switch (type)
                    {
                        case PlatformType.Simple:
                            GivingImpulse(rb2d);
                            break;
                        case PlatformType.Force:
                            GivingImpulse(rb2d);
                            break;
                        case PlatformType.Moving:
                            GivingImpulse(rb2d);
                            break;
                    }
                }
            }
        }

        public void GivingImpulse(Rigidbody2D rb2d)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
