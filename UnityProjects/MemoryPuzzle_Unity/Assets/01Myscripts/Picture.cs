using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine.SceneManagement;
using System;

public class Picture : MonoBehaviour
{
    //transfer it to private after complete the coding
    public bool isPuzzleCompleted;

    public Dialog DialogPrefabLarge;

    //used for add photo frame;
    public Material material;

    private FrameMenu frameMenu;

    //get the width of the image
    private Mesh mesh;
    float offset;

    public int width;
    public int height;
    public string path;

    private void Awake()
    {
        isPuzzleCompleted = false;
      
        frameMenu = gameObject.GetComponent<FrameMenu>();

        mesh = GetComponentInChildren<MeshFilter>().mesh;
        offset = mesh.bounds.size.x*transform.localScale.x;
        offset /= 2;

    }

    private void Start()
    {
        if (GameManager.PuzzleCompleteStatus.Count != 0)
        {
            foreach(var picStatus in GameManager.PuzzleCompleteStatus)
            {
                if (picStatus.Key == path)
                {
                    bool result;
                    if (GameManager.PuzzleCompleteStatus.TryGetValue(picStatus.Key, out result))
                    {
                        isPuzzleCompleted = result;
                       
                    }

                    bool status;
                    if(GameManager.PictureStatus.TryGetValue(picStatus.Key,out status))
                    {
                        //if the frame has been added, the origianl photo will not be shown.
                        gameObject.SetActive(status);
                    }
                }
            }
        }
    }

    public void HandleClick()
    {
        if (!isPuzzleCompleted)
        {
            OpenChoiceDialogSmall();
        }
        else if (isPuzzleCompleted)
        {
            
            Vector3 menuOffset = new Vector3(offset, 0, 0);
            frameMenu.HandleClick(transform,transform.position+menuOffset);

        }
    }

    public void OpenChoiceDialogSmall()
    {
        Dialog myDialog = Dialog.InstantiateFromPrefab(DialogPrefabLarge, new DialogProperty("Choice Dialog, Large, Near", "Do you want to play a jigsaw puzzle made from this photograph?", DialogButtonHelpers.YesNo), true, true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    private void OnClosedDialogEvent(DialogProperty property)
    {
        if (property.ResultContext.ButtonType == DialogButtonType.No)
        {
            //Do nothing
        }else if (property.ResultContext.ButtonType == DialogButtonType.Yes)
        {
            //Load the scene to complete the Jigsaw puzzle;
            StartCoroutine(LoadYourAsyncScene());

            //hide the frames that have been created, otherwise, the frames will be appeared in the newChallenge scene
            FramesController.instance.SetFramesStatus(false);

            //save the Picture that has been clicked;
            GameManager.instance.SetCurrPuzzle(this);
           
        }
    }

    public void HidePicture()
    {
        gameObject.SetActive(false);
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("NewChallenge");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
