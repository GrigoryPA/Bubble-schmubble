using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int xSize = 2, ySize = 2;
    public Ball ballPrefab;
    public List<Color> ballColors;

    public void CreateBoard()
    {
        Ball[,] ballsArray = new Ball[xSize, ySize];
        Vector3 centerPos = transform.position;
        Vector3 ballSize = ballPrefab.size;

        Vector2 iPos;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                iPos = centerPos + new Vector3(ballSize.x * i, ballSize.y * j, 0);
                Ball newBall = Instantiate(ballPrefab, iPos, Quaternion.identity, transform);
                ballsArray[i, j] = newBall;

                List<Color> colorsPool = new List<Color>();
                colorsPool.AddRange(ballColors);
                //исключаем повтор€ющиес€ цвета по горизонтали и вертикали (не более двух подр€д)
                if (i > 1 && ballsArray[i - 1, j].sprite.color == ballsArray[i - 2, j].sprite.color)
                {
                    colorsPool.Remove(ballsArray[i - 1, j].sprite.color);
                }
                if (j > 1 && ballsArray[i, j - 1].sprite.color == ballsArray[i, j - 2].sprite.color)
                {
                    colorsPool.Remove(ballsArray[i, j - 1].sprite.color);
                }

                //рандомим цвет из поулченного пула цветов
                newBall.sprite.color = colorsPool[Random.Range(0, colorsPool.Count)];
            }
        }
    }
}
