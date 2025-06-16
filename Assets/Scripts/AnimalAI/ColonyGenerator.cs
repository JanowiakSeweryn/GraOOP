using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyGenerator : MonoBehaviour
{
    public enum SpawnShape { Circle, Rectangle };
    Vector3 center;

    [Header("SETTINGS")]
    public string ColonyName; 
    public SpawnShape spawnShape = SpawnShape.Circle;

    public GameObject prefab;
    public int numberOfIndividuals = 10;

    public Terrain terrain;

    // Parametry okr�gu
    [Header("Circle parameters")]
    public float radius = 10f;

    // Parametry prostok�ta
    [Header("Ractangle parameters")]
    public float width = 10f;
    public float height = 10f;
    [SerializeField]
    private List<GameObject> spawnedIndividuals = new List<GameObject>();
    [SerializeField]
    GameObject ColonyPrefab; 

    private List<Vector3> spawnedPositions = new List<Vector3>();
    GameObject CreatedColony;



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
         
        if (spawnShape == SpawnShape.Circle)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        else if (spawnShape == SpawnShape.Rectangle)
        {
            Vector3 size = new Vector3(width, 0.1f, height);
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
    float minDistanceBetweenIndividuals = 1;
  
  

    private void Start()
    {
        int attempts = 0;
        int maxAttempts = numberOfIndividuals * 10;

        center = transform.position;
        CreatedColony = Instantiate(ColonyPrefab, center, Quaternion.identity); // tworzymy obiekt Colony -> stado pod kt�re podpiszemy wszytkie stowrzone osobniki
        //CreatedColony.Start(); 
        CreatedColony.name = ColonyName;


        while (spawnedPositions.Count < numberOfIndividuals && attempts < maxAttempts)
        {
            attempts++;
            

            Vector2 randomPoint2D = Random.insideUnitCircle * radius;
            Vector3 candidatePos = center + new Vector3(randomPoint2D.x, 0, randomPoint2D.y); // generujemy pozycje dla osobnika w wybranym promienmiu 

            float terrainHeight = terrain.SampleHeight(candidatePos);
            candidatePos.y = terrainHeight; // wysoko�� nowego osobnika

            bool tooClose = false;
            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(pos, candidatePos) < minDistanceBetweenIndividuals) // p�tla sprawdzaj�ca odleg�o�� pomi�dzy osobnikami
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                // Mo�na spawnowa� pingwina
                spawnedPositions.Add(candidatePos); // dodajemy wylosowan� pozycje do listy wygenerowanych pozycji 
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0); // losowa rotacja 
                GameObject SpawnedIndividual = Instantiate(prefab, candidatePos, randomRotation); // wyspawnowanie osobnika
                //SpawnedIndividual.Start(); 
                SpawnedIndividual.transform.SetParent(CreatedColony.transform); // podpisanie osobnika jako child object do stada 
                spawnedIndividuals.Add(SpawnedIndividual); // dodanie osobnika do listy wyspawnowanych 

            }
        }
        // PO WYGENEROWANIU WSZYSTKICH OSOBNIK�W 
        CreatedColony.GetComponent<AnimalColony>().Individuals = spawnedIndividuals; // przepisujemy liste utworzonych obnik�w z lokalnej listy do na liste osobnik�w nowego stada 
        foreach(GameObject animal in CreatedColony.GetComponent<AnimalColony>().Individuals)
        {
            animal.GetComponent<HerdAnimalAI>().colony = CreatedColony.GetComponent<AnimalColony>(); // dla ka�dego osobnika ze stada przypisujemy mu skrypt stada
            animal.GetComponent<HerdAnimalAI>().Start(); 
        }
        CreatedColony.GetComponent<AnimalColony>().OnCreateColony(); 
        Destroy(this.gameObject); 
    }

}

