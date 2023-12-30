using System.Collections;
using UnityEngine;

public class ToonEffects : MonoBehaviour
{

    // Public members

    [Range(0.0f, 25.0f)]
    public float smokeSize = 1.0f;

    // Private members

    private SpriteRenderer sprite;

    [Range(0.0f, 2.0f)]
    [SerializeField]
    private float timeToDetach;
    [SerializeField]
    private float timeToDissapear = 0.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float disappearDelta = 0.01f;

    [SerializeField]
    private Vector3 smokeRotation;
    [SerializeField]
    private Vector3 smokeoffset;

    [SerializeField]
    private bool destroy = true;

    // Initialization

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;

        timeToDissapear = 0.0f;

        transform.position += smokeoffset;
        transform.localScale *= smokeSize;
        transform.rotation *= Quaternion.Euler(smokeRotation);
    }

    // Public methods

    // TOREVIEW: A mí no me gusta exponer las corrutinas. En vez de eso prefiero exponer métodos que internamente las inician
    // Ej: public void StartDetach() { ... StartCoroutine(Detach()); ... } y private IEnumerator Detach() { ... }
    // Así cada clase es la única responsable de iniciar y parar sus propias corrutinas

    public IEnumerator Detach()
    {
        yield return new WaitForSeconds(timeToDetach);
        transform.parent = null;
        StartCoroutine(Dissapear());
    }

    public IEnumerator Dissapear()
    {
        yield return new WaitForEndOfFrame();
        sprite.color = Color.Lerp(sprite.color, new Color(0, 0, 0, 0), timeToDissapear);
        timeToDissapear += disappearDelta;
        if (timeToDissapear < 1.0f)
        {
            StartCoroutine(Dissapear());
        }
        else
        {
            if (destroy)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
            }
        }

    }

    
}