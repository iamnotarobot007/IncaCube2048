//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.UI;

//public class Shooting : MonoBehaviour
//{
//    PlayerControls _controls;
//    Camera cam;
//  // Vector3 newPosition;
//    [SerializeField] private float movementTime = 5f;

//    public Rigidbody _cubeRbd;
//    public int _shootForce;
  

//    float Deltadiff;
//    float Deltadifclamp;
//    Vector3 dragStartPosition = Vector3.zero;
//    Vector3 dragCurrentPosition = Vector3.zero;

//    //  private Vector3 startingPosition;
//    private Cube mainCube;
//    Vector3 cubePos;

//    private void Start()
//    {
//      //  newPosition = transform.position;
//        cam = Camera.main;
//        mainCube = this.GetComponent<Cube>();
//        // SpawnNewCube();
//       // SpawnCube();


//    }

//    private void Awake()
//    {
//        _controls = new PlayerControls();
//    }

//    private void Update()
//    {
//        ApplyMovements();
//    }

//    private void OnEnable()
//    {
//        _controls.Player.Enable();
//        _controls.Player.DragandMove.started += DragAndMove;
//        _controls.Player.DragandMove.performed += DragAndMove;
//        _controls.Player.DragandMove.canceled += DragAndMove;
//    }

//    private void OnDisable()
//    {
//        _controls.Player.Disable();
//        _controls.Player.DragandMove.started -= DragAndMove;
//        _controls.Player.DragandMove.performed -= DragAndMove;
//        _controls.Player.DragandMove.canceled -= DragAndMove;
//    }

//    public void DragAndMove(InputAction.CallbackContext context)
//    {
//        string buttonControlPath = "/Mouse/leftButton";

//        if (context.started)
//        {
//            if (context.control.path == buttonControlPath)
//            {
//                Debug.Log("Button Pressed Down Event - called once when button pressed");

//                Ray dragStartRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
//                Plane dragStartPlane = new Plane(Vector3.up, Vector3.zero);
//                float dragStartEntry;

//                if (dragStartPlane.Raycast(dragStartRay, out dragStartEntry))
//                {
//                    dragStartPosition = dragStartRay.GetPoint(dragStartEntry);
//                }
//            }
//        }
//        else if (context.performed)
//        {
//            if (context.control.path == buttonControlPath)
//            {
//                Debug.Log("Button Hold Down - called continuously until the button is pressed");

//                Ray dragCurrentRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
//                Plane dragCurrentPlane = new Plane(Vector3.up, Vector3.zero);
//                float dragCurrentEntry;

//                if (dragCurrentPlane.Raycast(dragCurrentRay, out dragCurrentEntry))
//                {
//                    dragCurrentPosition = dragCurrentRay.GetPoint(dragCurrentEntry);
//                    Deltadiff = transform.position.x + dragCurrentPosition.x - dragStartPosition.x;
//                }
//            }
//        }
//        else if (context.canceled)
//        {
//            if (context.control.path == buttonControlPath)
//            {
//                Debug.Log("Button released");
//                Debug.Log("SHOOT");
//               // startingPosition = transform.position;
//                _cubeRbd.AddForce(new Vector3(0, 0, _shootForce));
//               // CubeSpawner.Instance.CubeSpawn(startingPosition);
//               Invoke("SpawnNewCube",0.3f);
//                this.enabled = false;

                
               
//            }
//        }
//    }
//    private void SpawnNewCube()
//    {
//        mainCube.IsMainCube = false;
//        SpawnCube();
//    }
//    private void SpawnCube()
//    {
//        Debug.Log("Shooting clas Spawning");
//        mainCube = CubeSpawner.Instance.SpawnRandom();
//        mainCube.IsMainCube = true;
//        cubePos = mainCube.transform.position;
//    }


//    private void ApplyMovements()
//    {
//        Deltadifclamp = Mathf.Clamp(Deltadiff, -3, 3);
//        transform.position = Vector3.Lerp(transform.position, new Vector3(Deltadifclamp, transform.position.y, transform.position.z), movementTime * Time.deltaTime);
//    }
//}
