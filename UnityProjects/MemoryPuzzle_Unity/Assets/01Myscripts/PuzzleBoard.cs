using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UX;

public class PuzzleBoard : MonoBehaviour
{
    float width;
    float height;
    public float pieceSize;
    float depth;
    public Dialog DialogPrefabLarge;

    public event Action OnCompleted;
    public event Action<string, string> Onfailed;

    public string header;
    public string description;

    PanelMessage failedMessage = new PanelMessage();

    public static PuzzleBoard instance;

    private void Awake()
    {
        //width = cols * pieceSize;
        //height = rows * pieceSize;
        height = GameManager.instance.CurrPicture.height;
        width = GameManager.instance.CurrPicture.width;

        failedMessage.header = "Try It Again";
        failedMessage.description = "Whoops! This is not the original picture. Try it again, you are almost there!";

        instance = this;
    }

    public Vector2 GetCellFromWorldPos(Vector3 Pos)
    {
        //get the local point
        Vector3 localPoint = transform.InverseTransformPoint(Pos);
        localPoint = Vector3.Scale(localPoint, transform.localScale);
        depth = localPoint.z;

        //get the colum (column number starts from 0)
        float column = Mathf.Floor((localPoint.x + width / 2) / pieceSize);

        //get row (row number starts from 0)
        float row = Mathf.Floor((localPoint.y + height / 2) / pieceSize);
        
        return new Vector2(column, row);
    }

    public Vector3 TransferCellPosToWolrdPos(Vector2 cell)
    {
        // go from cell's col,row --> local point
        float x = (-width / 2 + pieceSize / 2 + cell.x * pieceSize) / transform.localScale.x;
        float y = (-height / 2 + pieceSize / 2 + cell.y * pieceSize) / transform.localScale.y;

        Vector3 localPoint = new Vector3(x, y, depth);

        // go from local point --> global point
        Vector3 globalPoint = transform.TransformPoint(localPoint);

        return globalPoint;


    }

    //check whether a cell is taken or not
    public bool IsACellTaken(Vector2 cell)
    {
        GameObject puzzlePieceCollections = GameObject.Find("PuzzlePieces");
        PuzzlePiece[] puzzlePieces = puzzlePieceCollections.GetComponentsInChildren<PuzzlePiece>();
        //Debug.Log("puzzlePieces count" + puzzlePieces.Length);

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            //if we find a piece in there, it's taken
            if (puzzlePieces[i].isPlacedOnBoard == true && puzzlePieces[i].currCellPos == cell)
            {
                print("cell is taken");
                return true;
            }
        }
        return false;
    }

    public void CheckCompletion()
    {
        GameObject puzzlePieceCollections = GameObject.Find("PuzzlePieces");
        PuzzlePiece[] puzzlePieces = puzzlePieceCollections.GetComponentsInChildren<PuzzlePiece>();
        //bool isComplete = true;
        bool isCorrect = true;
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (!puzzlePieces[i].isPlacedOnBoard) return;

            isCorrect = isCorrect && puzzlePieces[i].CheckCorrectness();
        }

        if (isCorrect)
        {
            OnCompleted?.Invoke();
            
        }
        else Onfailed?.Invoke(failedMessage.header, failedMessage.description);
    }


    private void OnEnable()
    {
        OnCompleted += Win;
    }

    private void OnDisable()
    {
       OnCompleted -= Win;
    }

    private void Win()
    {
        //update the dictionary to set the isPuzzleCompleted to true;
        GameManager.instance.SetPuzzleCompleted(GameManager.instance.CurrPicture);

        //direct the user to the origianl home scene.
        OpenChoiceDialogSmall();
    }

    
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void OpenChoiceDialogSmall()
    {
        Dialog myDialog = Dialog.InstantiateFromPrefab(DialogPrefabLarge, new DialogProperty("Choice Dialog, Small, Near", "Congrats! You have completed this jigsaw puzzle. Let's go back to add a frame to this photo", DialogButtonHelpers.OK), true, true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    void OnClosedDialogEvent(DialogProperty property)
    {
        if(property.ResultContext.ButtonType == DialogButtonType.OK)
        {
            StartCoroutine(LoadYourAsyncScene());
            //activate the frames
            FramesController.instance.SetFramesStatus(true);
        }
        
    }

    
}
