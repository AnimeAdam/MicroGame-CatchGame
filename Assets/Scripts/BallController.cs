using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //Ball Properties
    private const float rotateSpeed = 900f; //How fast it spins.
    private const float shrinkingSpeed = 0.2f; //Speed the ball shrinks when thrown.
    private const float minimumSize = 0.13f; //Minimum size for the ball to be for the glove to catch it.
        
    //Flight path properties
    public GameObject flightPath;
    private Transform[] flightPathTargets;
    public float[] flightPathSpeeds;
    private int currentFlightPathTarget;
    private List<(float time, Transform transform)> flightPathCollection = new List<ValueTuple<float, Transform>>(); //Contains the flight path and time
    public GameObject target; //End target point
    private const float finalBallspeed = 2f; //Speed the ball moves at the final target.

    // Start is called before the first frame update
    void Start()
    {
        SelectTargetRandomLoc();
        InitialiseFlightPath();
        BeginNextFlightPath();
    }

    // Update is called once per frame
    void Update()
    {
        SpinningBall();
        ShrinkInSize();
        DestoryBallFromShrinking();
    }

    //Spins the ball automatically.
    private void SpinningBall()
    {
        transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime, Space.Self);
    }

    //Shrinks the ball as it's thrown
    private void ShrinkInSize()
    {
        if (transform.localScale.x > 0.001f)
            transform.localScale -= shrinkingSpeed * Time.deltaTime * Vector3.one;
    }
    
    #region FlightPath

    //Randomly sets the location of the target
    private void SelectTargetRandomLoc()
    {
        float targetX = UnityEngine.Random.Range(-7.5f, 7.5f);
        float targetY = UnityEngine.Random.Range(-5f, 5f);
        target.transform.position = new Vector2(targetX, targetY);
    }

    //Setup the flight path to follow, starts at 0 because that is the parent, will be incremented at nextFlightPath
    private void InitialiseFlightPath()
    {
        flightPathTargets = flightPath.GetComponentsInChildren<Transform>();
        currentFlightPathTarget = 0;

        if (flightPathTargets.Length != flightPathSpeeds.Length)
        {
            Debug.Log("There isn't enough targets for speeds in flight path at " + gameObject.name);
            Debug.Break();
        }
        for(int i = 0; i < flightPathTargets.Length; i++)
        {
            flightPathCollection.Add(new ValueTuple<float, Transform>(flightPathSpeeds[i], flightPathTargets[i]));
        }
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
        BeginNextFlightPath();
        yield break;
    }    
    
    //Simplified tweening movement animation.
    public IEnumerator MoveObjectAtTimeOnce(float duration, Vector3 target)
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
        yield break;
    }


    //Call the next flight path target and the last one.
    private void BeginNextFlightPath()
    {
        currentFlightPathTarget++;
        if (currentFlightPathTarget != flightPathCollection.Count)
        {
            StartCoroutine(MoveObjectAtTime(
                flightPathCollection[currentFlightPathTarget].time
                , flightPathCollection[currentFlightPathTarget].transform.position));
        }
        else
        {
            StartCoroutine(MoveObjectAtTimeOnce(finalBallspeed, target.transform.position)); //FINAL target for the ball to reach.
        }
    }

    #endregion FlightPath

    //Ball goes too far away so it's best to destory it.
    private void DestoryBallFromShrinking()
    {
        if (transform.localScale.x < 0.01f)
            Destroy(gameObject);
    }

    //When the ball is catche it will call endgame functions.
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && transform.localScale.x < minimumSize)
        {
            FindAnyObjectByType<GameManager>().BallWasCatched();
            FindAnyObjectByType<PlayerController>().PlayCatchAnimation();
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }

    //Step 3: Destory after it's either too small or hits the glove.
    //Step 4: IF hit the glove then glove changes to catch animation.


}
