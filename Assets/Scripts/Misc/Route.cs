
using UnityEngine;

public class Route
{

    // Public members

    public enum RouteType
    {
        OneWay,
        OneCycle,
        InfiniteCycles,
        OneTicToc,
        InfiniteTicTocs
    }

    public int currentPoint { get; private set; }
    public int nextPoint { get; private set; }

    public bool routeStarted { get; private set; }
    public bool routeCompleted { get; private set; }

    // Private members

    private RouteType routeType;
    private int pointCount;
    private bool goingForwardByDefault;
    
    private int initialPosition;
    private int finalPosition;

    private bool goingForward;
    private bool oneCycleCompleted;
    private bool endOnNextCycle;

    // Initialization

    public Route(int pointCount,
        RouteType routeType,
        bool goForward,
        int initialPosition,
        int finalPosition)
    {
        SetRouteData(pointCount,
            routeType,
            goForward,
            initialPosition,
            finalPosition);
    }

    public void SetRouteData(int pointCount,
        RouteType routeType,
        bool goForward,
        int initialPosition,
        int finalPosition)
    {
        this.pointCount = pointCount > 0 ? pointCount : 0;
        this.routeType = routeType;
        goingForwardByDefault = goForward;
        goingForward = goForward;
        
        if (goingForwardByDefault)
        {
            this.initialPosition = Mathf.Clamp(initialPosition, 0, this.pointCount - 1);
            this.finalPosition = Mathf.Clamp(finalPosition, this.initialPosition, this.pointCount - 1);
        }
        else
        {
            this.initialPosition = Mathf.Clamp(initialPosition, 0, this.pointCount - 1);
            this.finalPosition = Mathf.Clamp(finalPosition, 0, this.initialPosition);
        }
        
        ResetRoute();
    }

    // Public methods

    public bool TryToGo()
    {
        if (pointCount < 2
            || (routeStarted && goingForward == goingForwardByDefault)
            || (routeType == RouteType.OneWay && currentPoint == finalPosition && nextPoint == finalPosition)
            || ((routeType == RouteType.OneTicToc || routeType == RouteType.InfiniteTicTocs) && goingForward != goingForwardByDefault)
            )
        {
            endOnNextCycle = false;
            return false;
        }

        routeStarted = true;
        routeCompleted = false;
        oneCycleCompleted = false;
        endOnNextCycle = false;
        currentPoint = nextPoint;
        goingForward = goingForwardByDefault;

        return routeStarted;
    }

    public bool TryToGoBack()
    {
        if (pointCount < 2
            || (routeStarted && goingForward != goingForwardByDefault)
            || (currentPoint == initialPosition && nextPoint == initialPosition)
            || ((routeType == RouteType.OneCycle || routeType == RouteType.InfiniteCycles) && nextPoint == initialPosition))
        {
            endOnNextCycle = true;
            return false;
        }

        routeStarted = true;
        routeCompleted = false;
        oneCycleCompleted = false;
        endOnNextCycle = true;
        currentPoint = nextPoint;
        goingForward = !goingForwardByDefault;

        return routeStarted;
    }

