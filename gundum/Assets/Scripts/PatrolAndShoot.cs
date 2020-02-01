using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAndShoot : MonoBehaviour
{
    [SerializeField] EnemyState state;
    [SerializeField] PatrolData patrolData;
    [SerializeField] ShootingData shootingData;

    Rigidbody2D rb;
    Vector3 direction;
    private bool shouldWait = true;
    bool arrived;
    private float targetSpeed;
    private bool shouldContinueChase;
    private float time;
    const float TIME_TO_TARGET = 0.1f;
    const float TARGET_RADIUS = 0.5f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InitEnemyPatrol();
        time = shootingData.fireRate;
    }

    void InitEnemyPatrol()
    {
        patrolData.SetupPatrolPoints(transform.parent);
        transform.position = patrolData.GetPatrolPoint(0).position;

        SetPatrolPath();
    }

    void SetPatrolPath()
    {
        patrolData.IncrementPatrol();

        if (patrolData.GetCurrentPatrolPointIndex() > patrolData.GetNumPatrolPoints() - 1)
            patrolData.SetCurrentPatrolPoint(0);

        if (patrolData.GetNextPatrolPointIndex() > patrolData.GetNumPatrolPoints() - 1)
            patrolData.SetNextPatrolPoint(0);

        arrived = false;
        shouldWait = true;
    }

    void Update()
    {
        Vector3 diff = shootingData.target.position - transform.position;
        Debug.Log(diff.magnitude);

        if (diff.magnitude < shootingData.shotRadius)
        {
            shouldContinueChase = true;
            state = EnemyState.CHASE;
        }
        else
        {
            if (shouldContinueChase)
                StartCoroutine(WaitForPatrol(shootingData.waitTime));

            shouldContinueChase = false;
        }
    }

    private void FixedUpdate()
    {
        if (state == EnemyState.PATROL)
        {
            if (arrived)
            {
                SetPatrolPath();
            }

            Patrol();
        }
        else if (state == EnemyState.CHASE)
            Chase();
    }

    void Chase()
    {
        direction = shootingData.target.position - transform.position;

        if (time <= 0)
        {
            GameObject bullet = Instantiate(shootingData.bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Bullet>().Fire(direction, shootingData.bulletSpeed);

            time = shootingData.fireRate;
        }
        else
        {
            time -= Time.fixedDeltaTime;
        }
    }

    void Patrol()
    {
        Vector3 targetVec = Vector3.zero;

        int nextPatrolPoint = patrolData.GetNextPatrolPointIndex();
        targetVec = patrolData.GetPatrolPoint(nextPatrolPoint).position;

        Vector3 direction = targetVec - transform.position;
        float distance = direction.magnitude;

        if (distance <= TARGET_RADIUS)
        {
            rb.velocity = Vector2.zero; //stop velocity and set next point

            if (shouldWait)
                StartCoroutine(WaitForPatrol(patrolData.waitTime));

            shouldWait = false;
            return;
        }

        if (patrolData.smoothArrival)
            targetSpeed = patrolData.patrolSpeed * distance / patrolData.slowRadius;
        else
            targetSpeed = patrolData.patrolSpeed / patrolData.slowRadius;

        Vector2 targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        rb.velocity = targetVelocity;
        rb.velocity /= TIME_TO_TARGET;

        ClampAccel();
    }

    void ClampAccel()
    {
        //clamp accel if necessary
        if (rb.velocity.sqrMagnitude > patrolData.maxAccel * patrolData.maxAccel)
        {
            rb.velocity.Normalize();
            rb.velocity *= patrolData.maxAccel;
        }
    }

    IEnumerator WaitForPatrol(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        arrived = true;
        state = EnemyState.PATROL;
    }
}
