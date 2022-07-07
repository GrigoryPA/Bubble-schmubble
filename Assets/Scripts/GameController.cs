using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Board board;
    public bool swapMode = false;



    private void Start()
    {
        //√енерируем стартовую доску
        board.CreateBoard();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 15, board.ballsLayerMask) ;
            if (hit != false)
            {
                InteractionWithBall(hit.collider.gameObject.GetComponent<Ball>());
            }
        }
    }

    private void InteractionWithBall(Ball ball)
    {
        if (!swapMode)
        {
            //режим игры BLOWUP
            board.BlowUpBall(ball);
        }
        else
        {
            //режим игры SWAP 
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
                        //board.FindAllMatch(board.selectedBall);
                        //board.FindAllMatch(ball);
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
