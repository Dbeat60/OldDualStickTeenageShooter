using UnityEngine;
using UnityEngine.Events;

public class Automovable : MonoBehaviour
{

    // Public members

    public bool isAtInitialPosition
    {
        get
        {
            return !isMoving
                && route.routeCompleted
                && route.currentPoint == initialPosition;
        }
    }

    public bool isAtFinalPosition
    {
        get
        {
            return !isMoving
                && route.routeCompleted
                && route.currentPoint == finalPosition;
        }
    }

    // Protected members

    protected Vector3 positionUpdate { get; private set; }
    protected bool isMoving { get; private set; }

    // Private members

    [Header("Movement")]
    [SerializeField]
    private float speed = 10.0f;

    [Header("Route options")]
    [SerializeField]
    private GameObject routePointsRoot;
    [SerializeField]
    private Route.RouteType routeType = Route.RouteType.OneWay;
    [SerializeField]
    private bool startAwake = true;

    [Header("Direction")]
    [SerializeField]
    private bool goForward = true;

    [Header("Subroute options")]
    [SerializeField]
    private bool forceInitialPosition = false;
    [SerializeField]
    private int initialPosition = 0;
    [SerializeField]
    private bool forceFinalPosition = false;
    [SerializeField]
    private int finalPosition = 0;

    private Route route;
    private Transform[] routePoints;

    private Vector3 target;
    private Vector3 direction;

    // Initialization

    protected void Start()
    {
        isMoving = false;

        bool setUpCorrectly = false;

        if (routePointsRoot != null)
        {
            Transform[] routeTransforms = routePointsRoot.GetComponentsInChildren<Transform>();

            if (routeTransforms != null
                && routeTransforms.Length > 1)
            {
                int pointCount = routeTransforms.Length - 1;
                routePoints = new Transform[pointCount];
                for (int t = 0, t2 = 0; t < routeTransforms.Length; ++t)
                {
                    if (!routeTransforms[t].gameObject.Equals(routePointsRoot))
                    {
                        routePoints[t2] = routeTransforms[t];
                        t2++;
                    }
                }

                if (!forceInitialPosition)
                {
                    initialPosition = goForward ? 0 : pointCount - 1;
                }
                if (!forceFinalPosition)
                {
                    finalPosition = goForward ? pointCount - 1 : 0;
                }

                route = new Route(pointCount,
                    routeType,
                    goForward,
                    initialPosition,
                    finalPosition);

                ResetMoving();

                if (startAwake)
                {
                    StartMoving();
                }

                setUpCorrectly = true;
            }
        }

        if (!setUpCorrectly)
        {
            Debug.LogWarning("Auto-movable " + name + " has no route or it has only one point, so it won't move.");
        }
    }

    // Main loop

    private void Update()
    {
        if (isMoving)
        {
            UpdatePosition();
        }
    }

    // Public methods

    public void StartMoving()
    {
        if (route.TryToGo())
        {
            isMoving = true;
            UpdateRoute();
        }
    }

    public void StopMoving()
    {
        if (isMoving)
        {
            isMoving = false;
        }
    }

    public void StartGoingBack()
    {
        if (route.TryToGoBack())
        {
            isMoving = true;
            UpdateRoute();
        }
    }

    public void ResetMoving()
    {
        if (routePoints != null
            && routePoints.Length > 0)
        {
            StopMoving();
            route.ResetRoute();
            transform.position = routePoints[route.currentPoint].position;
        }
    }

    // Private methods

    private void UpdateRoute()
    {
        route.GetNextPoint();

        if (!route.routeCompleted)
        {
            target = routePoints[route.nextPoint].position;
            direction = (target - transform.position).normalized;
        }
        else
        {
            StopMoving();
        }
    }

    private void UpdatePosition()
    {
        positionUpdate = transform.position;

        float distance = speed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, target);

        int maxIt = routePoints.Length;

        while (isMoving
            && distance > distanceToTarget
            && maxIt > 0)
        {
            transform.position += direction * distanceToTarget;
            distance -= distanceToTarget;

            UpdateRoute();
            distanceToTarget = Vector3.Distance(transform.position, target);
            --maxIt;
        }

        if (maxIt == 0)
        {
            Debug.LogWarning("Could't get a final destination after " + maxIt + " iterations. Maybe it's an infinite loop");
        }

        if (isMoving)
        {
            transform.position += direction * distance;
        }
        
        positionUpdate = transform.position - positionUpdate;
    }

}
