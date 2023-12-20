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

    public static List<BallBehavior> GrahamScanHeap(List<BallBehavior> points)
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
        
        HeapSort(points);
        hull.Add(points[0]);
        hull.Add(points[1]);
        for (int i = 2; i < points.Count; i++)
        {
            while (hull.Count >= 2 && OrientationStates.Clockwise!=Orientation(hull[^1],hull[^2],points[i]))
            {
                hull.RemoveAt(hull.Count-1);
            }
            hull.Add(points[i]);
        }


        return hull;
    }

    public static List<BallBehavior> GrahamScanInsert(List<BallBehavior> points)
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
        
        
        InsertSort(points);
        hull.Add(points[0]);
        hull.Add(points[1]);
        for (int i = 2; i < points.Count; i++)
        {
            while (hull.Count >= 2 && OrientationStates.Clockwise!=Orientation(hull[^1],hull[^2],points[i]))
            {
                hull.RemoveAt(hull.Count-1);
            }
            hull.Add(points[i]);
        }


        return hull;
    }
    
    private static void InsertSort(List<BallBehavior> balls)
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
    
    public static void HeapSort(List<BallBehavior> balls)
    {
        //https://en.wikipedia.org/wiki/Heapsort
        //https://fullyunderstood.com/pseudocodes/heap-sort/

        int count = balls.Count;

        for (int i =  (count/2) - 1; i >=0; i--)
            Heapify(balls,count,i);

        for (int i = count - 1; i > 0; i--)
        {
            (balls[0], balls[i]) = (balls[i], balls[0]);
            Heapify(balls,i,0);
        }
    }

    private static void Heapify(List<BallBehavior> balls, int count, int root)
    {
        int largestValue = root;
        int leftNode = 2 * largestValue + 1;
        int rightNode = 2 * largestValue + 2;

        if (rightNode < count && balls[rightNode].angle > balls[largestValue].angle)
            largestValue = rightNode;
        else
            largestValue = root;

        if (leftNode < count && balls[leftNode].angle > balls[largestValue].angle)
            largestValue = leftNode;

        if (largestValue != root)
        {
            (balls[root], balls[largestValue]) = (balls[largestValue], balls[root]);
            
            Heapify(balls,count,largestValue);
        }
        

    }
}
