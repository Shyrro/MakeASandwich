using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    private const float MAX_LEFT_POS = -7f;
    private const float MAX_RIGHT_POS = 7f;
    private bool _isSpawning = true;
    [HideInInspector]
    public float timeBtwSpawn = 2f;
    private int _diffNbSpawn;
    // Number to substract depending on the level
    // The higher the number is, the lesser we spawn
    [HideInInspector]
    public int DiffNbSpawn
    {
        get
        {
            return _diffNbSpawn;
        }
        set
        {
            if (value > objectsToSpawn.Count)
            {
                _diffNbSpawn = objectsToSpawn.Count;
            }
            else if (value < 0)
            {
                _diffNbSpawn = 0;
            }
            else
            {
                _diffNbSpawn = value;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(timeBtwSpawn);
            //add logic for penalties and obstacles to avoid
            SpawnIngredient();
        }
    }

    private void SpawnIngredient()
    {
        float randomX = Random.Range(MAX_LEFT_POS, MAX_RIGHT_POS);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y - 1, transform.position.z);
        int randomNumber = Random.Range(0, objectsToSpawn.Count - DiffNbSpawn);
        Instantiate(objectsToSpawn[randomNumber], spawnPosition, Quaternion.identity);
    }

}