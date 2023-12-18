using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SimulationManager : MonoBehaviour
{
    public bool jarvisHull;
    public bool grahamScanInsert;
    public bool grahamScanHeap;

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
        if(jarvisHull && grahamScanInsert && grahamScanHeap)
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
                grahamScanHeap = true;
                break;
            case "GnomeSort":
                grahamScanInsert = true;
                break;
            case "InsertSort":
                jarvisHull = true;
                break;
            default:
                Debug.LogError("ahhhhh");
                break;
        }
    }
}
