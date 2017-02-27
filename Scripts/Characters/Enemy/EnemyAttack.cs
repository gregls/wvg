using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public int damage = 1;
    public bool canAttackPlayer;

    Animator animator;
	PlayerHealth playerHealth;
	GameObject player;
    EnemyPosition position;

	void Start(){
		animator = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
        position = GetComponent<EnemyPosition>();
        canAttackPlayer = true;
	}

    public void Attack()
    {
        if (canAttackPlayer)
        {
            switch (position.orientation)
            {
                case "S":
                    if (player.transform.position.z > gameObject.transform.position.z 
                        && player.transform.position.z - gameObject.transform.position.z < 1.5f
                        && player.transform.position.z - gameObject.transform.position.z > 0.5f
                        && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < 0.6f)
                    {
                        triggerAttack();
                    }
                    break;
                case "W":
                    if (player.transform.position.x > gameObject.transform.position.x 
                        && player.transform.position.x- gameObject.transform.position.x < 1.5f
                        && player.transform.position.x - gameObject.transform.position.x > 0.5f
                        && Mathf.Abs(player.transform.position.z - gameObject.transform.position.z) < 0.6f)
                    {
                        triggerAttack();
                    }
                    break;
                case "N":
                    if (player.transform.position.z < gameObject.transform.position.z 
                        && gameObject.transform.position.z - player.transform.position.z < 1.5f
                        && gameObject.transform.position.z - player.transform.position.z > 0.5f
                        && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < 0.6f)
                    {
                        triggerAttack();
                    }
                    break;
                case "E":
                    if (player.transform.position.x < gameObject.transform.position.x 
                        && gameObject.transform.position.x - player.transform.position.x < 1.5f
                        && gameObject.transform.position.x - player.transform.position.x > 0.5f
                        && Mathf.Abs(player.transform.position.z - gameObject.transform.position.z) < 0.6f)
                    {
                        triggerAttack();
                    }
                    break;
            }
        }
	}

    void triggerAttack()
    {
        canAttackPlayer = false;
        animator.SetTrigger("canAttack");
        playerHealth.takeDamage(damage);
    }
}
