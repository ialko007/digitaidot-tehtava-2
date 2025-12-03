using System.Collections.Generic;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEditor.Rendering.Analytics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float startingSpeed = 5.0f;
    [SerializeField] private float doublingTime = 10.0f;
    private float speed = 5.0f;
    private float lifetime = 0.0f;
    [HideInInspector] public bool active = false;

    [SerializeField] private float timeToSpawnMin = 1.5f;
    [SerializeField] private float timeToSpawnMax = 4.0f;
    private float timeToSpawnLeft = 0.0f;

    [SerializeField] private GameObject spikesPrefab;

    private List<GameObject> obstacles = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            lifetime += Time.deltaTime;
            speed = startingSpeed * Mathf.Pow(2.0f, lifetime / doublingTime);
            timeToSpawnLeft -= Time.deltaTime * Mathf.Pow(2.0f, lifetime / doublingTime);
            if (timeToSpawnLeft <= 0.0f)
            {
                obstacles.Add(Instantiate(spikesPrefab, new Vector3(13.0f + Random.Range(0.0f, 10.0f), -2.6f, 0.0f), Quaternion.identity));
                timeToSpawnLeft = Random.Range(timeToSpawnMin, timeToSpawnMax);
            }
        }
    }

    public float getSpeed()
    {
        if (active) return speed;
        else return 0.0f;
    }

    public void Stop()
    {
        active = false;
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        speed = startingSpeed;
        lifetime = 0.0f;
    }
}
