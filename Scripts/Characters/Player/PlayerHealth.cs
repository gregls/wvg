using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth = 5;
	public int maxEnergy = 5;
    public int armor = 0;
    public int currentHealth;
	public int currentEnergy;

	Animator animator;
	bool isDead = false;

	void Start() {
		currentHealth = maxHealth;
		currentEnergy = maxEnergy;
		animator = GetComponent<Animator> ();
	}

	public void takeDamage(int amount){
		currentHealth = currentHealth - amount;

		if (currentHealth <= 0 && !isDead) {
			isDead = true;
			animator.SetTrigger ("Die");
		}
    }

    public void castSpell(int amount)
    {
        if (canCastSpell(amount))
        {
            currentEnergy = currentEnergy - amount;
        }
    }

    public bool canCastSpell(int amount)
	{
        return currentEnergy - amount >= 0;
    }

    public bool isAlive()
    {
        return !isDead;
    }
}
