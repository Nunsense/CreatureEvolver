using UnityEngine;
using System.Collections;

public class CreatureMovement : MonoBehaviour {

	public GameObject creaturePrefav;
	
	float speed = 0.5f;
	Vector3 wayPoint;
	float range = 0.5f;
	
	float minSleepTime = 0.2f;
	float maxSleepTime = 0.8f;
	float sleepTime;
	bool sleeping;
	
	Vector2 lookRight = new Vector2(1, 1);
	Vector2 lookLeft = new Vector2(-1, 1);
	
	Color mouseOverColor = Color.blue;
	Color originalColor = Color.yellow;
	bool dragging = false;
	Vector3 offSet;
	
	Creature currentTouchingCreature;
	
	void Start() {
		sleeping = true;
	}

	void Update() {
		if (dragging) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offSet;;
			pos.z = -5;
			transform.position = pos;
		} else {
			if (sleeping) {
				sleepTime -= Time.deltaTime;
				if (sleepTime <= 0) {
				 	sleeping = false;
					NewTargetPosition();
				}
			} else {
				Vector3 pos = Vector3.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
				pos.z = pos.y;
				transform.position = pos;
				
				if ((pos - wayPoint).magnitude < 0.01f) {
					sleepTime = Random.Range(minSleepTime, maxSleepTime);
					sleeping = true;
				}
			}
		}
	}
	
	void NewTargetPosition() {
		wayPoint = new Vector3(
			Random.Range(transform.position.x - range, transform.position.x + range), 
			Random.Range(transform.position.y - range, transform.position.y + range), 
			transform.position.z
		);
		if (wayPoint.x > transform.position.x) {
			transform.localScale = lookRight;
		} else {
			transform.localScale = lookLeft;
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Wall") {
			wayPoint = -wayPoint;
		} else if (dragging && col.tag == "Creature") {
			currentTouchingCreature = col.GetComponent<Creature> ();
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
	}
	
	void OnMouseUp() {
		dragging = false;
		if (currentTouchingCreature != null) {
			MergeCreatures(currentTouchingCreature);
		}
	}
	
	void MergeCreatures(Creature other) {
		GameObject creatureGO = GameObject.Instantiate(creaturePrefav) as GameObject;
		creatureGO.transform.position = transform.position;
		Creature creature = creatureGO.GetComponent<Creature> ();
		creature.MergeCreatures(GetComponent<Creature> (), other);
	}
}
