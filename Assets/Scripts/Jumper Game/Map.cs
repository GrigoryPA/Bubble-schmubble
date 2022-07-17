using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumperGame
{
    public class Map : MonoBehaviour
    {
        public LowerBorder lowerBorder;
        public Transform platformHome;
        public Vector2 rangeStep = new Vector2(1, 2);
        [Space]
        public GameObject simpleAndStartPlatform;
        [Space]
        public GameObject forcePlatform;
        [Range(0, 1)]
        public float chanceForcePlt;

        private float leftBorder;
        private float rightBorder;
        private Queue<GameObject> simplePlatformPool;
        private Queue<GameObject> forcePlatformPool;
        private GameObject highestPlatform;

        private const int poolMaxSize = 20;

        private void OnEnable()
        {
            //Инициализировать необходимые переменные
            leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 10)).x;

            simplePlatformPool = new Queue<GameObject>(poolMaxSize);
            forcePlatformPool = new Queue<GameObject>(poolMaxSize);
            for (int i = 0; i < poolMaxSize; i++)
            {
                simplePlatformPool.Enqueue(Instantiate(simpleAndStartPlatform, platformHome.position, Quaternion.identity));
                forcePlatformPool.Enqueue(Instantiate(forcePlatform, platformHome.position, Quaternion.identity));
            }

            highestPlatform = simpleAndStartPlatform;

            //Подписаться на нужные события
            lowerBorder.OnPlatformCollided.AddListener(MovePlatformToEndQueue);
        }

        private void Start()
        {
            //ВЫСТАВИТЬ ПЛАТФОРМЫ
            //первая под игроком она же префаб, ее не двигаем
            //остальные выставляем по алгоритму рандома из видоса
            for (int i = 0; i < 5; i++)
            {
                CreateNewPlatform();
            }
        }

        private void MovePlatformToEndQueue(GameObject platform)
        {
            switch (platform.GetComponent<Platform>().type)
            {
                case PlatformType.simple:
                    simplePlatformPool.Enqueue(platform.gameObject);
                    break;

                case PlatformType.force:
                    forcePlatformPool.Enqueue(platform.gameObject);
                    break;
            }

            platform.transform.position = platformHome.position;
            CreateNewPlatform();
        }

        private void CreateNewPlatform()
        {
            float highestY = highestPlatform.transform.position.y;
            Vector3 rndPos = new Vector3(Random.Range(leftBorder, rightBorder), highestY + Random.Range(rangeStep.x, rangeStep.y), 0.0f); 
            int rndPlt = Random.Range(0, 100);

            if (rndPlt < chanceForcePlt * 100) //Сильная платорма
            {
                highestPlatform = forcePlatformPool.Dequeue();
            }
            else //Обычная платформа
            {
                highestPlatform = simplePlatformPool.Dequeue();
            }

            highestPlatform.transform.position = rndPos;
        }
    }
}
