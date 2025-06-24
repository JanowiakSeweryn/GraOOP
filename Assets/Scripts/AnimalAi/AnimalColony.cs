using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalColony : MonoBehaviour
{
    public List<GameObject> Individuals = new List<GameObject>();
    static Vector3 center;
    float GenerationRadius = 10f;

 

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
        return center;
    }

    public void AlertColony()
    {
        foreach (GameObject animal in Individuals)
        {
            HerdAnimalAI ai = animal.GetComponent<HerdAnimalAI>();
            if (ai != null)
            {
                Vector3 fleeTarget = GenerateFleeDestination(ai.transform, animal.transform.position);
                ai.SetState(AnimalAI.AnimalState.Flee); // zak³adam, ¿e masz tak¹ metodê — jeœli nie, podmienimy
            }
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


    public Vector3 GenerateFleeDestination(Transform playerTransform, Vector3 animalPosition)
    {
        float fleeDistance = 2 * GenerationRadius;

        Vector3 toAnimal = animalPosition - playerTransform.position;
        Vector3 playerForward = playerTransform.forward;
        Vector3 playerRight = Vector3.Cross(Vector3.up, playerForward).normalized;

        float side = Mathf.Sign(Vector3.Dot(toAnimal, playerRight));

        float baseAngle = side > 0 ? 90f : -90f;


        int seed = Mathf.FloorToInt(animalPosition.x * 1000 + animalPosition.z * 1000);
        Random.InitState(seed);
        float angleOffset = Random.Range(-30f, 30f);
        float finalAngle = baseAngle + angleOffset;

        Quaternion rotation = Quaternion.AngleAxis(finalAngle, Vector3.up);
        Vector3 fleeDirection = rotation * playerForward;

        Vector3 randomOffset = new Vector3(
            Random.Range(-0.5f, 0.5f),
            0f,
            Random.Range(-0.5f, 0.5f)
        );

        Vector3 targetPosition = animalPosition + fleeDirection.normalized * fleeDistance + randomOffset;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 3f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return animalPosition; // fallback
        }
    }

    public void RemoveFromColony(GameObject gameObject)
    {
        if (Individuals.Contains(gameObject))
        {
            Individuals.Remove(gameObject);
        }
        else
        {
            Debug.LogWarning("Próba usuniêcia obiektu, którego nie ma na liœcie.");
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
