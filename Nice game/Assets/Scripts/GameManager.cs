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
    [SerializeField] private GameObject sawPrefab;

    private List<GameObject> obstacles = new List<GameObject>();

    public List<GameObject> parallaxScreens = new List<GameObject>();

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
                int choice = Random.Range(0, 3);
                switch (choice)
                {
                    case 0: // spikes
                    case 1: // spikes
                        obstacles.Add(Instantiate(spikesPrefab, new Vector3(13.0f + Random.Range(0.0f, 20.0f), -2.4f, 0.0f),
                            Quaternion.identity));
                        break;
                    case 2: // saw
                        obstacles.Add(Instantiate(sawPrefab, new Vector3(13.0f + Random.Range(0.0f, 20.0f), 1.75f, 0.0f),
                            Quaternion.identity));
                        break;
                }
                timeToSpawnLeft = Random.Range(timeToSpawnMin, timeToSpawnMax);
            }
            for (int i = 0; i < parallaxScreens.Count; i++)
            {
                GameObject parallaxScreen = parallaxScreens[i];
                float xOffset = speed * Time.deltaTime * Mathf.Pow(0.8f, (float)i);
                parallaxScreen.transform.position = new Vector3(
                    (parallaxScreen.transform.position.x - xOffset) % 16.0f,
                    parallaxScreen.transform.position.y,
                    parallaxScreen.transform.position.z);
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
        obstacles.Clear();
        speed = startingSpeed;
        lifetime = 0.0f;
    }
}
