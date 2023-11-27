using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;

    [SerializeField] private Cube mainCube;
    private bool isDragging;

    private Vector3 dragStartPosition;
    private bool isInputDelayActive = true;


    public bool isGameStarted = false;

    private Vector2 previousTouchPosition;

    [SerializeField] private LineRenderer pathLineRenderer;

    public float _sensi = 1;
    public int TimeDelay = 1000;

    public Canvas _ShopPopup;
    public int Bombcount = 0;
    public int RainBowcount = 0;

    public TextMeshProUGUI[] BombPowerUpCount;
    public TextMeshProUGUI[] RainBowPowerUpCount;

    public bool usingPowerUp = false;
    



    public void OnPlayButtonClick()
    {
        if (!isGameStarted)
        {
            isInputDelayActive = false;
            TurnInputONonstart();
            isGameStarted = true;
            SpawnCube();
            Score.inst.ResetScore();
            // Debug.Log("Game Started");
        }
    }

    private void Start()
    {
        UpdatePowerupCountBomb();
        UpdatePowerupCountRainbow();

    }


    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isGameStarted = false;
        }
        else
        {
            isGameStarted = true;
        }
        if (Input.touchCount > 0)
        {
            // Check if finger is over a UI element

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {

                isGameStarted = false;
            }
            else
            {
                isGameStarted = true;
            }
        }
        if (isGameStarted)
            InputHandler();
    }

    public void ChangeCubeBomb()
    {

        if (!isInputDelayActive && Bombcount >= 1 && !usingPowerUp)
        {
            usingPowerUp = true;
            mainCube.SetCube(CubeType.bomb);
            mainCube.CubeUpdate();
            Bombcount--;
            UpdatePowerupCountBomb();
            SoundManager.inst.PlaySound(SoundName.ButtonClick);
        }

    }

    public void ChangeCubeRainbow()
    {
        if (!isInputDelayActive && RainBowcount >= 1 && !usingPowerUp)
        {
            usingPowerUp = true;
            mainCube.SetCube(CubeType.Rainbow);
            mainCube.CubeUpdate();
            RainBowcount--;
            UpdatePowerupCountRainbow();
            SoundManager.inst.PlaySound(SoundName.ButtonClick);
        }

    }

    public void UpdatePowerupCountBomb()
    {
        foreach (TextMeshProUGUI text in BombPowerUpCount)
        {
            text.text = Bombcount.ToString();
        }
    }

    public void UpdatePowerupCountRainbow()
    {
        foreach (TextMeshProUGUI text in RainBowPowerUpCount)
        {
            text.text = RainBowcount.ToString();
        }
    }

    private void InputHandler()
    {
        if (isInputDelayActive)
            return;

        if (Input.touchCount > 0)
        {
            MobileInput();
        }
        else
        {
            MouseClick();
        }
    }


    private void MobileInput()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                isDragging = true;
                dragStartPosition = GetTouchWorldPosition(touch.position);
                // dragCurrentPosition = dragStartPosition;

                pathLineRenderer.enabled = true;
                pathLineRenderer.positionCount = 2;
                pathLineRenderer.SetPosition(0, mainCube.transform.position);
                pathLineRenderer.SetPosition(1, mainCube.transform.position + mainCube.transform.forward * 16f);

                previousTouchPosition = touch.position; // Store the initial touch position
                break;
            case TouchPhase.Moved:
                if (isDragging)
                {
                    Vector2 touchPosition = touch.position;
                    float deltaDiff = touchPosition.x - previousTouchPosition.x;
                    MoveCube(deltaDiff * _sensi); // Multiply deltaDiff by _sensi for smoother movement

                    pathLineRenderer.positionCount = 2;
                    pathLineRenderer.SetPosition(0, mainCube.transform.position);
                    pathLineRenderer.SetPosition(1, mainCube.transform.position + mainCube.transform.forward * 16f);

                    previousTouchPosition = touchPosition; // Update the previous touch position
                }
                break;
            case TouchPhase.Ended:
                if (isDragging)
                {
                    isDragging = false;
                    ShootCube();
                    pathLineRenderer.enabled = false;
                    TurnInputON();
                    usingPowerUp = false;
                }
                break;
        }
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = GetMouseWorldPosition();
            // dragCurrentPosition = dragStartPosition;

            pathLineRenderer.enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            float deltaDiff = GetMouseDelta().x;
            MoveCube(deltaDiff);

            pathLineRenderer.positionCount = 2;
            pathLineRenderer.SetPosition(0, mainCube.transform.position);
            pathLineRenderer.SetPosition(1, mainCube.transform.position + mainCube.transform.forward * 16f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                ShootCube();
                pathLineRenderer.enabled = false;
                TurnInputON();
                usingPowerUp = false;
            }
        }
    }


    private void MoveCube(float deltaDiff)
    {
        float newXPosition = Mathf.Clamp(mainCube.transform.position.x + deltaDiff, -cubeMaxPosX, cubeMaxPosX);
        mainCube.transform.position = new Vector3(newXPosition, mainCube.transform.position.y, mainCube.transform.position.z);
    }

    private void ShootCube()
    {
        mainCube.CubeRb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
        Invoke(nameof(SpawnNewCube), .3f);
    }

    private Vector3 GetTouchWorldPosition(Vector2 touchPosition)
    {
        Vector3 touchPositionWithZ = new Vector3(touchPosition.x, touchPosition.y, -Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(touchPositionWithZ);
    }

    private Vector3 GetMouseDelta()
    {
        return new Vector3(Input.GetAxis("Mouse X"), 0, 0);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }


    private void SpawnNewCube()
    {

        SpawnCube();
        mainCube.isMainCube = false;
    }

    private void SpawnCube()
    {

        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.GetComponent<CubeCollision>().IsCollisionReady = false;
        mainCube.isMainCube = true;
    }
    private async void TurnInputON()
    {
        isInputDelayActive = true;
        await Task.Delay(TimeDelay); // Adjust the delay time (in milliseconds) as per your requirement.
        isInputDelayActive = false;

    }

    private async void TurnInputONonstart()
    {
        isInputDelayActive = true;
        await Task.Delay(1000); // Adjust the delay time (in milliseconds) as per your requirement.
        isInputDelayActive = false;
    }
}

