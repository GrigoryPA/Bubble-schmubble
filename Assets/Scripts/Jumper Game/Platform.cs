using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumperGame
{
    public enum PlatformType
    {
        simple,
        force
    }

    public class Platform : MonoBehaviour
    {
        public PlatformType type;
        public float jumpForce = 600f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb2d.velocity.y <= 0)
                {
                    rb2d.AddForce(Vector2.up * jumpForce);
                }
            }
        }
    }

}
