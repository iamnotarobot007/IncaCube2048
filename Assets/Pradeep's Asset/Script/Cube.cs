using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CubeType
{
    normal,
    bomb,
    Rainbow
}


public class Cube : MonoBehaviour
{
    [SerializeField] private TMP_Text[] numbersText;
    static int staticID = 0;
    public int CubeID;
    public Color CubeColor;
    public int CubeNumber;
    public Rigidbody CubeRb;
    public bool isMainCube;
    public CubeType cubeType;

    //public MeshRenderer MeshRenderer;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Color mainColor;

    //bomb cube
    public Color BombColor;
    //rainbow cube
    public Color RainbowColor;

   
    private void Awake()            
    {
        CubeID = staticID++;
        CubeRb.constraints = RigidbodyConstraints.FreezeRotationX;
    }

    public void SetColor(Color color)
    {
      
        CubeColor = color;
        skinnedMeshRenderer.material.color = color;
       // MeshRenderer.material.color = color;
    }

    public void SetNumber(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            
            numbersText[i].enabled = true;
            numbersText[i].text = number.ToString();
        }
    }

    public void SetNumberoff()
    {
        for (int i = 0; i < 6; i++)
        {
            numbersText[i].enabled = false;
        }
    }

    public void SetCube(CubeType cType)
    {
        cubeType = cType;
      
    }
    
    public void CubeUpdate()
    {
        switch (cubeType)
        {
            case CubeType.normal:
                Debug.Log("Currently cube is : Normal");
                SetColor(mainColor);
                SetNumber(CubeNumber);
                break;

            case CubeType.bomb:
                Debug.Log("Currently cube is : Bomb");
                SetColor(BombColor);
                SetNumberoff();
                break;

            case CubeType.Rainbow:
                Debug.Log("Currently cube is : Rainbow");
                SetColor(RainbowColor);
                SetNumberoff();
                break;

            default:
                break;
        }
    }

}
