using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Color lineColor = Color.blue;
    public float lineWidth = 0.1f;

    private LineRenderer line;

    void Start()
    {
        // Create and set up the LineRenderer
        line = gameObject.AddComponent<LineRenderer>();
        SetupLineRenderer(line, lineColor);
    }

    void Update()
    {
        startPoint = inputReader.player.transform.position;
        endPoint = inputReader.MouseWorldPosition;

        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
    }

    void SetupLineRenderer(LineRenderer lineRenderer, Color color)
    {
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineRenderer.endColor = color;
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
    }
}
