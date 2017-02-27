using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;

	Animator animator;
	Rigidbody playerRigidbody;
    PlayerHealth playerHealth;
    Vector3 movement;
	Vector3 pointToMove;
	public bool move = false;
	public bool changeMove = false;
	public string direction = "";
	int floorMask;

	void Awake() {
		animator = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		playerHealth = GetComponent<PlayerHealth>();
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (!move && changeMove) {
			animator.SetBool ("IsWalking", false);
			playerRigidbody.velocity = transform.TransformDirection(new Vector3(0,0,0));
			changeMove = false;
		} else if(move && playerHealth.isAlive ()) {
			ActionMove ();
			changeMove = false;
			//playerRigidbody.velocity = transform.TransformDirection(new Vector3(-10,0,0));
		}
		triggerEnemyAttack();
		triggerExit();

		/* Mobile Clic
		 if (playerHealth.isAlive ()) {
			triggerEnemyAttack ();
			triggerExit ();
			if (Mathf.Abs (transform.position.x - pointToMove.x) < 0.1f && Mathf.Abs (transform.position.z - pointToMove.z) < 0.1f) {
				move = false;
			}
			Animate ();
			if (move) {
				Move (pointToMove.x, pointToMove.z);
			}
		}

		if (Input.touchCount == 1) {
			//foreach (Touch touch in Input.touches) {
				if (Input.GetTouch(0).phase == TouchPhase.Stationary) {
					RaycastHit hitInfo = new RaycastHit ();
					bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.GetTouch(0).position), out hitInfo);
					if (hit) {
						if (hitInfo.transform.gameObject.tag == "Untagged" || hitInfo.transform.gameObject.tag == "Exit") {
							pointToMove = hitInfo.point;
							move = true;
							if (playerHealth.isAlive ()) {
								Move (pointToMove.x, pointToMove.z);
								Animate ();
								Turning (pointToMove.x, pointToMove.z);
								triggerEnemyAttack ();
								triggerExit ();
							}
						}
					}
				}
			//}
		}
		*/
		/* PC
		float h = -Input.GetAxisRaw ("Horizontal");
		float v = -Input.GetAxisRaw ("Vertical");
        if (playerHealth.isAlive())
        {
            Move(h, v);
            Animate(h, v);
            Turning(h, v);
            triggerEnemyAttack();
            triggerExit();
        }*/
    }


	/* Mobile Clic
	void Move (float h, float v) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(h, 0, v), step);
	}
	
	void Animate () {
		animator.SetBool ("IsWalking", move);
	}
	*/
	/* PC 
	void Move (float h, float v) {
        movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Animate (float h, float v) {
		bool walking = h != 0f || v != 0f ;
		animator.SetBool ("IsWalking", walking);
	}
	
	void Turning(float h, float v) {
		if (h < 0f) { //haut
            transform.forward = new Vector3(-1f, 0f, 0f);
            //transform.rotation = Quaternion.AngleAxis(-90f, new Vector3(0, 1, 0));
		} else if (h > 0f) { //bas
            transform.forward = new Vector3(1f, 0f, 0f);
            //transform.rotation = Quaternion.AngleAxis(90f, new Vector3(0, 1, 0));
		} else if (v < 0f) { //droite
            transform.forward = new Vector3(0f, 0f, -1f);
            //transform.rotation = Quaternion.AngleAxis(180f, new Vector3(0, 1, 0));
		} else if (v > 0f) { //gauche
            transform.forward = new Vector3(0f, 0f, 1f);
            //transform.rotation = Quaternion.AngleAxis(0f, new Vector3(0, 1, 0));
		}
	}*/

	/* Mobile
	void Turning(float h, float v) {
		transform.forward = new Vector3 ((transform.position.x - h) / transform.position.x, 0f, (transform.position.z - v) / transform.position.z);
	}*/

    void triggerEnemyAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 1.3f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                Vector3 toTarget = (gameObject.transform.position - hitColliders[i].transform.position).normalized;
                if (Vector3.Dot(toTarget, hitColliders[i].transform.forward) > 0 && Vector3.Dot(toTarget, hitColliders[i].transform.forward) <1f)
                {
                    EnemyAttack attack = hitColliders[i].GetComponent<EnemyAttack>();
                    attack.Attack();
                }
            }
            i++;
        }
    }

    void triggerExit()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.3f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Exit")
            {
                GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                gc.Exit();
            }
            i++;
        }
    }

	public void ActionMove()
	{
		float h, v, xMovement, zMovement;
		h = v = 0;
		switch (direction) {
			case "D": 
				h = -1;
				break;
			case "G": 
				h = 1;
				break;
			case "B": 
				v = 1;
				break;
			case "H": 
				v = -1;
				break;	
		}

		if (h != 0) {
			xMovement = Mathf.Abs (v);
			zMovement = Mathf.Abs (h);
		} else {
			xMovement = Mathf.Abs (h);
			zMovement = Mathf.Abs (v);
		}
		transform.forward = new Vector3(h, 0f, v);
		animator.SetBool ("IsWalking", true);
		playerRigidbody.velocity = transform.TransformDirection(new Vector3(xMovement * speed,0,zMovement * speed));
	}
}
