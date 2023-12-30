using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    // Public members

    public GameObject wall;
    public GameObject gameObject;

    // Private members

    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    [SerializeField]
    private int x2;
    [SerializeField]
    private int y2;

    // Initialization

    // TOREVIEW: gameObject se asigna dentro de todos los for y creo que esto no sirve para nada
    void Start()
    {
        float z1 = wall.GetComponent<Renderer>().bounds.size.z + wall.GetComponent<Renderer>().bounds.extents.z;
        float z2 = wall.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < x; i++)
        {
            gameObject = Instantiate(wall, transform.position + Vector3.forward * i * z1, Quaternion.identity);
        }

        for (int i = 0; i < y; i++)
        {
            gameObject = Instantiate(wall, transform.position + Vector3.right * i * z1, Quaternion.Euler(0.0f, 90.0f, 0.0f));
        }

        for (int i = 0; i < x2; i++)
        {
            Vector3 yoffset = Vector3.right * y * z2;
            gameObject = Instantiate(wall, yoffset + transform.position + Vector3.forward * i * z1, Quaternion.identity);
        }

        for (int i = 0; i < y2; i++)
        {
            Vector3 xoffset = Vector3.forward * (x * z2);
            gameObject = Instantiate(wall, xoffset + transform.position + Vector3.right * i * z1, Quaternion.Euler(0.0f, 90.0f, 0.0f));
        }
    }

    // Private methods

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}
