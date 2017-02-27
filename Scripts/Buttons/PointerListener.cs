using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

	public string direction;

	bool _pressed = false;
	PlayerMovement playerMovement;

	void Start() {
		playerMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_pressed = true;
		playerMovement.move = true;
		playerMovement.changeMove = true;
		playerMovement.direction = direction;
		//playerMovement.ActionMove ();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pressed = false;
		playerMovement.move = false;
		playerMovement.changeMove = true;
	}

	void Update()
	{
		if (!_pressed)
			return;

		// DO SOMETHING HERE
	}
}
