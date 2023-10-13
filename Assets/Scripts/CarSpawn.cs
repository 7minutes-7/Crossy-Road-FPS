using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    protected struct Car
    {
        public GameObject carGameObject;

        public int lane; // 0 or 1
        public float speed;
    }

    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float minSpeed = 2.0f;
    [SerializeField] GameObject[] carPrefabs;

    private const int NUM_CARS_IN_ROAD = 2;
    private const int CAR_LAYER = 8;

    public bool[] isCarInRoad = { false, false };// Spawn two cars per road
    private Car[] carSpawned = new Car[2];

    private float spawnStartX = 15f;
    private float laneLengthZ = 1.0f;
    [SerializeField] private float spawnStartY = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCar(Random.Range(0, carPrefabs.Length), 0);
        SpawnCar(Random.Range(0, carPrefabs.Length), 1);
    }

    // Update is called once per frame
    void Update()
    {
        //// Move spawned cars
        for (int i = 0; i < NUM_CARS_IN_ROAD; i++)
        {
            if (isCarInRoad[i] && carSpawned[i].carGameObject != null)
            {
                if (carSpawned[i].carGameObject.transform.position.x >= -spawnStartX &&
                    carSpawned[i].carGameObject.transform.position.x <= spawnStartX)
                {
                    MoveCar(carSpawned[i]);
                }
                else
                {
                    DestroyCar(carSpawned[i]);
                    SpawnCar(Random.Range(0, carPrefabs.Length), i);
                }
            }
        }
    }

    protected virtual Car SpawnCar(int carIndex, int lane)
    {
        //Debug.Log("spawning car at lane" + lane.ToString());
        Vector3 spawnPoint = transform.position + new Vector3(spawnStartX * (2 * lane - 1), spawnStartY, laneLengthZ*(2*lane-1)); // local coordunate to road

        GameObject car = Instantiate(carPrefabs[carIndex], spawnPoint, transform.rotation);

        car.transform.rotation *= Quaternion.Euler(0, 180 * lane, 0);
        //car.transform.parent = gameObject.transform;
        car.transform.SetParent(transform,true);
        Car newCar = new Car();
 
        newCar.carGameObject = car;
        newCar.lane = lane;
        newCar.speed = Random.Range(minSpeed, maxSpeed); // lane 0 : speed(+) lane 1: speed(-)


        isCarInRoad[lane] = true;
        carSpawned[lane] = newCar;

        return newCar;
    }

    private void DestroyCar(Car car)
    {
        //GameObject target = car.carGameObject;
        isCarInRoad[car.lane] = false;
        Destroy(car.carGameObject);
    }

    protected virtual void MoveCar(Car car)
    {
        car.carGameObject.transform.Translate(Vector3.right * Time.deltaTime * car.speed);
    }

}
