using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{

    public static CubeSpawner Instance;

    List<Cube> cubelist = new List<Cube>();

    Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int queueCapacity = 20;
    [SerializeField] private bool autoAdd = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber;

    private int maxPower = 15;

    private Vector3 defaultSpawnPosition;

    public void Awake()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        SetCubesQueue();
    }

    public void Resetcubes()
    {
        foreach (Cube cube in cubelist)
        {
            DestroyCube1(cube);
        }
    }
   

    private void SetCubesQueue()
    {
        for (int i = 0; i < queueCapacity; i++)
            AddToQueue();
    }

    private void AddToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
                                .GetComponent<Cube>();


        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
         cubelist.Add(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoAdd)
            {
                queueCapacity++;
                AddToQueue();
            }
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.CubeRb.velocity = Vector3.zero;
        cube.CubeRb.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.CubeRb.constraints = RigidbodyConstraints.FreezeRotationX;
        cube.GetComponent<CubeCollision>().IsCollisionReady = false;
        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
      
    }
    public void DestroyCube1(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }


    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }

    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}
