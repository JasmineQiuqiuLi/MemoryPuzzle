using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintCanvas : MonoBehaviour
{
    public static HintCanvas instance;
    Image hintImage;
    


    private void Awake()
    {
        instance = this;
        hintImage = GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }

    public void SetCanvasStatus(bool status)
    {
        gameObject.SetActive(status);

        if (status)
        {
            string pathString = "HintImage/" + GameManager.instance.CurrPicture.path;

            var texture = Resources.Load<Texture2D>(pathString);

            var newSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one * 0.5f);

            hintImage.sprite = newSprite;
        }
    }


}
