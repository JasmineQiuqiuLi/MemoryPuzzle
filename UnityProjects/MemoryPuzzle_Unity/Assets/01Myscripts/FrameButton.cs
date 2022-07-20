using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameButton : MonoBehaviour
{
    public GameObject framePrefb;
    private GameObject quad;

    public void HandleClick()
    {
        //get the parent Picture component
        Picture picture = GetComponentInParent<Picture>();

        //the position of the new frame
        Vector3 pos = picture.gameObject.transform.position;
        Vector3 scale = picture.gameObject.transform.localScale;

        GameObject frame = Instantiate(framePrefb, pos, framePrefb.transform.rotation);

        //change the scale of the frame
        frame.transform.localScale = scale*2;

        //change the material of the quad, so the photo on the frame is the photo clicked
        quad = frame.transform.GetChild(1).gameObject;
        //use the material
        Renderer rend = quad.GetComponent<MeshRenderer>();
        rend.material = picture.material;

        //set the Frames as the parent of frame, so that all the frames could be hide/show together
        Frames.instance.SetFramesAsParent(frame);

        //destroy the frame menu;
        //get the parent frame menu
        FrameMenu frameMenu = gameObject.GetComponentInParent<FrameMenu>();
        frameMenu.DestroyFrameMenu();

        //update the status of this origianl photo after it has been added a frame.
        GameManager.instance.SetPictureStatus(picture);

        //hide the origianl plain photo;
        picture.HidePicture();
    }



}
