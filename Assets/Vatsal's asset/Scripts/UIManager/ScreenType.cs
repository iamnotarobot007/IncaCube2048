using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    HomeScreen,
    Gameplay,
    Gameover

}

public class BaseScreen : MonoBehaviour
{

    public ScreenType screenType;
    [HideInInspector]
    public Canvas canvas;


    public void Awake()
    {
        canvas = GetComponent<Canvas>();
    }


}