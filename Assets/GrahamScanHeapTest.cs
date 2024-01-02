using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrahamScanHeapTest : BaseTest
{
    // Start is called before the first frame updat

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    
        if (HasGoneOverTheTime) return;
        List<BallBehavior> copyBallList = new List<BallBehavior>(ballsSO.balls);
        float startTime = Time.realtimeSinceStartup;
        ballsSO.Hull = KonvexAlgorithm.GrahamScanHeap(ballsSO.balls);
        float endTime = Time.realtimeSinceStartup;
        float deltaTime = endTime - startTime;
        updateTimes.Add(deltaTime);
        float newAverage = updateTimes.Sum() / updateTimes.Count;
        if (newAverage >= maxTimePerFixedUpdate)
        {
            hasGoneOverTime.Invoke(sortingName);
            UpdateLists();
            HasGoneOverTheTime = true;
        }
    }
}
