using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumperGame
{
    public class CameraFollower : MonoBehaviour
    {
        public Transform playerT;

        private void Start()
        {
            transform.position = new Vector3(transform.position.x, playerT.position.y, transform.position.z);
        }

        // Update is called once per frame
        private void Update()
        {
            if (playerT.position.y > transform.position.y)
            {
                transform.position += Vector3.up * (playerT.position.y - transform.position.y);
            }
        }
    }
}
