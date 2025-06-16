using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalColony : MonoBehaviour // skrypt dla stada osobników 
{
    public List<GameObject> Individuals = new List<GameObject>();
    static Vector3 center;
    float GenerationRadius = 10f; 

    public virtual void Alert() {
        Debug.Log("KURWA SPIERDALAMY"); 
    }

    public Vector3 GenerateNewDestination()
    {
        int maxTries = 100;
        int tries = 0;

        while (tries < maxTries)
        {
            Vector3 randomDirection = Random.insideUnitSphere * GenerationRadius;
            randomDirection += center;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, GenerationRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            tries++;
        }

        Debug.LogWarning("Nie znaleziono punktu na NavMesh po " + maxTries + " próbach. Zwracam center.");
        return center; // fallback w razie niepowodzenia
    }

    public void AlertColony()
    {
        foreach(GameObject animal in Individuals)
        {
            Alert(); 
        }
    }
    public Vector3 GetAveragePosition()
    {
        if (Individuals.Count == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;
        foreach (GameObject animal in Individuals)
        {
            sum += animal.transform.position;
        }

        return sum / Individuals.Count;
    }

    void DrawDiestance()
    {
        foreach (GameObject animal in Individuals)
        {
            Debug.DrawLine(animal.transform.position, center + Vector3.up * 2, Color.cyan);
        }
    }

    public void OnCreateColony()
    {
        center = GetAveragePosition();
        foreach (GameObject animal in Individuals)
        {
            animal.GetComponent<HerdAnimalAI>().SetState(AnimalAI.AnimalState.Walk); 
        }
    }

    public Vector3 GenerateFleeDestination(Transform playerTransform)
    {
        float fleeDistance = 2 * GenerationRadius;
        Vector3 directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;

        // Mo¿emy dodaæ lekki chaos, ¿eby nie wszyscy uciekali idealnie liniowo
        Vector3 randomOffset = new Vector3(
            Random.Range(-10f, 10f),
            0f,
            Random.Range(-10f, 10f)
        ).normalized * Random.Range(0f, 2f); // losowy chaos

        Vector3 fleeDirection = (directionAwayFromPlayer + randomOffset).normalized;

        Vector3 targetPosition = transform.position + fleeDirection * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 3f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return transform.position; // fallback — zostaje w miejscu jeœli brak dobrego punktu
        }
    }



    public void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawLine(center, center + Vector3.up * 2, Color.green); 
        DrawDiestance(); 

        
    }

    
}