    public int GetNextPoint()
    {
        if (!routeStarted)
        {
            return nextPoint;
        }

        currentPoint = nextPoint;

        if (routeType == RouteType.OneWay)
        {
            if (!routeCompleted)
            {
                if (goingForwardByDefault)
                {
                    if (goingForward)
                    {
                        nextPoint = nextPoint < finalPosition ? nextPoint + 1 : finalPosition;
                        if (currentPoint == finalPosition)
                        {
                            FinishRoute();
                            ChangeDirection();
                        }
                    }
                    else
                    {
                        nextPoint = nextPoint > initialPosition ? nextPoint - 1 : initialPosition;
                        if (currentPoint == initialPosition)
                        {
                            FinishRoute();
                            ChangeDirection();
                        }
                    }
                }
                else
                {
                    if (!goingForward)
                    {
                        nextPoint = nextPoint > finalPosition ? nextPoint - 1 : finalPosition;
                        if (currentPoint == finalPosition)
                        {
                            FinishRoute();
                            ChangeDirection();
                        }
                    }
                    else
                    {
                        nextPoint = nextPoint < initialPosition ? nextPoint + 1 : initialPosition;
                        if (currentPoint == initialPosition)
                        {
                            FinishRoute();
                            ChangeDirection();
                        }
                    }
                }
            }
        }
        else if (routeType == RouteType.OneCycle
            || (routeType == RouteType.InfiniteCycles && endOnNextCycle))
        {
            if (!routeCompleted)
            {
                if (goingForwardByDefault)
                {
                    if (goingForward)
                    {
                        nextPoint = nextPoint < finalPosition ? nextPoint + 1 : initialPosition;
                    }
                    else
                    {
                        nextPoint = nextPoint > initialPosition ? nextPoint - 1 : initialPosition;
                    }
                }
                else
                {
                    if (!goingForward)
                    {
                        nextPoint = nextPoint > finalPosition ? nextPoint - 1 : initialPosition;
                    }
                    else
                    {
                        nextPoint = nextPoint < initialPosition ? nextPoint + 1 : initialPosition;
                    }
                }

                oneCycleCompleted |= nextPoint == initialPosition && currentPoint != initialPosition;
                if (oneCycleCompleted
                    && currentPoint == initialPosition)
                {
                    nextPoint = currentPoint;
                    FinishRoute();
                }
            }
        }
        else if (routeType == RouteType.InfiniteCycles)
        {
            if (goingForwardByDefault)
            {
                if (goingForward)
                {
                    nextPoint = nextPoint < finalPosition ? nextPoint + 1 : initialPosition;
                }
                else
                {
                    nextPoint = nextPoint > initialPosition ? nextPoint - 1 : initialPosition;
                }
            }
            else
            {
                if (!goingForward)
                {
                    nextPoint = nextPoint > finalPosition ? nextPoint - 1 : initialPosition;
                }
                else
                {
                    nextPoint = nextPoint < initialPosition ? nextPoint + 1 : initialPosition;
                }
            }

            oneCycleCompleted |= nextPoint == initialPosition && currentPoint != initialPosition;
        }
        else if (routeType == RouteType.OneTicToc
            || (routeType == RouteType.InfiniteTicTocs && endOnNextCycle))
        {
            if (!routeCompleted)
            {
                if (goingForwardByDefault)
                {
                    if (goingForward)
                    {
                        nextPoint = nextPoint < finalPosition ? nextPoint + 1 : finalPosition;
                        goingForward = currentPoint < finalPosition;
                    }
                    else
                    {
                        nextPoint = nextPoint > initialPosition ? nextPoint - 1 : initialPosition;
                        if (currentPoint == initialPosition)
                        {
                            FinishRoute();
                        }
                    }
                }
                else
                {
                    if (!goingForward)
                    {
                        nextPoint = nextPoint > finalPosition ? nextPoint - 1 : finalPosition;
                        goingForward = currentPoint == finalPosition;
                    }
                    else
                    {
                        nextPoint = nextPoint < initialPosition ? nextPoint + 1 : initialPosition;
                        if (currentPoint == initialPosition)
                        {
                            FinishRoute();
                        }
                    }
                }
            }
        }
        else if (routeType == RouteType.InfiniteTicTocs)
        {
            if (goingForwardByDefault)
            {
                if (goingForward)
                {
                    nextPoint = nextPoint < finalPosition ? nextPoint + 1 : finalPosition;
                    goingForward = currentPoint < finalPosition;
                }
                else
                {
                    nextPoint = nextPoint > initialPosition ? nextPoint - 1 : initialPosition;
                    goingForward = currentPoint == initialPosition;
                }
            }
            else
            {
                if (!goingForward)
                {
                    nextPoint = nextPoint > finalPosition ? nextPoint - 1 : finalPosition;
                    goingForward = currentPoint == finalPosition;
                }
                else
                {
                    nextPoint = nextPoint < initialPosition ? nextPoint + 1 : initialPosition - 1;
                    goingForward = currentPoint < initialPosition;
                }
            }
        }
        
        return nextPoint;
    }

    public void ChangeDirection()
    {
        goingForward = !goingForward;
    }

    public void ResetRoute()
    {
        currentPoint = initialPosition;
        nextPoint = currentPoint;

        goingForward = goingForwardByDefault;
        routeStarted = false;
        routeCompleted = true;
    }

    // Private methods

    private void FinishRoute()
    {
        routeStarted = false;
        routeCompleted = true;
        oneCycleCompleted = false;
        endOnNextCycle = false;
        goingForward = goingForwardByDefault;
    }

}
