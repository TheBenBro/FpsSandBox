using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class CheckerBoardTexture : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material material;
    Texture2D texture;
    [SerializeField] float width = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;    
        texture = new Texture2D(256,256, TextureFormat.RGBA32, true, true);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;
        material.SetTexture("_MainTex", texture);
        CreateCheckerBoard();
    }
    void CreateCheckerBoard()
    {
        for (int y =0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
               Color temp =  EvaluateCheckerBoardPixel(x, y);
                texture.SetPixel(x, y, temp);
            }
        }
        texture.Apply();
    }
    Color EvaluateCheckerBoardPixel(int x, int y)
    {
        float valueX = (x % (width*2f)) / (width * 2f);
        int vX = 1;
        if (valueX < 0.5)
        {
            vX = 0; 
        }
        float valueY = (y % (width * 2f)) / (width * 2f);
        int vY = 1;
        if (valueY < 0.5)
        {
            vY = 0;
        }
        float value = 0f;
        if(vX == vY)
        {
            value = 1f; 
        }
        return new Color(value, value, value);    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
