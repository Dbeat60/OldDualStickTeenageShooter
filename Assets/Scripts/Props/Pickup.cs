using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{

    // Public members

    public UnityEvent PickedUp;

    // Initialization

    private void Start()
    {
        bool triggerFound = false;

        Collider []colliders = GetComponents<Collider>();

        if (colliders != null)
        {
            for (int c = 0; c < colliders.Length; ++c)
            {
                if (colliders[c].isTrigger)
                {
                    triggerFound = true;
                    break;
                }
            }
        }

        if (!triggerFound)
        {
            Debug.LogWarning("No trigger collider found on PickUp " + name);
        }
    }

    // Private methods

    private void OnTriggerEnter(Collider collider)
    {
        if (collider != null
            && collider.CompareTag("Player"))
        {
            Pick();
        }
    }

    virtual protected void Pick()
    {
        PickedUp.Invoke();
        Destroy(gameObject);
    }

}
