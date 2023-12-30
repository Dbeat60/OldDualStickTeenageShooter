using UnityEngine;

public class AutomovablePlatform : Automovable
{

    // Private members
    
    private Rigidbody[] passengers;
    private static readonly int MaxPassengers = 1024;
    private int npassengers;
    private bool passengersOnBoard { get { return npassengers > 0; } }

    // Initialization

    private new void Start()
    {
        passengers = new Rigidbody[MaxPassengers];
        npassengers = 0;

        base.Start();
    }

    // Main loop

    private void LateUpdate()
    {
        if (isMoving
            && passengersOnBoard)
        {
            UpdatePassengers();
        }
    }

    // Public methods
    
    public void OnCollisionEnter(Collision collision)
    {
        AddPassenger(collision.gameObject);
    }

    public void OnCollisionExit(Collision collision)
    {
        RemovePassenger(collision.gameObject);
    }

    // Private methods
    
    private void AddPassenger(GameObject passenger)
    {
        Rigidbody rigidbody = passenger != null ? passenger.GetComponent<Rigidbody>() : null;
        if (rigidbody == null
            || npassengers == MaxPassengers)
        {
            return;
        }

        for (int p = 0; p < passengers.Length; ++p)
        {
            if (passengers[p] == null)
            {
                passengers[p] = rigidbody;
                npassengers++;
            }
        }
    }

    private void RemovePassenger(GameObject leaver)
    {
        if (!passengersOnBoard)
        {
            return;
        }

        for (int p = 0; p < passengers.Length; ++p)
        {
            if (passengers[p] != null
                && passengers[p].gameObject.Equals(leaver))
            {
                passengers[p] = null;
                npassengers--;
            }
        }
    }

    private void UpdatePassengers()
    {
        int np = 0;
        for (int p = 0; np < npassengers && p < passengers.Length; ++p)
        {
            if (passengers[p] != null)
            {
                passengers[p].MovePosition(passengers[p].transform.position + positionUpdate);
                np++;
            }
        }
    }

}
