using UnityEngine;
using UnityEngine.AI;

public class AnimalStats : MonoBehaviour
{
    public int Health = 100;
    protected Animator animator;
    protected NavMeshAgent agent;
    [SerializeField] float speed;

    public virtual void HendleDeath()
    {
        //tutaj animacja umierania 
    }

    public virtual void Start()
    {
        animator = GetComponent<Animator>();  
        if(animator == null) { Debug.Log(this.name + "has no animator attached"); }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null) { Debug.Log(this.name + "has no NavMeshAgent attached"); }
        agent.speed = speed; 
        
    }

    public virtual void Update()
    {

    }
}
