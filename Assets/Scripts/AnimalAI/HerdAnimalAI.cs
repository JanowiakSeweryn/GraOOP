using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerdAnimalAI : AnimalAI
{
    [HideInInspector] public AnimalColony colony;
    [SerializeField, Range(0f, 100f)]
    private float MoveProbability = 50f;
    [HideInInspector] public Vector3 Destination;
    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        base.Update();

        Debug.DrawLine(transform.position, agent.destination, Color.yellow);


        if (Input.GetKeyDown(KeyCode.E))
        {
            SeeDanger();
        }
        
        

    }

    //-----------------------------------------------------------------------------------------------------------------
    public override void HandleWalk()
    {
        if (IsPlayerClose())
        {
            SetState(AnimalState.Run);
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(IdleThenMaybeMove());
        }

        if (IsStuck())
        {
            Destination = colony.GenerateNewDestination();
            agent.SetDestination(Destination);
        }
    }

    public override void HandleFlee()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!IsPlayerClose())
            {
                SetState(AnimalState.Idle);
                StartCoroutine(IdleThenMaybeMove());
            }
            else
            {
                agent.SetDestination(colony.GenerateFleeDestination(player.transform, transform.position));
                SetState(AnimalState.Flee);
            }
        }
        if (IsStuck())
        {
            agent.SetDestination(colony.GenerateFleeDestination(player.transform, transform.position));
        }
    }

    public override void HandleRun()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(IdleThenMaybeMove());
        }
        if (IsStuck())
        {
            Destination = colony.GenerateNewDestination();
            agent.SetDestination(Destination);
        }
    }


    public void SeeDanger()
    {
        foreach (GameObject animal in colony.Individuals)
        {
            SetState(AnimalState.Flee);
        }
    }

    private IEnumerator IdleThenMaybeMove()
    {
        int randomInt;

        do
        {
            SetState(AnimalState.Idle);
            yield return new WaitForSeconds(2f);
            randomInt = UnityEngine.Random.Range(0, 101);
        }
        while (randomInt <= MoveProbability);


        Destination = colony.GenerateNewDestination();
        agent.SetDestination(Destination);
        SetState(AnimalState.Walk);
    }

    public override void ApplyStateSettings()
    {
        switch (currentState)
        {
            case AnimalState.Idle:
                agent.SetDestination(transform.position); // stoi w miejscu
                agent.speed = 0f;
                break;

            case AnimalState.Walk:
                agent.speed = speed; // np. 2
                agent.SetDestination(colony.GenerateNewDestination());
                break;

            case AnimalState.Run:
                agent.speed = 2;
                agent.SetDestination(GenerateStepAsideDestination());
                break;

            case AnimalState.Flee:
                agent.speed = 2;
                agent.SetDestination(colony.GenerateFleeDestination(player.transform, transform.position));
                break;

            case AnimalState.Dead:
                agent.isStopped = true;
                break;
        }
    }

    public bool IsPlayerClose()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= 1f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Vector3 lastPosition;
    private float stuckCheckTimer = 0f;
    public float minMoveDistance = 0.2f;

    public bool IsStuck()
    {
        stuckCheckTimer += Time.deltaTime;

        if (stuckCheckTimer >= 1f)
        {
            float distanceMoved = Vector3.Distance(transform.position, lastPosition);

            lastPosition = transform.position;
            stuckCheckTimer = 0f;

            if (distanceMoved < minMoveDistance)
            {
                return true;
            }
        }

        return false;
    }

    public Vector3 GenerateStepAsideDestination()
    {
        Transform playerTransform = player.transform;
        float sideStepDistance = 2f;

        Vector3 toPlayer = (playerTransform.position - transform.position).normalized;


        Vector3 sideDirection = Vector3.Cross(toPlayer, Vector3.up); // w lewo


        if (Random.value > 0.5f)
            sideDirection = -sideDirection;

        Vector3 targetPosition = transform.position + sideDirection * sideStepDistance;


        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return transform.position;
        }
    }
}