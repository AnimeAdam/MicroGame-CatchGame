using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float rotateSpeed = 700f; //How fast it spins
    private const float scalingFactor = 10f; //How small the ball gets as it's moving
    private const float speed = 10f; //Speed the ball moves at

    
    public GameObject target; //End target point
    
    //Flight path of the ball
    public GameObject flightPath;
    private Transform[] flightPathTargets;
    private int currentFlightPathTarget;


    // Start is called before the first frame update
    void Start()
    {
        SelectTargetRandomLoc();
        //TODO: Add a function that iterates through the flight paths starting at 1 ending at the length
        InitialiseFlightPath();
        BeginNextFlightPath();

        //Starts at 1 because 0 is the parent.
        // StartCoroutine(MoveObjectAtTime(5f, flightPathTargets[1].position));
    }

    private void BeginNextFlightPath()
    {
        currentFlightPathTarget++;
        if (currentFlightPathTarget != flightPathTargets.Length)
        {
            StartCoroutine(MoveObjectAtTime(5f, flightPathTargets[currentFlightPathTarget].position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpinningBall();
    }

    //Spins the ball automatically.
    private void SpinningBall()
    {
        transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime, Space.Self);
    }

    //Randomly sets the location of the target
    private void SelectTargetRandomLoc()
    {
        float targetX = Random.Range(-7.5f, 7.5f);
        float targetY = Random.Range(-5f, 5f);
        target.transform.position = new Vector2(targetX, targetY);
    }

    //Setup the flight path to follow
    private void InitialiseFlightPath()
    {
        flightPathTargets = flightPath.GetComponentsInChildren<Transform>();
        currentFlightPathTarget = 1;
    }

    //Simplified tweening movement animation.
    public IEnumerator MoveObjectAtTime(float duration, Vector3 target)
    {
        // float duration = timeOfArrival; // - Time.time
        float distance = Vector3.Distance(transform.position, target);
        float speed = distance / duration;
 
        while (transform.position != target)
        {
            //Not sure about the Time.deltaTime, maybe try without and tweak it to see what results you get
            transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
            //wait for the frame to finish calculating
            yield return null;
        }
        //stops the coroutine
        Invoke("BeginNextFlightPath", 1.0f);
        yield break;
    }

    //Step 1: Animate swirl coming in, with scaling down.
    //Step 2: Mid-point flys to target location.
    //Step 3: Destory after it's either too small or hits the glove.
    //Step 4: IF hit the glove then glove changes to catch animation.


}
