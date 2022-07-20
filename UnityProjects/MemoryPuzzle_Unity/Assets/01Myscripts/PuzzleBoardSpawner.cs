using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoardSpawner : MonoBehaviour
{
    int width;
    int height;
    public GameObject puzzleBoardPrefab;
    GameObject puzzleBoard;

    private void Awake()
    {
        height = GameManager.instance.CurrPicture.height;
        width = GameManager.instance.CurrPicture.width;

        puzzleBoard = Instantiate(puzzleBoardPrefab, puzzleBoardPrefab.transform.position, puzzleBoardPrefab.transform.rotation);
        Vector3 scale = new Vector3(width, height, puzzleBoard.transform.localScale.z);
        puzzleBoard.transform.localScale = scale;

    }
}
