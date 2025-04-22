using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roadPrefabs;
    [SerializeField] private float startSpeed = 150;
    [SerializeField] private int maxRoadCount = 4;

    public float speed = 0;

    private int prevRandValue;
    private List<GameObject> roads = new List<GameObject>();
    private float buffSpeed;
   
    void Start()
    {
        ResetLevel();
        Pause();
        StartLevel();
    }

    void Update()
    {
        if (speed == 0)
        {
            return;
        }

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (speed > startSpeed)
        {
            speed--;

        }
        else if (speed < startSpeed)
        {
            speed++;

        }

        if (roads[0].transform.position.z < -200)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

        startSpeed += 0.005f; //постепенное ускорение
        speed += 0.005f;
    }

    private void CreateNextRoad()
    {
        bool isCorrectGeneration = false;

        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 200);
        }

        int randInt = 0;
        if (roads.Count > 1)
        {
            while (!isCorrectGeneration)
            {
                System.Random rand = new System.Random();
                randInt = rand.Next(0, roadPrefabs.Count);

                if (randInt != prevRandValue)
                    isCorrectGeneration = true;
            }
        }

        GameObject go = Instantiate(roadPrefabs[randInt], pos, Quaternion.identity);

        go.transform.SetParent(transform);
        roads.Add(go);

        prevRandValue = randInt;
    }

    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }

    public void StartLevel()
    {
        startSpeed = 150;
        speed = startSpeed;
        //YandexGame.FullscreenShow();
        Storage.Instance.Save();
    }

    public void Restart()
    {
        ResetLevel();
        StartLevel();
    }
    public void PauseButton()
    {
        enabled = false;
    }
    public void ContinueButton()
    {
        enabled = true;

    }

    public void Pause()
    {
        startSpeed = speed;
        speed = 0;
    }

    public void Continue()
    {
        speed = startSpeed;
    }

    public void AddSpeed(float buff)
    {
        speed += buff;
    }
}
