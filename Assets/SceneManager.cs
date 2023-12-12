using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private SOBalls _soBalls;
    [SerializeField] private int ballIncrease;
    
    public GameObject ballPrefab;
    private LineRenderer _lineRenderer;

    

    public float addBallInterval;

    private float addBallTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _soBalls.balls.Clear();
        AddBall(20);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _soBalls.Hull = KonvexAlgorithm.JarvisAlgorithm(_soBalls.balls);
        Vector3[] positions = new Vector3[_soBalls.Hull.Count+1];
        //SortingAlgorithms.HeapSort(_soBalls.balls);
        for (int i = 0; i < _soBalls.Hull.Count; i++)
        {
            positions[i] = (_soBalls.Hull[i].transform.position);
        }

        positions[^1] = positions[0];

        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }

 
    public void AddBall( int amountOfBalls)
    {
        int currentCount = _soBalls.balls.Count;
        for (int i = currentCount; i < amountOfBalls + currentCount; i++)
        {
            _soBalls.balls.Add(Instantiate(ballPrefab, Vector3.zero, Quaternion.identity).GetComponent<BallBehavior>());
            _soBalls.balls[i].direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0).normalized;
            
            while(_soBalls.balls[i].direction == Vector3.zero)
            {
                _soBalls.balls[i].direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0).normalized;
            }
        }
    }
}
