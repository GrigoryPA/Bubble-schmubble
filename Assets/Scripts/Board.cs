using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int xSize = 2, ySize = 2;
    public Ball ballPrefab;
    public List<Sprite> ballSprites;

    public bool isShifting = false;
    public Ball selectedBall;
    public List<Ball> adjacentBalls;
    private static readonly Vector2[] dirRay = new Vector2[4] { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
    private Ball[,] ballsArray;
    public bool isSearchEmptyBall = false;


    public void CreateBoard()
    {
        ballsArray = new Ball[xSize, ySize];
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

                List<Sprite> spritesPool = new List<Sprite>();
                spritesPool.AddRange(ballSprites);
                //исключаем повтор€ющиес€ цвета по горизонтали и вертикали (не более двух подр€д)
                if (i > 0)
                {
                    spritesPool.Remove(ballsArray[i - 1, j].spriteRenderer.sprite);
                }
                if (j > 0)
                {
                    spritesPool.Remove(ballsArray[i, j - 1].spriteRenderer.sprite);
                }

                //рандомим цвет из поулченного пула цветов
                newBall.spriteRenderer.sprite = spritesPool[Random.Range(0, spritesPool.Count)];
            }
        }
    }

    public void SwapTwoBall(Ball ball1, Ball ball2)
    {
        if (ball1.spriteRenderer.sprite == ball2.spriteRenderer.sprite)
        {
            return;
        }
        else
        {
            Sprite bufSprite = ball2.spriteRenderer.sprite;
            ball2.spriteRenderer.sprite = ball1.spriteRenderer.sprite;
            ball1.spriteRenderer.sprite = bufSprite;
        }
    }

    public void SelectBall(Ball ball)
    {
        ball.isSelected = true;
        ball.spriteRenderer.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        selectedBall = ball;
        adjacentBalls = FindAdjacentBalls(selectedBall);
    }

    public void DeselectBall(Ball ball)
    {
        ball.isSelected = false;
        ball.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        selectedBall = null;
        adjacentBalls = null;
    }

    private List<Ball> FindAdjacentBalls(Ball ball)
    {
        List<Ball> neighbors = new List<Ball>(4);

        for (int i = 0; i < dirRay.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, dirRay[i]);
            if (hit != false)
            {
                neighbors.Add(hit.collider.gameObject.GetComponent<Ball>());
            }
        }

        return neighbors;
    }

    private List<Ball> FindMatch(Ball ball, Vector2 dir)
    {
        List<Ball> matchList = new List<Ball>();
        Ball bufBall;

        RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, dir);
        while (hit != false && (bufBall = hit.collider.GetComponent<Ball>()).spriteRenderer.sprite == ball.spriteRenderer.sprite)
        {
            matchList.Add(bufBall);
            hit = Physics2D.Raycast(hit.collider.transform.position, dir);
        }

        return matchList;
    }

    private bool DeleteBalls(Ball ball, Vector2[] dirArray)
    {
        List<Ball> deletingBalls = new List<Ball>();
        for (int i = 0; i < dirArray.Length; i++)
        {
            deletingBalls.AddRange(FindMatch(ball, dirArray[i]));
        }

        if (deletingBalls.Count >= 2)
        {
            foreach (Ball delBall in deletingBalls)
            {
                delBall.spriteRenderer.sprite = null;
            }
            return true;
        }

        return false;
    }

    public void FindAllMatch(Ball ball)
    {
        if (ball.isEmpty)
        {
            return;
        }
        else
        {
            if (DeleteBalls(ball, new Vector2[2] { Vector2.up, Vector2.down })
                || DeleteBalls(ball, new Vector2[2] { Vector2.right, Vector2.left }))
            {
                ball.spriteRenderer.sprite = null;
                isSearchEmptyBall = true;
            }
        }
    }

    public void SearchEmptyBalls()
    {
        //isShifting = true;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (ballsArray[i, j].isEmpty)
                {
                    ShiftBallDown(i, j);
                    break;
                }
            }
        }

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                FindAllMatch(ballsArray[i, j]);
            }
        }
        //isShifting = false;
        //isSearchEmptyBall = false;
    }

    private void ShiftBallDown(int iPos, int jPos)
    {
        List<SpriteRenderer> bufRen = new List<SpriteRenderer>();
        for (int j = jPos; j < ySize; j++)
        {
            if (ballsArray[iPos, j].isEmpty)
            {
                bufRen.Add(ballsArray[iPos, j].spriteRenderer);
            }
        }
        SetNewSprite(iPos, bufRen);
    }

    private void SetNewSprite(int iPos, List<SpriteRenderer> bufRen)
    {
        for (int j = 0; j < bufRen.Count - 1; j++)
        {
            bufRen[j].sprite = bufRen[j + 1].sprite;
            bufRen[j + 1].sprite = GetNewSprite(iPos, ySize - 1);
        }
    }

    private Sprite GetNewSprite(int iPos, int jPos)
    {
        List<Sprite> spritesPool = new List<Sprite>();
        spritesPool.AddRange(ballSprites);

        if (iPos > 0)
        {
            spritesPool.Remove(ballsArray[iPos - 1, jPos].spriteRenderer.sprite);
        }
        if (iPos < xSize - 1)
        {
            spritesPool.Remove(ballsArray[iPos + 1, jPos].spriteRenderer.sprite);
        }
        if (jPos > 0)
        {
            spritesPool.Remove(ballsArray[iPos, jPos - 1].spriteRenderer.sprite);
        }

        return spritesPool[Random.Range(0, spritesPool.Count)];
    }
}
