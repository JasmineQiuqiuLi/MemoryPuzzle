using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzlePieceSpawner : MonoBehaviour
{
    int height;
    int width;
    string path;

    private Object[] textures;

    public GameObject puzzlePiecePrefab;


    private void Awake()
    {
        height = GameManager.instance.CurrPicture.height;
        width = GameManager.instance.CurrPicture.width;
        path = GameManager.instance.CurrPicture.path;

        textures = Resources.LoadAll(path, typeof(Texture2D));
    }

    private void Start()
    {
        CreatePuzzlePieces();
    }

    private void CreatePuzzlePieces()
    {
        GameObject PuzzlePiece = new GameObject("PuzzlePieces");

        for (int i = 0; i < textures.Length; i++)
        {
            
            GameObject newPuzzlePiece = Instantiate(puzzlePiecePrefab, new Vector3(0,2,1.5f), puzzlePiecePrefab.transform.rotation);
            newPuzzlePiece.transform.parent = PuzzlePiece.transform;
            Transform[] transforms = newPuzzlePiece.GetComponentsInChildren<Transform>();
            GameObject quad = transforms[2].gameObject;

            Renderer rend = quad.GetComponent<MeshRenderer>();
            Texture2D texture = (Texture2D)textures[i];
            rend.material.mainTexture = texture;

            
            Vector2 cellPos = new Vector2(i % width , height - Mathf.Floor(i / width) - 1);
          
            PuzzlePiece puzzlePiece = newPuzzlePiece.GetComponent<PuzzlePiece>();
            puzzlePiece.corretCellPos = cellPos;
        }

    }
}
