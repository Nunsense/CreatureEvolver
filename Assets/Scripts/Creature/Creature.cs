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
	
	float timeToDuplicate;
	
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
		timeToDuplicate -= Time.deltaTime;
		if (timeToDuplicate <= 0) {
			
			timeToDuplicate = data.DuplicateRate;
		}
	}
	
	public void SetData(CreatureData _data) {
		data = _data;
		SetSprites();
	}
	
	void SetSprites() {
		bodyRenderer.sprite = bodySprites[data.BodyId];
		headRenderer.sprite = headSprites[data.HeadId];
	}
	
	public void MergeCreatures(Creature other) {
		if (other != null) {
			Creature creature = NewCreature();
			
			creature.SetData(CreatureData.Merge(this.data, other.data));
			creature.gameObject.SetActive(true);
				
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
	
	Creature NewCreature() {
		GameObject creatureGO = GameObject.Instantiate(creaturePrefav) as GameObject;
		creatureGO.SetActive(false);
		creatureGO.transform.position = transform.position;
		return creatureGO.GetComponent<Creature> ();
	}
}
