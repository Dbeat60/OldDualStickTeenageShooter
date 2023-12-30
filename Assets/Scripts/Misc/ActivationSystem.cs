using UnityEngine;
using UnityEngine.Events;

public class ActivationSystem : MonoBehaviour
{

    [Tooltip("If automatic is set, a Trigger Collider is needed")]
    [SerializeField]
    private bool automatic = true;

    public UnityEvent OnActivate = new UnityEvent();
    public UnityEvent OnDeactivate = new UnityEvent();

    [SerializeField]
    private bool locked = false;
    public bool IsLocked => locked;
    public void Lock() => locked = true;
    public void Unlock() => locked = false;

    [SerializeField]
    private bool keyRequired = false;
    [SerializeField]
    [NaughtyAttributes.ShowIf("keyRequired")]
    private Key key;
    private string keyId;
    private bool keyLocked;

    private void Start()
    {
        keyLocked = keyRequired && key != null && key.id != "";
        if (keyLocked)
        {
            keyId = key.id;
        }

        Collider triggerArea = null;
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].isTrigger)
            {
                triggerArea = colliders[i];
                break;
            }
        }
        if (automatic &&
            (triggerArea == null || !triggerArea.isTrigger))
        {
            Debug.LogErrorFormat("ActivationSystem in object {0} has no trigger even though it's set to automatic", name);
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!automatic
            || other == null
            || !other.CompareTag("Player"))
        {
            return;
        }

        Activate();
    }

    public void OnTriggerExit(Collider other)
    {
        if (!automatic
            || other == null
            || !other.CompareTag("Player"))
        {
            return;
        }

        Deactivate();
    }

    private bool CheckLock()
    {
        if (locked)
        {
            return false;
        }

        if (!keyLocked)
        {
            return true;
        }

        keyLocked = !player.HasKey(keyId);
        return !keyLocked;
    }

    public void Activate()
    {
        if (!CheckLock())
        {
            return;
        }

        OnActivate.Invoke();
    }

    public void Deactivate()
    {
        if (locked)
        {
            return;
        }

        OnDeactivate.Invoke();
    }
    
}
