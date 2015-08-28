using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public int bodyLevel = 0;
	public int headLevel = 0;
	
	public Sprite[] headSprites;
	public Sprite[] bodySprites;
	
	public GameObject body;
	SpriteRenderer bodyRenderer;
	public GameObject head;
	SpriteRenderer headRenderer;
	
	void Awake() {
		bodyRenderer = body.GetComponent<SpriteRenderer> ();
		headRenderer = head.GetComponent<SpriteRenderer> ();
	}
	
	void Start () {
		bodyLevel = Random.Range(0, bodySprites.Length);
		headLevel = Random.Range(0, headSprites.Length);
		SetSprites();
	}

	void Update () {
	
	}
	
	void SetSprites() {
		bodyRenderer.sprite = bodySprites[bodyLevel];
		headRenderer.sprite = headSprites[headLevel];
	}
	
	public void MergeCreatures(Creature one, Creature other) {
		if (one.bodyLevel == other.bodyLevel) {
			bodyLevel = one.bodyLevel + 1;
		} else {
			bodyLevel = Random.Range(
				Mathf.Min(one.bodyLevel, other.bodyLevel), 
				Mathf.Max(one.bodyLevel, other.bodyLevel)
			);
		}
		if (one.headLevel == other.headLevel) {
			headLevel = one.headLevel + 1;
		} else {
			headLevel = Random.Range(
				Mathf.Min(one.headLevel, other.headLevel), 
				Mathf.Max(one.headLevel, other.headLevel)
			);
		}
		SetSprites();
	}
}
