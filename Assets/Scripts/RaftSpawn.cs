using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftSpawn : CarSpawn
{
    //private struct Raft
    //{
    //    public GameObject raftGameObject;

    //    public int lane; // 0 or 1
    //    public float speed;
    //}


    //[SerializeField] GameObject[] raftPrefabs;

    //[SerializeField] private float maxSpeed = 5.0f;
    //[SerializeField] private float minSpeed = 2.0f;


    //private const int NUM_RAFTS_IN_ROAD = 2;


    protected override Car SpawnCar(int carIndex, int lane)
    {
        Car newCar = base.SpawnCar(carIndex, lane);
        newCar.carGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        return newCar;
    }   

    protected override void MoveCar(Car car)
    {

        if (car.lane == 0)
        {
            base.MoveCar(car);
        }
        else
        {
            car.carGameObject.transform.Translate(Vector3.right * Time.deltaTime * car.speed * (-1));

        }
    }
}
