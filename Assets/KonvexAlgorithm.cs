using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class KonvexAlgorithm
{
    enum OrientationStates
    {
        Clockwise,
        CounterClockwise,
        Collinear
    }
    /*
    public static List<BallBehavior> JarvisAlgorithm(List<BallBehavior> balls)
    {
        if (balls.Count < 3)
        {
            Debug.LogError("Big NoNo");
            return new List<BallBehavior>();
        }

        List<BallBehavior> temp = new List<BallBehavior>();

        //Find leftmost point
        int leftIndex = 0;
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].transform.position.x < balls[leftIndex].transform.position.x)
            {
                leftIndex = i;
            }
        }
        
        int previousPoint = leftIndex;
        int currentPoint;
        int kaka = 0;
        do
        {
            kaka++;
            //temp.Add(balls[previousPoint]);
            currentPoint = (previousPoint + 1) % balls.Count;
            for (int i = 0; i < balls.Count; i++)
            {
                if (Orientation(balls[previousPoint], balls[i], balls[currentPoint]) ==
                    2)
                {
                    Debug.Log("a");
                    currentPoint = i;
                }
            }

            previousPoint = currentPoint;
        } while (previousPoint != leftIndex|| kaka > 100);
        
        return temp;
    }
    
    

    private static int Orientation(BallBehavior ball1, BallBehavior ball2, BallBehavior ball3)
    {
        var position = ball2.transform.position;
        var position1 = ball1.transform.position;
        var position2 = ball3.transform.position;
        float temp =
            (position.y - position1.y) * (position2.x - position.x) -
            (position.x - position1.x) - (position2.y - position.y);
        if (temp == 0)
            return 0;
        return (temp > 0) ? 1 : 2;
    }*/

    private static OrientationStates Orientation(BallBehavior Ball1, BallBehavior Ball2, BallBehavior Ball3)
    {

        var pos1 = Ball1.transform.position;
        var pos2 = Ball2.transform.position;
        var pos3 = Ball3.transform.position;
        
        //2D cross product
        float product = (pos3.y - pos2.y) * (pos2.x - pos1.x) - (pos2.y - pos1.y) * (pos3.x - pos2.x);
            
            
            //(pos1.y - pos2.y) * (pos3.x - pos1.x) -
              //          (pos1.x - pos2.x) * (pos3.y - pos1.y);

        return product switch
        {
            > 0 => OrientationStates.CounterClockwise,
            < 0 => OrientationStates.Clockwise,
            _ => OrientationStates.Collinear
        };
    }
    

    public static List<BallBehavior> JarvisAlgorithm(List<BallBehavior> points)
    {

        if (points.Count< 3) return null;
        
         List<BallBehavior> hull = new List<BallBehavior>();



        // Find the leftmost point 

        int leftMostPoint = 0;

        for (int i = 1; i < points.Count; i++)
            if (points[i].transform.position.x < points[leftMostPoint].transform.position.x)
                leftMostPoint = i;


        int previousPoint = leftMostPoint; 
        int currentPoint;

        do
        {

            hull.Add(points[previousPoint]);

            currentPoint = (previousPoint + 1) % points.Count;

            for (int i = 0; i < points.Count; i++)

            {
                if (Orientation(points[previousPoint], points[currentPoint], points[i]) == OrientationStates.Clockwise)

                    currentPoint = i;
            }

            previousPoint = currentPoint;
            
        } while (previousPoint != leftMostPoint);

        return hull;
    }


    public static List<BallBehavior> GrahamScan(List<BallBehavior> points)
    {
        List<BallBehavior> hull = new List<BallBehavior>();
        // find lägsta till vänster punkten
        int firstBall = 0;
        
        for (int i = 1; i < points.Count; i++)
            if (points[i].transform.position.y < points[firstBall].transform.position.y || 
                (points[i].transform.position.y == points[firstBall].transform.position.y && points[i].transform.position.x < points[firstBall].transform.position.x)) 
                firstBall = i;

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 delta = points[i].transform.position - points[firstBall].transform.position;
            points[i].angle = Mathf.Atan2(delta.y, delta.x);
        }
        
        
        
        hull.Add(points[firstBall]);


        return hull;
    }
    
    public static void InsertSort(List<BallBehavior> balls)
    {
        //https://en.wikipedia.org/wiki/Insertion_sort
        for (int i = 0; i < balls.Count; i++)
        {
            int j = i;
            while (j > 0 && balls[j-1].angle > balls[j].angle)
            {
                (balls[j], balls[j-1]) = (balls[j-1], balls[j]);
                j--;
            }
        }
    }
}
