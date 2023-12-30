using UnityEngine;

public class Mine : MonoBehaviour
{

    // Public members
    
    public GameObject explosionParticles;
    public int damage = 15;
    public float lifeSpan = 2.0f;

    // Private members

    public bool damagesPlayer = false;

    // Initialization
    
    // Destroy

    void OnDestroy()
    {
       
    }

    // Private methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            damagesPlayer = true;
            StartSelfdestruction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        damagesPlayer = false;
    }

    private void StartSelfdestruction()
    {
        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("explode");
        }
        GameObject particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(particles, 1.0f);

        if (damagesPlayer)
        {
            player.Instance.playerHealthManager.callUpdateHealth(damage);
        }
        Destroy(gameObject, lifeSpan);
    }

}
