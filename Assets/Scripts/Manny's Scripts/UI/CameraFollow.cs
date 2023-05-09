using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings:")]
    public Transform Player;
    public Vector3 offset;
    public float smoothFactor = 2;
    private Vector3 minValues;
    private Vector3 maxValues;

    // Boundaries
    public Vector3 MinBounds1;
    public Vector3 MaxBounds1;
    public Vector3 MinBounds2;
    public Vector3 MaxBounds2;
    public Vector3 MinBounds3;
    public Vector3 MaxBounds3;

    public GameObject[] boundaries; 

    public Collectibles collectibles;

    private void Update()
    {
        StartCoroutine(Boundary());
        
    }
    private void FixedUpdate()
    {
        CamFollow();
    }

    void CamFollow()
    {
        // Sets the cameras boundaries depending on the mushroom count
        if (collectibles.mushroomCount == 0)
        {
            minValues = MinBounds1;
            maxValues = MaxBounds1;
            
        }
        else if (collectibles.mushroomCount == 1)
        {

            minValues = MinBounds2;
            maxValues = MaxBounds2;

        }
        else
        {
            minValues = MinBounds3;
            maxValues = MaxBounds3;

        }


        // set camera position with offset
        Vector3 cameraPosition = Player.position + offset;

        // Clamps the camera position to min and max values
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(cameraPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(cameraPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(cameraPosition.z, minValues.z, maxValues.z));

        // Smooth transition from cameras current position to desired bounded camera position by lerping
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);

        // Set the cameras position to equal desired camera position
        transform.position = smoothPosition; 
    }

    // Boundary() method activates certain gameobjects to stop the player from going backwards after collecting a mushroom
    private IEnumerator Boundary()
    {
        if (collectibles.mushroomCount == 0)
        {
            yield return new WaitForSeconds(0);
            boundaries[0].SetActive(true);

        }
        else if (collectibles.mushroomCount == 1)
        {
            yield return new WaitForSeconds(2);
            boundaries[1].SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(2);
            boundaries[2].SetActive(true);
        }

    }
}

