using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BaseTest : MonoBehaviour
{
    [SerializeField] protected string filePath;
    [SerializeField] protected string sortingName;
    [SerializeField] protected SOBalls ballsSO;
    [SerializeField] protected int numberOfUpdates;
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] protected float maxTimePerFixedUpdate;
    protected bool HasGoneOverTheTime;

    private bool hasBeenUpdatedThisFrame;
    public static Action<string> hasGoneOverTime;

    public List<float> updateTimes;

    public List<float> AverageUpdateTime;

    public List<int> AmountOfBalls;
    [SerializeField] private int nextSceneIndex;
    
    // Start is called before the first frame update

    private void OnEnable()
    {
        SimulationManager.SimulationHasEnded += WriteInDocument;
        SimulationManager.updateListsNow += UpdateLists;
    }
    
    private void OnDisable()
    {
        SimulationManager.SimulationHasEnded -= WriteInDocument;
        SimulationManager.updateListsNow -= UpdateLists;
    }

    protected virtual void Start()
    {
        numberOfUpdates = 0;
        updateTimes = new List<float>();
        AverageUpdateTime = new List<float>();
        AmountOfBalls = new List<int>();
    }

    public virtual void Update()
    {
        numberOfUpdates++;
        if (numberOfUpdates>100)
        {
            IncreaseAmountOfBalls();
        }
    }


    protected void WriteInDocument()
    {
        Debug.Log("end");
        using (StreamWriter writer = new StreamWriter(filePath + sortingName + "NumberOfBalls.txt"))
        {
            for (int i = 0; i < AmountOfBalls.Count; i++)
            {
                writer.Write(AmountOfBalls[i] + ";");
            }
        }

        using (StreamWriter writer = new StreamWriter(filePath + sortingName + "AverageUpdateTime.txt"))
        {
            for (int i = 0; i < AverageUpdateTime.Count; i++)
            {
                writer.Write(AverageUpdateTime[i] + ";");
            }
        }

        if (nextSceneIndex < 0)
        {
            Application.Quit();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void IncreaseAmountOfBalls()
    {
        SimulationManager.updateListsNow.Invoke();

        numberOfUpdates = 0;
        _sceneManager.AddBall(HowManyBallsShouldBeAdded());
    }

    private int HowManyBallsShouldBeAdded()
    {
        int amountOfBalls = ballsSO.balls.Count;
        switch (amountOfBalls)
        {
            case < 100:
                return 10;
            case < 1000:
                return 100;
            case < 5000:
                return 200;
            case < 10000:
                return 500;
            case < 15000:
                return 1000;
            case <30000:
                return 5000;
            default:
                return 10000;
        }
    }

    protected void UpdateLists()
    {
        if (HasGoneOverTheTime) return;
        float newAverage = updateTimes.Sum() / updateTimes.Count;
        AverageUpdateTime.Add(newAverage*1000);
        updateTimes.Clear();
        AmountOfBalls.Add(ballsSO.balls.Count);
    }
}
