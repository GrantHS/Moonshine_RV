using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Array of car prefabs
    public Transform spawnPoint; // The empty GameObject's transform where cars will spawn
    public Transform endPoint; // The empty GameObject's transform where cars will end the lap
    public List<Transform> waypoints; // List of transforms for the car to follow around the track
    private int currentCarIndex = 0; // Index to track the current car
    private GameObject currentCar; // Reference to the current car
    private int currentWaypointIndex = 0; // Index to track the current waypoint

    private void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true) // infinite loop to keep spawning cars
        {
            if (currentCar == null) // if there is no current car, spawn a new one
            {
                currentCar = Instantiate(carPrefabs[currentCarIndex % carPrefabs.Length], spawnPoint.position, Quaternion.identity);
                currentWaypointIndex = 0; // reset the waypoint
                MoveToNextWaypoint(currentCar); // this starts moving the car

                currentCarIndex++; //add the index for the car
            }
            yield return null
        }
    }

    void MoveToNextWaypoint(GameObject car)
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            NavMeshAgent agent = car.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.destination = waypoints[currentWaypointIndex].position;
            }
        }
        else
        {
            // after the last waypoint, set the car to move to the end point
            NavMeshAgent agent = car.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.destination = endPoint.position;
            }
        }
    }

    void Update()
    {
        if (currentCar != null)
        {
            //this checks if the car has reaches the curreny waypoint
            if (HasReachedDestination(currentCar))
            {
                if (currentWaypointIndex < waypoints.Count)
                {
                    currentWaypointIndex++;
                    MoveToNextWaypoint(currentCar);
                }
                else
                {
                    // if the car has no more waypoints, check if it has reached the end point
                    if (HasReachedDestination(currentCar, true))
                    {
                        Destroy(currentCar);
                        currentCar = null; // this spawns the next car after it's reached the endpoint
                    }
                }
            }
        }
    }

    bool HasReachedDestination(GameObject car, bool isEndPoint = false)
    {
        NavMeshAgent agent = car.GetComponent<NavMeshAgent>();
        // Check if we're close to the destination and if the agent is not calculating a new path
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // If we're at the end point, make sure we're actually close enough to the end point position
            if (isEndPoint && Vector3.Distance(car.transform.position, endPoint.position) > agent.stoppingDistance)
                return false;

            return true;
        }
        return false;
    }
}
