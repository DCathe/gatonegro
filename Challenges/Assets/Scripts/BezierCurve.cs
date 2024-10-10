using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    // Control points for the Bezier curve
    public Transform point0;
    public Transform point1;
    public Transform point2;

    // Number of segments to divide the curve into (higher = smoother)
    [Range(10, 100)]
    public int segments = 50;

    // Color for the curve
    public Color curveColor = Color.green;

    void OnDrawGizmos()
    {
        // Set the color for drawing the curve
        Gizmos.color = curveColor;

        Vector3 previousPoint = point0.position;

        // Loop through the segments to draw the curve
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 currentPoint = CalculateQuadraticBezierPoint(t);

            // Draw a line between the previous point and the current point on the curve
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }
    }

    // Calculate a point on a quadratic Bezier curve given t (0 to 1)
    public Vector3 CalculateQuadraticBezierPoint(float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * point0.position;        // (1 - t)^2 * P0
        point += 2 * u * t * point1.position;        // 2(1 - t)t * P1
        point += tt * point2.position;               // t^2 * P2

        return point;
    }
}
