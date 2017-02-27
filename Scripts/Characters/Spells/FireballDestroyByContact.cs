using UnityEngine;
using System.Collections;

public class FireballDestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.takeDamage(damage);
            Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, 5f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Enemy" && Vector3.Distance(other.transform.position, hitColliders[i].transform.position) < 1.5f )
                {
                    EnemyHealth colliderHealth = hitColliders[i].GetComponent<EnemyHealth>();
                    colliderHealth.takeDamage(damage);
                }
                if (hitColliders[i].tag == "Player" && Vector3.Distance(other.transform.position, hitColliders[i].transform.position) < 1.5f)
                {
                    PlayerHealth playerHealth = hitColliders[i].GetComponent<PlayerHealth>();
                    playerHealth.takeDamage(damage);
                }
                i++;
            }
            Destroy(gameObject);
        }
    }
}
