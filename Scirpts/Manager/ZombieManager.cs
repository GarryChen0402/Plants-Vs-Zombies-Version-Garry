using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }

    public List<GameObject> zombieSpawnPoints;

    public List<GameObject> zombies;

    private const int MaxZombieEachRow = 100;

    [SerializeField] private int maxZombiesNumber;
    [SerializeField] private int curZombiesNumber;

    [SerializeField] private float spawnCD;
    [SerializeField] private float spawnTimer;

    [SerializeField] public List<List<GameObject>> livingZombies;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }

            spawnTimer = 0;
        livingZombies = new List<List<GameObject>>();
        for(int i = 0; i < zombieSpawnPoints.Count; i++)livingZombies.Add(new List<GameObject>());
        maxZombiesNumber = 10;
        curZombiesNumber = 0;
    }



    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnCD)
        {
            spawnTimer -= spawnCD;
            GenerateSingleZombie();
        }
    }


    private void GenerateSingleZombie()
    {
        if (curZombiesNumber >= maxZombiesNumber) return;
        curZombiesNumber++;
        //int size = zombieSpawnPoints.Count;
        int generateRow = Random.Range(0, zombieSpawnPoints.Count);
        GameObject randomZombie = GameObject.Instantiate(zombies[Random.Range(0, zombies.Count - 1)], zombieSpawnPoints[generateRow].transform.position, Quaternion.identity) ;
        randomZombie.GetComponent<SpriteRenderer>().sortingOrder = generateRow * MaxZombieEachRow + livingZombies[generateRow].Count;
        livingZombies[generateRow].Add(randomZombie);
        //CheckLivingZombies();
    }

    public void RemoveDeadZombie(GameObject zombie)
    {
        foreach(var zombies in livingZombies)
        {
            if(zombies.Contains(zombie))zombies.Remove(zombie);
        }
        //CheckLivingZombies();
    }

    private void CheckLivingZombies()
    {
        Debug.Log("Here is the living zombie information:");
        foreach(var zombie in livingZombies)
        {
            Debug.Log(zombie.Count);
        }
        Debug.Log("Check over");
    }

}
