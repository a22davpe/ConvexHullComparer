using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GrahamScanInsertTest : BaseTest
{
    // Start is called before the first frame update

    // Update is called once per frame


    public override void Update()
    {
        base.Update();
        if (HasGoneOverTheTime) return;
        float startTime = Time.realtimeSinceStartup;
        ballsSO.Hull = KonvexAlgorithm.GrahamScanInsert(ballsSO.balls);
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
