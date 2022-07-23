using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling objPooling;
    private GameObject ratPref;
    private GameObject ratInstantiate;
    [SerializeField] int amountOfRat;

    public Queue<GameObject> ratPool = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ratPref = GameManager.instance.ratPrefab;
        CreateRatFirstStart();   
    }
    private void Update()
    {
        int numberOfRat = GameObject.FindGameObjectsWithTag("Rat").Length;
        GameManager.instance.ratText.text = "Rats: " + numberOfRat;
        if (numberOfRat == 0 && GameManager.instance.isGameStarted)
        {
            GameManager.instance.NextLevel();
            SpawnRat();
        }
       
    }

    private void SpawnRat()
    {
        
        for (int i = 0; i < GameManager.instance.levelIndex; i++)
        {
            ratInstantiate = GetRatFromPool();
            if (ratInstantiate != null)
            {
                ratInstantiate.SetActive(true);
            }
        }

        
    }
    private GameObject GetRatFromPool()
    {
        foreach (GameObject rat in ratPool)
        {
            if (!rat.activeInHierarchy)
            {
                return rat;
            }
        }
        return null;
    }

    private void CreateRatFirstStart()
    {
        for (int i = 0; i < amountOfRat; i++)
        {
            ratInstantiate = Instantiate(ratPref,GameManager.instance.CreateSpawnPoint(0),Quaternion.identity);
            GameObject ratParent = GameObject.Find("Rat Parent");
            ratInstantiate.transform.parent = ratParent.transform;
            ratInstantiate.SetActive(false);
            ratPool.Enqueue(ratInstantiate);
        }
    }
    
}
