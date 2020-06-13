using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    [Range(0f, 1f)]
    public float step;

    private Path _path;

    private void Start()
    {
        _path = FindObjectOfType<Path>();
        _path.Initialize();
    }

    private void Update()
    {
        Test();
    }

    private void Test()
    {
        for (int i = 0; i < _path.segmentsCount; i++)
        {
            var points = _path.PointsInSegments(i);
            DrawCurve(points);
        }
    }

    private void DrawCurve(List<Vector3> points)
    {
        var drawPoints = new Queue<Vector3>();

        for (float i = 0; i < 1; i = i + step)
        {
            var point = Evaluate(points[0], points[1], points[2], points[3], i);
            drawPoints.Enqueue(point);
        }

        var lastPoint = drawPoints.Dequeue();
        while (drawPoints.Count > 0)
        {
            var current = drawPoints.Dequeue();
            Debug.DrawLine(lastPoint, current, Color.red);

            lastPoint = current;
        }
    }

    private Vector3 Evaluate(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    {
        var t = Mathf.Clamp01(value);
        var difference = 1f - t;

        return Mathf.Pow(difference, 3) * p1 + 3 * Mathf.Pow(difference, 2) * t * p2 +
               3 * difference * t * t * p3 + t * t * t * p4;
    }

}
