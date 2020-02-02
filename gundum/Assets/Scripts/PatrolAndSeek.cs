using UnityEngine;
using System.Collections;

public enum EnemyState { NULL = -1, PATROL, CHASE, SHOOT }

public class PatrolAndSeek : MonoBehaviour
{
    [SerializeField] EnemyState state;
    [SerializeField] PatrolData patrolData;
    [SerializeField] ChaseData chaseData;

    Rigidbody2D rb;
    Vector3 direction;
    private bool shouldWait = true;
    bool arrived;
    private float targetSpeed;
    private bool shouldContinueChase;
    const float TIME_TO_TARGET = 0.1f;
    const float TARGET_RADIUS = 0.5f;

    void Start()
    {
        if (chaseData.target == null)
            chaseData.target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();

        InitEnemyPatrol();
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

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = chaseData.target.position - transform.position;
        Debug.Log(diff.magnitude);

        if (diff.magnitude < chaseData.chaseRadius)
        {
            shouldContinueChase = true;
            state = EnemyState.CHASE;
        }
        else
        {
            if(shouldContinueChase)
                StartCoroutine(WaitForPatrol(chaseData.waitTime));

            shouldContinueChase = false;
        }
    }

    private void FixedUpdate()
    {
        if (state == EnemyState.PATROL)
        {
            if(arrived)
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
        direction = chaseData.target.position - transform.position;
        direction.Normalize();
        rb.velocity = direction;
        rb.velocity *= chaseData.chaseSpeed;
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

            if(shouldWait)
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
