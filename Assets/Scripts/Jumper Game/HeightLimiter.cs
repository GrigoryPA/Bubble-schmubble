using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumperGame
{
    public class HeightLimiter : MonoBehaviour
    {
        public GameObject map;
        public new GameObject camera;
        public GameObject player;
        public float heightLimit;

        private int count = 0;

        private void Start()
        {
            camera.GetComponent<CameraFollower>().SetHeight(heightLimit, count);
        }

        // Update is called once per frame
        void Update()
        {
            if (player.transform.position.y >= heightLimit)
            {
                camera.transform.position = camera.transform.position - Vector3.up * heightLimit;
                Transform[] platforms = map.GetComponentsInChildren<Transform>();
                for (int i = 1; i < platforms.Length; i++)
                {
                    platforms[i].position = platforms[i].position - Vector3.up * heightLimit;
                }
                player.transform.position = player.transform.position - Vector3.up * heightLimit;

                camera.GetComponent<CameraFollower>().SetHeight(heightLimit, ++count);
            }
        }
    }
}
