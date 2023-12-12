using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeapSortTest : BaseTest
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    public override void LateUpdate()
    {
        base.LateUpdate();
        if (HasGoneOverTheTime) return;
        float startTime = Time.realtimeSinceStartup;
        //SortingAlgorithms.HeapSort(ballsSO.balls);
        //ballsSO.Hull = KonvexAlgorithm.JarvisAlgorithm(ballsSO.balls);
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
