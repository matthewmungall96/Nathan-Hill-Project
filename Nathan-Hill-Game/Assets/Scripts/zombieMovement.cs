using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class zombieMovement : MonoBehaviour
{
    //Navmesh agent to set the target destination
    public NavMeshAgent zombieAgent;

    //Empty object that holds all the possible locations
    public GameObject pointCollectionParent;

    //List to avoid too much accessing in the memory
    private List<Transform> pointsCollection;

    //Reference to point targeted 
    private int pointIndex;

    //Flag up if player detected
    private bool chasing;

    //Reference to the player's position
    public Transform player;
    

    //Time set of flee if takes damage
    private float fleeTime;

    private void Start()
    {
        //Initialization of the list
        pointsCollection = new List<Transform>();

        //Access and storage of the possible destinations (transform preferred to keep their names available, could be replaced by hash maps)
        foreach(Transform child in pointCollectionParent.transform)
        {
            pointsCollection.Add(child);
        }

        //Initialization of the index
        pointIndex = 0;

        //Set destination to the first point
        zombieAgent.SetDestination(pointsCollection[pointIndex].position);

        //Initialization of the chase flag
        chasing = false;

        //Initialization of the fleeing timer
        fleeTime = 0.0f;

    }


    // Update is called once per frame
    void Update()
    {

        //If flee mode is on, keeps fleeing
        if (fleeTime > 0)
        {
            //Sets a temporary index
            int tmpIndex = 0;
            
            //Decreases counter
            fleeTime -= Time.deltaTime;

            //Loop from 0 to number of points
            for (int i = 0; i < pointsCollection.Count; i++)
                {
                //If point number i is farther than last farthest point, replaces farthest point number
                if (Vector3.Distance(player.position, pointsCollection[i].position) > Vector3.Distance(player.position, pointsCollection[tmpIndex].position))
                    tmpIndex = i;
            }
            //Sets as destination the farthest point
            zombieAgent.SetDestination(pointsCollection[tmpIndex].position);
            
        }
        //If player detected starts chasing the player, placed in update loop because the player may move
        else if (chasing)
        {
            zombieAgent.SetDestination(player.position);
        }
        //If neither flees or chases, keeps the routine pattern
        else
        {
            zombieAgent.SetDestination(pointsCollection[pointIndex].position);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the zombie reaches the point she was targeting (identified by its name which is not relevant), proceeds to next point of the list
        if (other.gameObject.name == pointsCollection[pointIndex].gameObject.name)
        {
            pointIndex = (pointIndex+1) % pointsCollection.Count;
        }
        //If the zombie encounters the player, raises the flag to start the chase
        else if (other.tag == "Player" )
        {
            chasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player runs away passed the collider's distance, resets the chase flag
        if (other.tag == "Player")
            chasing = false;
    }

   
    
    //If called without damage argument, will start fleeing without inflicting damages0
    public void TakesDamage(int damage = 0)
    {
        //If first call, sets the timer to 5 (theoretical) seconds
        fleeTime = 5.0f;
        
        //TODO
        //Apply damages
    }

}
