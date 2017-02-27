using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public int screenWidth = 1280;
	public int screenHeight = 800;
    public int level;
	public Text lifeText;
	public Text energyText;
    public Text armorText;
    public Text timerText;
    public Text action1Text;
    public Text action2Text;
    public Text action3Text;
    public Text action4Text;
    public Text action5Text;
    public Text action6Text;
    public Text action7Text;
	public Text action8Text;
	public Text actionMText;
    public int roundTime = 30;
    public int remainingTime;
    public float currentTime;

    GameObject player;
    GameObject target;
    Vector3 clickPosition;
    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    Dictionnary dict;
    bool canTargetEnemy;
    string actionToCall;
    
	void Start () {
		Screen.SetResolution (screenWidth, screenHeight, true);
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
        playerAttack = player.GetComponent<PlayerAttack>();
        GameObject dictGameObject = GameObject.FindGameObjectWithTag ("Dictionnary");
		dict = dictGameObject.GetComponent<Dictionnary> ();
        canTargetEnemy = false;
        remainingTime = roundTime;
        currentTime = Time.fixedTime;
        timerText.text = dict.getSentence("HEALTHBAR_NEXTROUND") + " : " + remainingTime;
    }
	
	void Update ()
    {
        UpdateLifeGUI();
        if (playerHealth.isAlive())
        {
            UpdateEnergyGUI();
            UpdateArmorGUI();
            UpdateTimerGUI();
            UpdateActionBar();
            
			//MOBILE
			if (Input.touchCount == 1 /*|| Input.GetMouseButtonDown(0)*/) {
			//foreach (Touch touch in Input.touches) {
				//if (touch.phase != TouchPhase.Began) {Input.GetTouch(0)
				if (Input.GetTouch(0).phase != TouchPhase.Began) {
					if (canTargetEnemy) {
						//Comme sur mobile on n'est pas précis, si jamais on touche le sol alors on regarde l'ennemi le plus proche
						RaycastHit hitInfo = new RaycastHit ();
						bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.GetTouch(0).position), out hitInfo);
						if (hit) {
							if (hitInfo.transform.gameObject.tag == "Enemy" && actionToCall != null) {
								target = hitInfo.transform.gameObject;
								Invoke (actionToCall, 0);
								canTargetEnemy = false;
							} else if (hitInfo.transform.gameObject.tag == "Untagged"  && actionToCall != null){
								Collider[] hitColliders = Physics.OverlapSphere (hitInfo.point, 0.9f);
								if (hitColliders.Length > 0) {
									if (hitColliders [0].tag == "Enemy") {
										target = hitInfo.transform.gameObject;
										Invoke (actionToCall, 0);
										canTargetEnemy = false;
									}
								}
							}
						}
					} else {
						RaycastHit hitInfo = new RaycastHit();
						bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo);
						if (hit)
						{
							if (hitInfo.transform.gameObject.tag == "Untagged" && actionToCall != null)
							{
								clickPosition = hitInfo.point;
								clickPosition.y = player.transform.position.y;

								Invoke(actionToCall, 0);
							}
						}
					}
				}
			}

			/* PC
            if (canTargetEnemy)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hitInfo = new RaycastHit();
                    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                    if (hit)
                    {
                        if (hitInfo.transform.gameObject.tag == "Enemy")
                        {
                            target = hitInfo.transform.gameObject;
                            Invoke(actionToCall, 0);
                            canTargetEnemy = false;
                        }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hitInfo = new RaycastHit();
                    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                    if (hit)
                    {
                        if (hitInfo.transform.gameObject.tag == "Untagged")
                        {
                            clickPosition = hitInfo.point;
                            clickPosition.y = player.transform.position.y;
                            
                            Invoke(actionToCall, 0);
                        }
                    }
                }
            }
            */
        }
    }

    public Vector3 GetClickPosition()
    {
        return clickPosition;
    }


    void UpdateLifeGUI() {
		lifeText.text = dict.getSentence("HEALTHBAR_LIFE") + " : " + playerHealth.currentHealth + " / " + playerHealth.maxHealth;
    }

    void UpdateEnergyGUI()
    {
        energyText.text = dict.getSentence("HEALTHBAR_ENERGY") + " : " + playerHealth.currentEnergy + " / " + playerHealth.maxEnergy;
    }

    void UpdateArmorGUI() {
        armorText.text = dict.getSentence("HEALTHBAR_ARMOR") + " : " + playerHealth.armor;
    }

    void UpdateTimerGUI()
    {
        if (Time.fixedTime > currentTime + 1f)
        {
            currentTime = Time.fixedTime;
            remainingTime--;
            if (remainingTime == 0)
            {
                remainingTime = roundTime;
                resetEnemiesAttack();
            }
            timerText.text = dict.getSentence("HEALTHBAR_NEXTROUND") + " : " + remainingTime;
        }
    }

    void resetEnemiesAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyAttack enemyattack = enemy.GetComponent<EnemyAttack>();
            enemyattack.canAttackPlayer = true;
        }
    }

    void UpdateActionBar()
    {
        if (action1Text != null) {
            action1Text.text = dict.getSentence("ACTIONBAR_ATTACK");
        }
        if (action2Text != null)
        {
            action2Text.text = dict.getSentence("ACTIONBAR_MAGICMISSILE");
        }
        if (action3Text != null)
        {
            action3Text.text = dict.getSentence("ACTIONBAR_FIREBALL");
        }
        if (action4Text != null)
        {
            action4Text.text = dict.getSentence("ACTIONBAR_LIGTHNING");
        }
        if (action5Text != null)
        {
            action5Text.text = dict.getSentence("ACTIONBAR_ARMOR");
        }
        if (action6Text != null)
        {
            action6Text.text = dict.getSentence("ACTIONBAR_TELEKINESIE");
        }
        if (action7Text != null)
        {
            action7Text.text = dict.getSentence("ACTIONBAR_TELEPORTATION");
        }
        if (action8Text != null)
        {
            action8Text.text = dict.getSentence("ACTIONBAR_RELOAD");
		}
		if (actionMText != null)
		{
			actionMText.text = dict.getSentence("ACTIONBAR_MAINMENU");
		}
    }

    public void MakePlayerAttack()
    {
        playerAttack.MeleeAttack();
    }

    public void CastMagicMissive()
    {
        actionToCall = "MakePlayerCastMagicMissive";
        playerAttack.spellToCast = playerAttack.magicMissile;
        canTargetEnemy = true;
    }

    public void MakePlayerCastMagicMissive()
    {
        playerAttack.MagicMissiveAttack(target);
    }

    public void CastFireball()
    {
        actionToCall = "MakePlayerCastFireball";
        playerAttack.spellToCast = playerAttack.fireball;
        canTargetEnemy = true;
    }

    public void MakePlayerCastFireball()
    {
        playerAttack.FireballAttack(target);
    }

    public void CastLightning()
    {
        actionToCall = "MakePlayerCastLightning";
        playerAttack.spellToCast = playerAttack.lightning;
        canTargetEnemy = true;
    }

    public void MakePlayerCastLightning()
    {
        playerAttack.LightningAttack(target);
    }

    public void CastTeleportation()
    {
        actionToCall = "MakePlayerCastTeleportation";
        playerAttack.spellToCast = playerAttack.teleportation;
        canTargetEnemy = false;
    }

    public void MakePlayerCastTeleportation()
    {
        playerAttack.TeleportationAttack(clickPosition);
    }

    public void CastTelekinesia()
    {
        canTargetEnemy = true;
        actionToCall = "MakePlayerCastTelekinesia";
        playerAttack.spellToCast = playerAttack.telekinesia;
    }

    public void MakePlayerCastTelekinesia()
    {
        playerAttack.TelekinesiaAttack(target);
    }

    public void CastArmor()
    {
        playerAttack.spellToCast = playerAttack.armor;
        playerAttack.CastArmor();
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

    public void Reload()
    {
        SceneManager.LoadScene("Level" + level, LoadSceneMode.Single);
    }

    public void Exit()
    {
        int nextLevel = level + 1;
        SceneManager.LoadScene("Level" + nextLevel, LoadSceneMode.Single);
    }
}
