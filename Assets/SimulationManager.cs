using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public bool insertSortFailed;
    public bool gnomeSortFailed;
    public bool heapSortFailed;

    public static Action SimulationHasEnded = delegate {};
    public static Action updateListsNow = delegate {};
    private void OnEnable()
    {
        BaseTest.hasGoneOverTime += CheckSortFailed;
    }

    private void OnDisable()
    {
        BaseTest.hasGoneOverTime -= CheckSortFailed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(insertSortFailed && gnomeSortFailed && heapSortFailed)
            EndSimulation();
    }

    private void EndSimulation()
    {
        SimulationHasEnded.Invoke();
    }
    
    private void CheckSortFailed(string sort)
    {
        Debug.Log(sort);
        switch (sort)
        {
            case "HeapSort":
                heapSortFailed = true;
                break;
            case "GnomeSort":
                gnomeSortFailed = true;
                break;
            case "InsertSort":
                insertSortFailed = true;
                break;
            default:
                Debug.LogError("ahhhhh");
                break;
        }
    }
}
