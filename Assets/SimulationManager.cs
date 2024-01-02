using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SimulationManager : MonoBehaviour
{
    public bool sortHasFailed;

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
    }

    private void EndSimulation()
    {
        SimulationHasEnded.Invoke();
    }
    
    private void CheckSortFailed(string sort)
    {
        EndSimulation();
    }
}
