using UnityEngine;
using System.Collections;

public class MagicMissileDestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int damage;
    public float lifetime;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.takeDamage(damage);
            Destroy(gameObject, lifetime);
        }
    }
}
