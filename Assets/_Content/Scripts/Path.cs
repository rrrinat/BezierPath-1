using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public float radius;
    public List<Transform> points;

    private List<Vector3> _points;

    public int segmentsCount
    {
        get { return (_points.Count - 4) / 3 + 1; }
    }

    public List<Vector3> PointsInSegments(int i)
    {
        return new List<Vector3>()
        {
            _points[i * 3],
            _points[i * 3 + 1],
            _points[i * 3 + 2],
            _points[i * 3 + 3]
        };
    }

    public void Initialize()
    {
        _points = new List<Vector3>();
        foreach (var point in points)
        {
            _points.Add(point.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (points.Count == 0)
        {
            return;
        }

        //spheres
        Gizmos.color = Color.green;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point.position, radius);
        }
        //lines
        Gizmos.color = Color.white;
        var lastPoint = points[0];
        foreach (var point in points)
        {
            Gizmos.DrawLine(point.position, lastPoint.position);
            lastPoint = point;
        }
    }

}
