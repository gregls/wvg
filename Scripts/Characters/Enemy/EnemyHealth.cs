using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int maxHealth = 1;
	public int currentHealth;
	public bool isDead = false;
    public int destroyDelay = 1;

    Animator animator;

	void Start() {
		currentHealth = maxHealth;
		animator = GetComponent<Animator> ();
	}

	public void takeDamage(int amount){
		currentHealth = currentHealth - amount;

		if (currentHealth <= 0 && !isDead) {
			isDead = true;
			animator.SetTrigger ("die");
			BoxCollider boxCollider = GetComponent<BoxCollider> ();
            boxCollider.enabled = false;
            DestroyEnemyWall();
            Destroy(gameObject, destroyDelay);
            alertOthers();
        }
	}

    public void alertOthers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 5f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy" 
                &&  Vector3.Distance(gameObject.transform.position, hitColliders[i].transform.position) < 1.7f
                && hitColliders[i].GetType() == typeof(BoxCollider))
            {
                EnemyPosition enemyPosition = hitColliders[i].GetComponent<EnemyPosition>();
                enemyPosition.Rotate();
            }
            i++;
        }
    }

    void DestroyEnemyWall()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.2f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "EnemyWall")
            {
                Destroy(hitColliders[i].gameObject);
            }
            i++;
        }
    }
}
