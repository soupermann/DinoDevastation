using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LastThrow : MonoBehaviour
{
    public Transform throwableObject;
    public Camera mainCamera;
    public Camera zoomCamera;
    public float delay = 2f;

    private bool switched = false;
    private float timeSinceSpawned = 0f;
    private bool waitingToSwitchBack = false;
    private float timeSinceDestroyed = 0f;

    public float zoomSpeed = 2f;
    public float maxZoom = 5f;
    public float minZoom = 1f;
    public float zoomAmount = 10.0f;
    public float zoomTime = 2.0f;

    private float currentZoom = 1f;
    private bool isZooming = false;
    private float zoomStartTime;
    private Vector3 originalPosition;

    void Update()
    {
        if (throwableObject == null)
        {
            timeSinceDestroyed += Time.deltaTime;

            if (!waitingToSwitchBack && timeSinceDestroyed >= delay)
            {
                mainCamera.enabled = true;
                zoomCamera.enabled = false;
                waitingToSwitchBack = true;
                switched = false;
            }
        }
        else
        {
            timeSinceSpawned += Time.deltaTime;

            if (!switched && timeSinceSpawned >= delay)
            {
                mainCamera.enabled = false;
                zoomCamera.enabled = true;
                switched = true;
                waitingToSwitchBack = false;
                timeSinceDestroyed = 0f;
            }
        }// Check if the throwable object exists
        if (throwableObject == null){
            mainCamera.enabled = true;
            zoomCamera.enabled = false;
            return;
        }
        
            

        // Calculate the distance between the second camera and the throwable object
        float distance = Vector3.Distance(zoomCamera.transform.position, throwableObject.position);

        // Zoom in or out based on the distance
        if (distance > maxZoom)
        {
            currentZoom -= zoomSpeed * Time.deltaTime;
        }
        else if (distance < minZoom)
        {
            currentZoom += zoomSpeed * Time.deltaTime;
        }

        // Clamp the zoom to the min and max values
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Set the second camera's position and field of view based on the zoom
        zoomCamera.transform.position = throwableObject.position - zoomCamera.transform.forward * currentZoom;
        zoomCamera.fieldOfView = currentZoom * 10f;
        if (isZooming)
        {
            float zoomProgress = (Time.time - zoomStartTime) / zoomTime;
            if (zoomProgress >= 1.0f)
            {
                zoomCamera.transform.localPosition = originalPosition;
                isZooming = false;
            }
            else
            {
                float currentZoom = Mathf.Lerp(0.0f, zoomAmount, zoomProgress);
                zoomCamera.transform.localPosition = originalPosition + Vector3.back * currentZoom;
            }
        }
    }

    
        public void StartZoom()
    {
        isZooming = true;
        zoomStartTime = Time.time + 2.0f;
        originalPosition = zoomCamera.transform.localPosition;
    }
}