using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySplineController : MonoBehaviour
{
    private SplineFollower splineFollower;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    Vector3 objectPosition;

    // Start is called before the first frame update
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>(); 
    }

    private void Update()
    {
        objectPosition = transform.position;
    }
    public void ChekingBunnyPosition()
    {
        if (objectPosition.x >= minX && objectPosition.x <= maxX &&
            objectPosition.y >= minY && objectPosition.y <= maxY &&
            objectPosition.z >= minZ && objectPosition.z <= maxZ)
        {
            ToughtButtonKachely();
        }
    }

    public void ToughtButtonKachely()
    {
        if (splineFollower != null)
        {
            splineFollower.enabled = true;
        }
    }
    public void SplineControllerOff()
    {
        if (splineFollower != null)
        {
            splineFollower.enabled = false;
        }
    }
}