using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RoadGenerator : MonoBehaviour, IArcadeStateListener
{
    [SerializeField] private List<GameObject> roadPrefabs;
    [SerializeField] private float startSpeed = 150;
    [SerializeField] private int maxRoadCount = 4;
    [SerializeField] private float segmentLength;
    [SerializeField] private float speedAdjustmentRate = 0.005f;
    public float speed = 0;

    private int prevRandValue;
    private readonly List<GameObject> roads = new();
    private float prevSpeed;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(roadPrefabs, nameof(roadPrefabs), this);

    }
    private void Start()
    {
        enabled = false;
    }
    void OnEnable()
    {
        ResetLevel();
        Pause();
        StartLevel();
    }

    public void OnArcadePaused() => Pause();
    public void OnArcadeContinued() => Continue();
    public void OnArcadeRestart() => Restart();

    public void Pause()
    {
        speed = 0;
    }

    public void Continue()
    {
        speed = prevSpeed;
    }

    public void Restart()
    {
        ResetLevel();

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

        if (roads[0].transform.position.z < -segmentLength)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

        speed += speedAdjustmentRate;
        startSpeed += speedAdjustmentRate;
        prevSpeed = speed;
    }

    private void CreateNextRoad()
    {
        bool isCorrectGeneration = false;

        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, segmentLength);
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
        prevSpeed = 0;
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
        enabled = true;
        speed = startSpeed;

    }


    public void ModifySpeed(float buff)
    {
        speed += buff;
    }
    public void StopMovement()
    {
        speed = 0;
    }
}