using UnityEngine;
using System.Collections;

public class CreatureMovement : MonoBehaviour {
	float speed = 0.5f;
	Vector3 targetPosition;
	float range = 1f;
	
	float minSleepTime = 0.5f;
	float maxSleepTime = 1.4f;
	float sleepTime;
	bool resting;
	
	Vector2 lookRight = new Vector2(-1, 1);
	Vector2 lookLeft = new Vector2(1, 1);
	
	Color mouseOverColor = Color.blue;
	Color originalColor = Color.yellow;
	bool dragging = false;
	Vector3 offSet;
	
	GameObject currentTouchingCreature;
	Creature creature;
	
	Animator anim;
	
	Transform body;
	
	void Awake() {
		creature = GetComponent<Creature> ();
		body = transform.FindChild("Body");
		anim = GetComponent<Animator> ();
	}
	
	void Start() {
		resting = true;
	}

	void Update() {
		if (dragging) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offSet;;
			pos.z = -5;
			transform.position = pos;
		} else {
			if (resting) {
				sleepTime -= Time.deltaTime;
				if (sleepTime <= 0) {
				 	resting = false;
					NewTargetPosition();
					anim.SetBool("walking", true);
				}
			} else {
				Vector3 pos = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
				pos.z = pos.y;
				transform.position = pos;
				
				if (Vector2.Distance(pos, targetPosition) < speed) {
					sleepTime = Random.Range(minSleepTime, maxSleepTime);
					resting = true;
					anim.SetBool("walking", false);
				}
			}
		}
	}
	
	void NewTargetPosition() {
		targetPosition = transform.position + Random.onUnitSphere * range;
		if (targetPosition.x > transform.position.x) {
			body.transform.localScale = lookRight;
		} else {
			body.transform.localScale = lookLeft;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Wall") {
			targetPosition = -targetPosition;
			NewTargetPosition();
		} else if (dragging && col.tag == "Creature") {
			currentTouchingCreature = col.gameObject;
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (dragging && col.tag == "Creature") {
			currentTouchingCreature = null;
		}
	}
	
	void OnMouseDown() {
		offSet = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		dragging = true;
		anim.SetBool("walking", false);
	}
	
	void OnMouseUp() {
		dragging = false;
		if (currentTouchingCreature != null && currentTouchingCreature.activeSelf) {
			creature.MergeCreatures(currentTouchingCreature.GetComponent<Creature>());
		}
	}
}
