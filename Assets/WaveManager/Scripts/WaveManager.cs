using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Animator animator;
    public Text waveName;

    private int currentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;
    private bool canAnimate = false;


    private bool generatingWave = false;
    private bool ended = false;
    public UnityEvent onEnd;

    void Start()
    {
        if (waves.Length == 0) Debug.Log("NO HAS DEFINIDO NINGUNA OLEADAS");

        //Empiezo el juego
        waveName.text = waves[currentWaveNumber].waveName;
        animator.SetTrigger("Init");
    }
    private void Update()
    {
        if (ended) return;
        if (generatingWave) return;
        if (waves.Length == 0) return;

        //Compobar si se ha terminado la oleada
        GameObject[] totalEnemies = FindChildrenWithTag("Enemy");//GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemies.Length == 0  )//Si ya has eliminado a los enemigos
        {
            //Muestro el aviso de oleada terminada

            if (currentWaveNumber  < waves.Length)//Si queda oleadas
            {
                if (currentWaveNumber != 0)
                {
                    if (canAnimate)
                    {
                        waveName.text = waves[currentWaveNumber].waveName;

                        animator.SetTrigger("WaveComplete");
                        canAnimate = false;
                    }
                }

                //SpawnNextWave(); Actualmente llamado desde el Animator
            }
            else {
                Debug.Log("GameFinish");
                ended = true;
                onEnd.Invoke();
            }
        }
        
    }

    public void SpawnNextWave()
    {

        if (currentWaveNumber < waves.Length)//Si queda oleadas
        {
            //Genero la siguiente oleada
            canSpawn = true;

            StartCoroutine(GenerarOleada(waves[currentWaveNumber]));

            currentWaveNumber++;
        }
    }


    IEnumerator GenerarOleada(Wave currentWave)
    {
        generatingWave = true;

        while (canSpawn)
        {
            SpawnWave(currentWave);
            yield return new WaitForFixedUpdate();
        }
        generatingWave = false;
    }
    void SpawnWave(Wave currentWave)
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            randomEnemy.tag = "Enemy";
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity, transform);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
        
    }

    GameObject[] FindChildrenWithTag( string tag)
    {
        // Find the parent object by name
        GameObject parentObject = gameObject;

        // If the parent object is found
        if (parentObject != null)
        {
            // Get all child objects of the parent
            Transform parentTransform = parentObject.transform;
            int childCount = parentTransform.childCount;

            // List to store found child objects
            GameObject[] children = new GameObject[childCount];
            int foundCount = 0;

            // Iterate through each child
            for (int i = 0; i < childCount; i++)
            {
                // Check if the child's tag matches the specified tag
                Transform childTransform = parentTransform.GetChild(i);
                if (childTransform.CompareTag(tag))
                {
                    // If the tag matches, add it to the list of found children
                    children[foundCount] = childTransform.gameObject;
                    foundCount++;
                }
            }

            // Resize the array to fit the number of found children
            System.Array.Resize(ref children, foundCount);
            return children;
        }
        else
        {
            Debug.LogError("Parent object not found: " + name);
            return null;
        }
    }

}
