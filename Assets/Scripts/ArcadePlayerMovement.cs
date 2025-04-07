using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class ArcadePlayerMovement : MonoBehaviour
{
    [SerializeField] private float movespeed;
    [SerializeField] private float moveRange;
    [SerializeField] private int maxSteerRotation;
    [SerializeField] private float steerRotationSpeed;
    [SerializeField] private RoadGenerator roadGenerator;
    [SerializeField] smoothScore smooth_Score;
    [SerializeField] private float sensetivity;

    public bool isGameIntro = false;

    private Vector3 currentPos;
    private Vector3 lastPos;
    private float minMoveDistanceToRotate = 0.02f;
    private float rotation;
    private Renderer carMesh;
    private Camera cachedCamera;

    private void Start()
    {
        carMesh =FindObjectOfType<mainManager>().cars[Storage.Instance.SelectedCar].GetComponent<Renderer>(); 
        cachedCamera = Camera.main;
        cachedCamera.gameObject.transform.SetParent(transform);     
    }

    void LateUpdate()
    {      
        if (isGameIntro)
        {
            Vector3 camPos = cachedCamera.transform.position;
            Vector3 camRotation = cachedCamera.transform.eulerAngles;

            float tmpX = Mathf.Lerp(cachedCamera.transform.position.x, 0, 0.15f);
            float tmpY = Mathf.Lerp(cachedCamera.transform.position.y, 24, 0.15f);
            float tmpZ = Mathf.Lerp(cachedCamera.transform.position.z, -25, 0.3f);

            Vector3 tmpPos = new Vector3(tmpX, tmpY, tmpZ);

            tmpX = Mathf.Lerp(cachedCamera.transform.eulerAngles.x, 26, 0.12f);
            tmpY = Mathf.Lerp(cachedCamera.transform.eulerAngles.y, 0, 0.1f);

            Vector3 tmpEuler = new Vector3(tmpX, tmpY, cachedCamera.transform.eulerAngles.z);

            if (cachedCamera.transform.position.x >= -0.3 && cachedCamera.transform.position.x < 0.3 &&
                cachedCamera.transform.position.y >= 23.7 && cachedCamera.transform.position.y < 24.3 &&
                cachedCamera.transform.position.z >= -25.3 && cachedCamera.transform.position.z < -24.7 &&
                cachedCamera.transform.eulerAngles.x >= 25.7 && cachedCamera.transform.eulerAngles.x < 26.3 &&
                cachedCamera.transform.eulerAngles.y >= -0.3 && cachedCamera.transform.eulerAngles.y < 0.3)
            {
                isGameIntro = false;
            }
               
            else
            {
                cachedCamera.transform.position = tmpPos;
                cachedCamera.transform.eulerAngles = tmpEuler;
            }
        }
        else
        {
            if (cachedCamera.fieldOfView < 60)
            {
                cachedCamera.fieldOfView += 0.1f;
            }
            else if (cachedCamera.fieldOfView > 60)
            {
                cachedCamera.fieldOfView -= 0.1f;

            }

            UpdateRotation();
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2 && transform.position.x > -moveRange)
                {
                    transform.Translate(movespeed * Time.deltaTime * -5, 0, 0);
                }
                else if (Input.mousePosition.x > Screen.width / 2 && transform.position.x < moveRange)
                {
                    transform.Translate(movespeed * Time.deltaTime * 5, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.A) && transform.position.x > -moveRange)
            {
                transform.Translate(movespeed * Time.deltaTime * -5, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D) && transform.position.x < moveRange)
            {
                transform.Translate(movespeed * Time.deltaTime * 5, 0, 0);
            }
        }
    }

    void UpdateRotation()
    {
        currentPos = transform.position;

        if (Vector3.Distance(currentPos, lastPos) > minMoveDistanceToRotate)
        {
            if (currentPos.x > lastPos.x)
            {
                rotation += Time.deltaTime * steerRotationSpeed;
            }
            else if (currentPos.x < lastPos.x)
            {
                rotation += Time.deltaTime * -steerRotationSpeed;
            }
        }
        else
        {
            if (rotation > 0)
            {
                rotation += Time.deltaTime * -steerRotationSpeed * 2;
                if (rotation < 0)
                    rotation = 0;
            }
            else if (rotation < 0)
            {
                rotation += Time.deltaTime * steerRotationSpeed * 2;
                if (rotation > 0)
                    rotation = 0;
            }
        }

        lastPos = currentPos;
        rotation = Mathf.Clamp(rotation, -maxSteerRotation , maxSteerRotation );
        carMesh.gameObject.transform.localEulerAngles = new Vector3(carMesh.transform.localEulerAngles.x, rotation*1.1f, rotation/1.8f);
    }

    public void AddSpeed(float buff)
    {
        roadGenerator.AddSpeed(buff);
    }

    public void SetCameraView(float value)
    {
        cachedCamera.fieldOfView = value;
    }

    public void DestroyCar()
    {
        enabled = false;
        carMesh.gameObject.SetActive(false);
        roadGenerator.speed = 0;
    }
    public void RestartCar()
    {
        enabled = true;
        carMesh.gameObject.SetActive(true);
    }
    public void CountScore()
    {
        smooth_Score.CountScore();
    }
}
