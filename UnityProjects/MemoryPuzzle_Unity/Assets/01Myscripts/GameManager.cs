using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Linq;



public class GameManager : MonoBehaviour
{
   //save the status of each picture,if the puzzle made from this picture is completed, bool is true
    public static Dictionary<string,bool> PuzzleCompleteStatus= new Dictionary<string, bool>();

    //save the status of each picture, if a picture has been updated to a frame, bool is true
    public static Dictionary<string, bool> PictureStatus = new Dictionary<string, bool>();

    GameObject pictures;//empty parent objects of all the orinal pictures

    public static GameManager instance;

    //save the Picture reference which is clicked and will appear in the new challenge scene.
    //this variable pass the necessary info to the newChallenge scene.
    Picture currPicture;
    public Picture CurrPicture { get { return currPicture; } }
    

    static bool _created = false;//make sure the DontDestroyOnLoad only excuted once.

    private void Start()
    {
        instance = this;

        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;

            pictures = GameObject.Find("Pictures");

            for (int i = 0; i < pictures.transform.childCount; i++)
            {
                
                Picture picture = pictures.transform.GetChild(i).gameObject.GetComponent<Picture>();

                PuzzleCompleteStatus.Add(picture.path, false);
                PictureStatus.Add(picture.path, true);
            }
        }
        
    }

    public void SetPuzzleCompleted(Picture pic)
    {
        UpdateDictionary(pic, PuzzleCompleteStatus, true);
    }

    public void SetPictureStatus(Picture pic)
    {
        UpdateDictionary(pic, PictureStatus, false);
    }

    void UpdateDictionary(Picture pic, Dictionary<string,bool> dic,bool status)
    {
        List<string> stringKeys = new List<string>(dic.Keys);
        foreach (var key in stringKeys)
        {
            if (key == pic.path)
            {
                dic[key] = status;
            }
        }
    }

    //save the Picture reference who is clicked and will appear in the new challenge scene.
    //this variable pass the necessary info to the newChallenge scene.
    public void SetCurrPuzzle(Picture pic)
    {
        currPicture = pic;
    }
}
