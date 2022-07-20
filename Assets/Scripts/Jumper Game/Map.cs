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
        [Space]
        public GameObject movingPlatform;
        [Range(0, 1)]
        public float chanceMovingPlt;

        public static float leftBorder = -3;
        public static float rightBorder = 3;

        private Dictionary<PlatformType, Queue<GameObject>> platformsPool = new Dictionary<PlatformType, Queue<GameObject>>();
        private GameObject highestPlatform;

        private const int poolMaxSize = 40;

        private void Awake()
        {
            leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 10)).x;
        }

        private void OnEnable()
        {
            //Инициализировать необходимые переменные
            platformsPool.Add(PlatformType.Simple, new Queue<GameObject>());
            platformsPool.Add(PlatformType.Force, new Queue<GameObject>());
            platformsPool.Add(PlatformType.Moving, new Queue<GameObject>());
            GameObject go;
            for (int i = 0; i < poolMaxSize; i++)
            {
                go = Instantiate(simpleAndStartPlatform, platformHome.position, Quaternion.identity, transform);
                go.name += " - " + i.ToString();
                go.SetActive(false);
                platformsPool[PlatformType.Simple].Enqueue(go);

                go = Instantiate(forcePlatform, platformHome.position, Quaternion.identity, transform);
                go.name += " - " + i.ToString();
                go.SetActive(false);
                platformsPool[PlatformType.Force].Enqueue(go);

                go = Instantiate(movingPlatform, platformHome.position, Quaternion.identity, transform);
                go.name += " - " + i.ToString();
                go.SetActive(false);
                platformsPool[PlatformType.Moving].Enqueue(go);
            }

            highestPlatform = simpleAndStartPlatform;

            //Подписаться на нужные события
            lowerBorder.onPlatformCollided.AddListener(MovePlatformToEndQueue);
        }

        private void Start()
        {
            //ВЫСТАВИТЬ ПЛАТФОРМЫ
            //первая под игроком она же префаб, ее не двигаем
            //остальные выставляем по алгоритму рандома из видоса
            for (int i = 0; i < 15; i++)
            {
                CreateNewPlatform();
            }
        }

        private void MovePlatformToEndQueue(GameObject platform)
        {
            platformsPool[platform.GetComponent<Platform>().type].Enqueue(platform);

            platform.transform.position = platformHome.position;
            platform.SetActive(false);
            CreateNewPlatform();
        }

        private void CreateNewPlatform()
        {
            float highestY = highestPlatform.transform.position.y;
            
            int rndPlt = Random.Range(0, 100);
            if (rndPlt < chanceForcePlt * 100) //Сильная платорма
            {
                highestPlatform = platformsPool[PlatformType.Force].Dequeue();
            }
            else if (rndPlt < (chanceForcePlt + chanceMovingPlt) * 100) //Сильная платорма
            {
                highestPlatform = platformsPool[PlatformType.Moving].Dequeue();
            }
            else //Обычная платформа
            {
                highestPlatform = platformsPool[PlatformType.Simple].Dequeue();
            }

            float pltWidth = highestPlatform.GetComponent<BoxCollider2D>().size.x;
            Vector3 rndPos = new Vector3(Random.Range(leftBorder + pltWidth / 2, rightBorder - pltWidth / 2), highestY + Random.Range(rangeStep.x, rangeStep.y), 0.0f);
            highestPlatform.transform.position = rndPos;
            highestPlatform.SetActive(true);
        }
    }
}
