using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] public Transform point1;
    [SerializeField] public Transform point2;
    [SerializeField] public Transform point3;
    [SerializeField] public Transform point4;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set some positions
        Vector3[] positions = new Vector3[4];
        positions[0] = point1.position;
        positions[1] = point2.position;
        positions[2] = point3.position;
        positions[3] = point4.position;

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    void Update()
    {
    }
}