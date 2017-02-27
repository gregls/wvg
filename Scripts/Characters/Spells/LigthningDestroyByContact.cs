using UnityEngine;
using System.Collections;

public class LigthningDestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target" || other.tag == "Enemy")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.takeDamage(damage);
            if (other.tag == "Target")
            {
                Destroy(gameObject);
            }
        }
    }
}
