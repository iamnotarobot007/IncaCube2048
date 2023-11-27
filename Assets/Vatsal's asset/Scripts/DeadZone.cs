using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    bool gameEnd = true;
    public static DeadZone inst;
    private void Awake()
    {
        inst = this;
    }

    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();
        if (cube != null)
        {
            gameEnd = true;
            if (!cube.isMainCube && cube.CubeRb.velocity.magnitude < .1f && gameEnd)
            {
                gameEnd = false;
                Debug.Log("Gameover");
                UIManager.instance.SwitchScreen(ScreenType.Gameover);
                CubeSpawner.Instance.Resetcubes();
                CameraController.instance.TurnCamera(Quaternion.Euler(0, 0, 0), 1);

            }

        }
    }
}
