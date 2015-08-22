using UnityEngine;
using System.Collections;

public class CreatureMovement : MonoBehaviour {

	float speed = 0.5f;
	Vector3 wayPoint;
	float range = 0.5f;
	
	float minSleepTime = 0.2f;
	float maxSleepTime = 0.8f;
	float sleepTime;
	bool sleeping = true;
	
	Vector2 lookRight = new Vector2(1, 1);
	Vector2 lookLeft = new Vector2(-1, 1);
	
	void Start() {
	}
	
	void Update() {
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
		}
	}
}
