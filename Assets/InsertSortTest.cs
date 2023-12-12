using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InsertSortTest : BaseTest
{
    // Start is called before the first frame update

    // Update is called once per frame
    public void Update()
    {
        if (HasGoneOverTheTime) return;
        List<BallBehavior> copyBallList = new List<BallBehavior>(ballsSO.balls);
        float startTime = Time.realtimeSinceStartup;
        //SortingAlgorithms.InsertSort(copyBallList);
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
    
    public override void LateUpdate()
    {
        
    }
}
