using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField]
    GameObject platformPrefab;
    [SerializeField]
    GameObject bouncyPlatformPrefab;
    [SerializeField]
    GameObject fallOffPlatformPrefab;
    [SerializeField]
    GameObject deathPlatformPrefab;
    [SerializeField]
    Camera cam;
    GameObject[] platforms;

    public int numberOfPlatforms = 10;
    public float levelWidth = 2.5f;
    public float minY = 0.2f;
    public float maxY = 1.5f;
    float highestPlatformHeight;

    [SerializeField]
    GameObject warningSign;
    [SerializeField]
    GameObject shuriken;


    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        platforms = new GameObject[numberOfPlatforms];
        Vector3 spawnPosition = new Vector3(0f, 21f, 0f);
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            platforms[i] = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
        highestPlatformHeight = platforms[numberOfPlatforms - 1].transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            if (platforms[i].transform.position.y < (cam.transform.position.y - 5.5))
            {
                Vector3 newPos = new Vector3(Random.Range(-levelWidth, levelWidth), highestPlatformHeight + Random.Range(minY, maxY), platforms[i].transform.position.z);
                platforms[i].transform.position = newPos;
                if (Random.Range(1, 20) == 5)
                {
                    Vector3 bouncyPlatformPos = new Vector3(Random.Range(-levelWidth, levelWidth), newPos.y - Random.Range(0, maxY), newPos.z);
                    Instantiate(bouncyPlatformPrefab, bouncyPlatformPos, Quaternion.identity);
                }
                if (newPos.y > 200)
                {
                    if (Random.Range(1, 30) == 5)
                    {
                        Vector3 fallOffPlatformPos = new Vector3(Random.Range(-levelWidth, levelWidth), newPos.y - Random.Range(0, maxY), newPos.z);
                        Instantiate(fallOffPlatformPrefab, fallOffPlatformPos, Quaternion.identity);
                    }
                }
                if (newPos.y > 300)
                {
                    if (Random.Range(1, 30) == 5)
                    {
                        Vector3 deathPlatformPos = new Vector3(Random.Range(-levelWidth, levelWidth), newPos.y - Random.Range(0, maxY), newPos.z);
                        Instantiate(deathPlatformPrefab, deathPlatformPos, Quaternion.identity);
                    }
                }
                highestPlatformHeight = newPos.y;
            }
        }
        SpawnShuriken();
    }

    void SpawnShuriken()
    {
        if (highestPlatformHeight > 100)
        {
            if (Random.Range(1, 2500) == 5)
            {
                StartCoroutine(ShurikenRoutine());
            }
        }
    }

    IEnumerator ShurikenRoutine()
    {
        float randomX = Random.Range(-2.2f, 2.2f);
        Vector3 position = new Vector3(randomX, cam.transform.position.y + 4.4f, 0f);
        GameObject warning = Instantiate(warningSign, position, Quaternion.identity);
        warning.transform.parent = cam.transform;
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(warning);
        Instantiate(shuriken, new Vector3(randomX, cam.transform.position.y + 6f, 0f), Quaternion.identity);
    }

}
