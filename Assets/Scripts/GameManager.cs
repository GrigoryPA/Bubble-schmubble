using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public bool BUTTON_UPDATE = false;

    private void Start()
    {
        board.CreateBoard();
    }

    private void Update()
    {
        if (BUTTON_UPDATE)
        {
            BUTTON_UPDATE = !BUTTON_UPDATE;
            board.CreateBoard();
        }
    }
}
