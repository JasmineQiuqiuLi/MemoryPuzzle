using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    PuzzleBoard puzzleBoard;

    public bool isPlacedOnBoard;

    public Vector2 currCellPos;
    public Vector2 corretCellPos;
    Vector3 piecePosOnBoard;

    Vector3 initialPos;
    Quaternion initialRot;

    private bool isTouch;

    private void Awake()
    {
        isPlacedOnBoard = false;
        puzzleBoard = GameObject.Find("PuzzleBoard(Clone)").GetComponent<PuzzleBoard>();

        initialPos = transform.position;
        initialRot = transform.rotation;

        isTouch = false;

        currCellPos = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);
    }


    public void HandleDrop()
    {

        //if the puzzle piece is before the puzzle board, drop the puzzle piece on a specific pos on the board
        Vector3 forwDropPoint = Raycast(-transform.forward, 10.0f, "PuzzleBoard");
        if (forwDropPoint.magnitude != float.PositiveInfinity)//if return infinive, means there are no puzzle board detected
        {
            HandlePieceDrop(forwDropPoint);
            //whenever a puzzle piece is dropped, the puzzle board will check if the puzzle is completed;
            puzzleBoard.CheckCompletion();
        }
        else
        {
            //if the puzzle piece is hidden by the puzzle board, send it the the original position;
            Vector3 backDropPoint = Raycast(transform.forward, 10.0f, "PuzzleBoard");
            if (backDropPoint.magnitude != float.PositiveInfinity) SendToOriginalPosition();

            //if the puzzlepiece collide with the puzzleboard, send to the original position
            if (isTouch) SendToOriginalPosition();

            //check if the puzzle piece is moved from the board;
            CheckMoveOffBoard();
        }




    }


    Vector3 Raycast(Vector3 dir, float distance, string name)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, dir, distance);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag(name))
                {
                    return hits[i].point;
                }
            }
        }
        return Vector3.positiveInfinity;

    }

    void HandlePieceDrop(Vector3 pos)
    {
        
        Vector2 cellPos = puzzleBoard.GetCellFromWorldPos(pos);

        //check that the cell is not taken
        if (!puzzleBoard.IsACellTaken(cellPos))
        {
            //position the piece at the center of the cell
            Vector3 newPos = puzzleBoard.TransferCellPosToWolrdPos(cellPos);

            transform.position = newPos;
            piecePosOnBoard = newPos;

            transform.forward = -puzzleBoard.transform.forward;

            isPlacedOnBoard = true;

            currCellPos = cellPos;
        }

    }

    void CheckMoveOffBoard()
    {
        if (isPlacedOnBoard && transform.position != piecePosOnBoard)
        {
            isPlacedOnBoard = false;
            currCellPos = new Vector2(Mathf.NegativeInfinity,Mathf.NegativeInfinity);
            
        }
    }

    void SendToOriginalPosition()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PuzzleBoard"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("PuzzleBoard"))
        {
            isTouch = false;
        }
    }

    public bool CheckCorrectness()
    {
        return corretCellPos == currCellPos;
    }



}
