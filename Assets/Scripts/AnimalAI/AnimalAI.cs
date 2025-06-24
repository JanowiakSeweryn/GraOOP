using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class AnimalAI : MonoBehaviour
{
    
    public NavMeshAgent agent;
    [SerializeField] protected float speed;
    [SerializeField] protected Animator anim;
    public GameObject player; 

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) { Debug.Log(this.name + "has no animator attached"); }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null) { Debug.Log(this.name + "has no NavMeshAgent attached"); }
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player"); 


    }

    public virtual void Update()
    {
        HandleStates();
    }


    public enum AnimalState { Idle, Walk, Run, Flee, Dead}
    public AnimalState currentState; 

    public virtual void HandleStates()
    {
        switch (currentState)
        {
            case AnimalState.Idle:
                HandleIdle();
                break;
            case AnimalState.Walk:
                HandleWalk();
                break;
            case AnimalState.Run:
                HandleRun();
                break;
            case AnimalState.Flee:
                HandleFlee();
                break;
            case AnimalState.Dead:
                HandleDeath();
                break;
        }
    }

    public virtual void HandleIdle()
    {
        // funkcja odpowiedzialna za œledzenie stanu idle
    }

    public virtual void HandleWalk()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetState(AnimalState.Idle); // wróæ do idle
        }
    }

    public virtual void HandleRun()
    {
        // funkcja odpowiedzialna za œledzenie stanu run
    }

    public virtual void HandleFlee()
    {
        // funkcja odpowiedzialna za œledzenie stanu flee
    }


    public virtual void HandleDeath()
    {
        //tutaj animacja umierania 
    }


    public void ResetAnimatorBools()
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }
    }

    public virtual void SetState(AnimalState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        ResetAnimatorBools();
        switch (newState)
        {
            case AnimalState.Idle:
                
                break;

            case AnimalState.Walk:
                anim.SetBool("isWalking", true);
                break;

            case AnimalState.Run:
                anim.SetBool("isRunning", true);
                break;

            case AnimalState.Flee:
                anim.SetBool("isRunning", true);
                // agent.SetDestination(GenerateNewDestination()); // kierunek ucieczki — ewentualnie specjalna wersja
                break;

            case AnimalState.Dead:
                anim.SetBool("isDead", true);
                break;
        }
           

        ApplyStateSettings(); // <- centralna logika dla prêdkoœci i ruchu
    }

    public virtual void ApplyStateSettings()
    {
        switch (currentState)
        {
            case AnimalState.Idle:
                agent.SetDestination(transform.position); // stoi w miejscu
                agent.speed = 0f;
                break;

            case AnimalState.Walk:
                agent.speed = speed; // np. 2
               // agent.SetDestination(GenerateNewDestination());
                break;

            case AnimalState.Run:
                agent.speed = speed * 1.5f;
                break;

            case AnimalState.Flee:
                agent.speed = speed * 2f;
                // agent.SetDestination(GenerateNewDestination()); // kierunek ucieczki — ewentualnie specjalna wersja
                break;

            case AnimalState.Dead:
                agent.isStopped = true;
                break;
        }
    }

}
