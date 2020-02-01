using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public struct PatrolData
{
    public bool smoothArrival;
    public float patrolSpeed, waitTime, slowRadius, maxAccel;

    List<Transform> patrolPoints;
    int currentPatrolPoint, nextControlPoint;
    
    public void SetupPatrolPoints(Transform t)
    {
        currentPatrolPoint = -1;
        nextControlPoint = 0;

        patrolPoints = new List<Transform>();
        foreach (Transform child in t)
        {
            if (child.tag == "PatrolPoint")
                patrolPoints.Add(child);
        }
    }

    public void IncrementPatrol()
    {
        currentPatrolPoint++;
        nextControlPoint++;
    }

    internal Transform GetPatrolPoint(int index)
    {
        return patrolPoints[index];
    }

    internal int GetNumPatrolPoints()
    {
        return patrolPoints.Count;
    }

    internal int GetNextPatrolPointIndex()
    {
        return nextControlPoint;
    }

    internal void SetCurrentPatrolPoint(int v)
    {
        currentPatrolPoint = v;
    }

    internal void SetNextPatrolPoint(int v)
    {
        nextControlPoint = v;
    }

    internal int GetCurrentPatrolPointIndex()
    {
        return currentPatrolPoint;
    }
}