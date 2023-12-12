using System;
using System.Collections;
using System.Collections.Generic;
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

    private static OrientationStates Orientation(BallBehavior po, BallBehavior qo, BallBehavior ro)
    {

        var q = po.transform.position;
        var p = qo.transform.position;
        var r = ro.transform.position;
        float val = (q.y - p.y) * (r.x - q.x) -

                    (q.x - p.x) * (r.y - q.y);



        switch (val)
        {
            case >0:
                return OrientationStates.Clockwise;
            case <0:
                return OrientationStates.CounterClockwise;
            default:
                return OrientationStates.Collinear;
        }
    }
    

    public static List<BallBehavior> JarvisAlgorithm(List<BallBehavior> points)
    {
        int n = points.Count;
        
        if (n < 3) return null;
        
         List<BallBehavior> hull = new List<BallBehavior>();



        // Find the leftmost point 

        int leftMostPoint = 0;

        for (int i = 1; i < n; i++)

            if (points[i].transform.position.x < points[leftMostPoint].transform.position.x)

                leftMostPoint = i;


        int previousPoint = leftMostPoint; 
        int currentPoint;

        do
        {

            hull.Add(points[previousPoint]);

            currentPoint = (previousPoint + 1) % n;

            for (int i = 0; i < n; i++)

            {
                if (Orientation(points[previousPoint], points[i], points[currentPoint]) == OrientationStates.CounterClockwise)

                    currentPoint = i;
            }

            previousPoint = currentPoint;
            
        } while (previousPoint != leftMostPoint);

        return hull;
    }
}
