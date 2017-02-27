using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public int damage = 1;
    public Transform magicSpawn;
    public GameObject magicMissile;
    public GameObject fireball;
    public GameObject lightning;
    public GameObject armor;
    public GameObject teleportation;
    public GameObject telekinesia;
    public GameObject spellToCast;
    public GameObject enemy;
    public int spellSpeed;
    public int magicMissileCost;
    public int fireballCost;
    public int lightningCost;
    public int armorCost;
    public int armorProtection;
    public int teleportationCost;
    public int telekinesiaCost;

    Animator animator;
    PlayerHealth playerHealth;
    bool moveSpellToCast;
    bool attackCac;
    GameObject buffSpell;
    GameObject player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator> ();
        playerHealth = GetComponent<PlayerHealth>();
        moveSpellToCast = false;
        attackCac = false;
    }

    void Update()
    {
        if (moveSpellToCast)
        {
            MoveSpellToCast();
        }
        if (buffSpell != null)
        {
            buffSpell.transform.position = player.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && attackCac)
        {
            attackCac = false;
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.takeDamage(damage);
        }
    }

    public void MeleeAttack()
    {
        attackCac = true;
        animator.SetTrigger("AttackCac");
    }

    public GameObject getSpellToCast()
    {
        return spellToCast;
    }

    void switchTarget(GameObject target)
    {
        GameObject lastTarget = GameObject.FindGameObjectWithTag("Target");
        if (lastTarget)
        {
            lastTarget.tag = "Enemy";
        }
        enemy = target;
        enemy.tag = "Target";
    }

    public void MagicMissiveAttack(GameObject target)
    {
		if (playerHealth.canCastSpell(magicMissileCost) && target != null)
        {
            playerHealth.castSpell(magicMissileCost);
            spellToCast = magicMissile;
            switchTarget(target);
            transform.forward = target.transform.position;
            animator.SetTrigger("AttackMissile");
        }
    }

    public void FireballAttack(GameObject target)
    {
		if (playerHealth.canCastSpell(fireballCost) && target != null)
        {
            playerHealth.castSpell(fireballCost);
            spellToCast = fireball;
            switchTarget(target);
            transform.forward = target.transform.position;
            animator.SetTrigger("AttackMissile");
        }
    }

    public void LightningAttack(GameObject target)
    {
		if (playerHealth.canCastSpell(lightningCost) && target != null)
        {
            playerHealth.castSpell(lightningCost);
            spellToCast = lightning;
            switchTarget(target);
            transform.forward = target.transform.position;
            animator.SetTrigger("AttackMissile");
        }
    }

    public void TeleportationAttack(Vector3 clickPosition)
    {
        if (playerHealth.canCastSpell(teleportationCost))
        {
            playerHealth.castSpell(teleportationCost);
            animator.SetTrigger("Teleportation");            
        }
    }

    public void TelekinesiaAttack(GameObject target)
    {
		if (playerHealth.canCastSpell(telekinesiaCost) && target != null)
        {
            playerHealth.castSpell(telekinesiaCost);
            switchTarget(target);
            transform.forward = -target.transform.position;
            animator.SetTrigger("DirectSpell");
            Object spell = Instantiate(telekinesia, target.transform.position, target.transform.rotation);
            EnemyPosition enemyPosition = target.GetComponent<EnemyPosition>();
            enemyPosition.Rotate();
            Destroy(spell, 1f);
        }
    }

    public void CastArmor()
    {
        if (playerHealth.canCastSpell(armorCost))
        {
            animator.SetTrigger("Buff");
            playerHealth.castSpell(armorCost);
            playerHealth.armor = armorProtection;
        }
    }

    public void MoveSpell(GameObject spell)
    {
        spellToCast = spell;
        moveSpellToCast = true;
    }

    public void AddSpell(GameObject spell)
    {
        buffSpell = spell;
    }

    void MoveSpellToCast()
    {
        if (spellToCast && enemy)
        {
            Vector3 newPos = Vector3.MoveTowards(spellToCast.transform.position, enemy.transform.position, spellSpeed * Time.deltaTime);
            spellToCast.transform.position = newPos;
        } else {
            moveSpellToCast = false;
        }
    }
}
