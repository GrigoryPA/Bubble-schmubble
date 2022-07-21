using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace JumperGame
{
    public class LowerBorder : MonoBehaviour
    {
        public UnityEvent<GameObject> onPlatformCollided;
        public UnityEvent onPlayerCollided;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("Platform"))
            {
                onPlatformCollided.Invoke(collision.gameObject);
            }
            if (collision.gameObject.name.Contains("Player"))
            {
                onPlayerCollided.Invoke();
            }
        }
    }
}
