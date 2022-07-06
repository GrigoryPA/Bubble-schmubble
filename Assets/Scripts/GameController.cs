using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Board board;
    public bool BUTTON_UPDATE = false;



    private void Start()
    {
        //Генерируем стартовую доску
        board.CreateBoard();
    }

    private void Update()
    {
        if (BUTTON_UPDATE)
        {
            BUTTON_UPDATE = !BUTTON_UPDATE;
            board.CreateBoard();
        }

        board.SearchEmptyBalls();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit != false)
            {
                InteractionWithBall(hit.collider.gameObject.GetComponent<Ball>());
            }
        }
    }

    private void InteractionWithBall(Ball ball)
    {
        if (ball.isEmpty || board.isShifting)
        {
            return;
        }
        else
        {
            if (ball.isSelected)
            {
                board.DeselectBall(ball);
            }
            else
            {
                if (board.selectedBall == null)
                {
                    board.SelectBall(ball);
                }
                else
                {
                    if (board.adjacentBalls.Contains(ball))
                    {
                        board.SwapTwoBall(board.selectedBall, ball);
                        board.FindAllMatch(board.selectedBall);
                        board.FindAllMatch(ball);
                        board.DeselectBall(board.selectedBall);
                    }
                    else
                    {
                        board.DeselectBall(board.selectedBall);
                        board.SelectBall(ball);
                    }
                }
            }
        }
    }
}
