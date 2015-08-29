using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {
	
	public GameObject creaturePrefav;
	
	public Sprite[] headSprites;
	public Sprite[] bodySprites;
	
	public GameObject body;
	SpriteRenderer bodyRenderer;
	public GameObject head;
	SpriteRenderer headRenderer;
	
	float timeToClone;
	
	public CreatureData data;
	
	void Awake() {
		bodyRenderer = body.GetComponent<SpriteRenderer> ();
		headRenderer = head.GetComponent<SpriteRenderer> ();
	}
	
	void Start () {
		if (!data.initialized) {
			SetData(CreatureData.RandomCreature());
		}
	}

	void Update () {
		timeToClone -= Time.deltaTime;
		Debug.Log(timeToClone);
		if (timeToClone <= 0) {
			Clone();
			
			timeToClone = data.CloneRate;
		}
	}
	
	public void SetData(CreatureData _data) {
		data = _data;
		
		bodyRenderer.sprite = bodySprites[data.BodyId];
		headRenderer.sprite = headSprites[data.HeadId];
		timeToClone = data.CloneRate;
	}
	
	void Clone() {
		NewCreature(data);
	}
	
	public void MergeCreatures(Creature other) {
		if (other != null) {
			NewCreature(CreatureData.Merge(this.data, other.data));
				
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
	
	void NewCreature(CreatureData data) {
		GameObject creatureGO = GameObject.Instantiate(creaturePrefav) as GameObject;
		Creature creature = creatureGO.GetComponent<Creature> ();
		
		creature.SetData(data);
		creatureGO.transform.parent = transform.parent;
		creatureGO.transform.position = transform.position;
	}
}
