using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdAnimalAI : AnimalAI
{
    [HideInInspector]public AnimalColony colony;
    [SerializeField, Range(0f, 100f)]
    private float MoveProbability = 50f;
    [HideInInspector] public Vector3 Destination;
    public GameObject Player; 

    // Start is called before the first frame update
    public override void HandleWalk()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
                StartCoroutine(IdleThenMaybeMove());
        }
    }

    public override void HandleFlee()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetState(AnimalState.Idle);
        }
    }

    public void SeeDanger()
    {
        foreach(GameObject animal in colony.Individuals)
        {
            animal.GetComponent<HerdAnimalAI>().SetState(AnimalState.Flee); 
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
            Debug.Log(randomInt);
        }
        while (randomInt <= MoveProbability);

        
        Destination = colony.GenerateNewDestination();
        agent.SetDestination(Destination);
        SetState(AnimalState.Walk);
    }

    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Destination != Vector3.zero)
        {
            Debug.DrawLine(transform.position, agent.destination, Color.yellow);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SeeDanger(); 
        }
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
                agent.speed = speed * 1.5f;
                break;

            case AnimalState.Flee:
                agent.speed = 2;
                agent.SetDestination(colony.GenerateFleeDestination(Player.transform));  
                break;

            case AnimalState.Dead:
                agent.isStopped = true;
                break;
        }
    }

    
}
