using UnityEngine;

public class LaserDrawer : MonoBehaviour
{

    // Public members
    [HideInInspector]
    public Vector3 p3p;
    public Vector3 p0p;
    public Vector3 p1p;
    public Vector3 p2p;

    public Transform target;
    public Transform startPos;

    // Private members

    private LineRenderer lineRenderer;

    [SerializeField]
    private int numSegments = 100;
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float laserDist = 5.0f;
    [SerializeField]
    private float frequency = 5.0f;
    [SerializeField]
    private float amplitude = 0.5f;
    [SerializeField]
    private float phase = 0.0f;
    [SerializeField]
    private float laserSpeed;

    private float t = 0.5f;
    private float t2 = 0.0f;
    private Vector3 offset1;
    private Vector3 offset2;
    private float noiseAmount = 5.0f;
    private float currentDist = 0.0f;

    private bool isFiring; // TOREVIEW: En ningún sitio se pone a false ni se comprueba. ¿Es correcto?
    private bool isSetUpReady = false;

    private Color color = new Color(1.0f, 0.0f, 0.0f, 0.25f);

    private bool resetDone;

    // Initialization

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Main loop

    // Public methods

    // Private methods
    
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(p3p, 2.0f);
    }

    public void SetupLaser(Transform startPos, Transform target)
    {
        this.startPos = startPos;
        this.target = target;
        p0p = this.startPos.position;

        p1p = p0p + Vector3.one * currentDist;
        p2p = p1p;
        p3p = p1p;
        // p1p = (p3p - p0p) * 0.5f;

        offset1 = new Vector3(
            UnityEngine.Random.Range(startPos.position.x - 5.0f, startPos.position.x + 5.0f),
            UnityEngine.Random.Range(startPos.position.y - 5.0f, startPos.position.y + 5.0f),
            UnityEngine.Random.Range(startPos.position.z - 5.0f, startPos.position.z + 5.0f));

        offset2 = new Vector3(
            UnityEngine.Random.Range(startPos.position.x - 5.0f, startPos.position.x + 5.0f),
            UnityEngine.Random.Range(startPos.position.y - 5.0f, startPos.position.y + 5.0f),
            UnityEngine.Random.Range(startPos.position.z - 5.0f, startPos.position.z + 5.0f));

        isSetUpReady = true;
    }

    private void ResetLaser()
    {
        if (currentDist > 0.0f)
        {
            p1p = p0p + transform.parent.forward * currentDist;
            p2p = p1p;
            p3p = p1p;

            currentDist -= laserSpeed * 3.0f;
        }
        else
        {
            resetDone = true;
        }

        lineRenderer.positionCount = (numSegments);

        for (int i = 0; i < numSegments; i++)
        {
            /* No se está usando
            t = i / (float)numSegments;
            Vector3 pos = p0p * (Mathf.Pow((1.0f - t), 3.0f)) +
                            3.0f * p1p * t * (Mathf.Pow((1.0f - t), 2.0f)) +
                            3.0f * p2p * (t * t) * (1.0f - t) +
                            p3p * (t * t * t);
            //*/

            lineRenderer.SetPosition(i, startPos.position);
        }
    }
    
    public void ShootLaser()
    {
        isFiring = true;

        p0p = startPos.position;

        if (p3p != target.position)
        {
            p3p = Vector3.Lerp(p3p, target.position, currentDist);
            currentDist += laserSpeed;
        }
        
        noiseAmount = UnityEngine.Random.Range(-100.0f, 100.0f);
        offset1 = new Vector3(
            Mathf.Lerp(startPos.position.x - frequency, startPos.position.x + frequency, t2),
            Mathf.Lerp(startPos.position.y - frequency, startPos.position.y + frequency, t2),
            p1p.z);
        noiseAmount = UnityEngine.Random.Range(-100.0f, 100.0f);
        offset2 = new Vector3(Mathf.Lerp(startPos.position.x + frequency, startPos.position.x - frequency, t2),
            Mathf.Lerp(startPos.position.y + frequency, startPos.position.y - frequency, t2),
            p2p.z);

        t2 = amplitude * (1.0f + Mathf.Sin(Time.time * phase));

        p1p = offset1;
        p2p = offset2;
        
        lineRenderer.positionCount = (numSegments);
        for (int i = 0; i < numSegments; i++)
        {
            t = i / (float)numSegments;
            Vector3 pos = p0p * (Mathf.Pow((1.0f - t), 3.0f)) +
                    3.0f * p1p * t * (Mathf.Pow((1.0f - t), 2.0f)) +
                    3.0f * p2p * (t * t) * (1.0f - t) +
                    p3p * (t * t * t);

            lineRenderer.SetPosition(i, pos);
        }
    }
}
