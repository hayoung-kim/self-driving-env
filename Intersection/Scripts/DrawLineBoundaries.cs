using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBoundaries : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 thisPosition;
    // Start is called before the first frame update

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        thisPosition = transform.position;
        
        // Set some positions
        Vector3[] positions = new Vector3[3];
        positions[0] = new Vector3(0.0f, 2.0f, 0.0f) - thisPosition;
        positions[1] = new Vector3(10.0f, 2.0f, 0.0f) - thisPosition;
        positions[2] = new Vector3(0.0f, 2.0f, 10.0f) - thisPosition;

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
