using UnityEngine;
using System.Collections;

[System.Serializable] public class CreatureData {
	
	public bool initialized = false;
	public int HeadId;
	public int BodyId;
	public float SicknessRate;
	public float CloneRate;
	
	public static CreatureData Merge(CreatureData data1, CreatureData data2) {
		CreatureData newData = new CreatureData();

		if (Random.value < 0.5f) {
			newData.BodyId = data1.BodyId;
			Debug.Log(newData.BodyId);
			newData.CloneRate = Mutate(data1.CloneRate);
		} else {
			newData.BodyId = data2.BodyId;
			Debug.Log(newData.BodyId);
			newData.CloneRate = Mutate(data2.CloneRate);
		}

		if (Random.value < 0.5f) {
			newData.HeadId = data1.HeadId;
			Debug.Log(newData.HeadId);
			newData.SicknessRate = Mutate(data1.SicknessRate);
		} else {
			newData.HeadId = data2.HeadId;
			Debug.Log(newData.HeadId);
			newData.SicknessRate = Mutate(data2.SicknessRate);
		}
		
		newData.initialized = true;
		return newData;
	}
	
	public static CreatureData RandomCreature() {
		switch(Random.Range(0, 3)) {
		case 0 : return MooseData();
		case 1 : return DolphinData();
		case 2 : return GekoData();
		}
		return MooseData();
	}
	
	public static CreatureData MooseData() {
		CreatureData newData = new CreatureData();
		newData.BodyId = 0;
		newData.HeadId = 0;
		newData.CloneRate = Mutate(10.0f);
		newData.SicknessRate = Mutate(0.01f);
		newData.initialized = true;
		return newData;
	}
	
	public static CreatureData DolphinData() {
		CreatureData newData = new CreatureData();
		newData.BodyId = 1;
		newData.HeadId = 1;
		newData.CloneRate = Mutate(20.0f);
		newData.SicknessRate = Mutate(0.3f);
		newData.initialized = true;
		return newData;
	}
	
	public static CreatureData GekoData() {
		CreatureData newData = new CreatureData();
		newData.BodyId = 2;
		newData.HeadId = 2;
		newData.CloneRate = Mutate(15.4f);
		newData.SicknessRate = Mutate(0.05f);
		newData.initialized = true;
		return newData;
	}
	
	public static float Mutate(float value) {
		return value * Random.Range(0.9f, 1.1f);
	}
}
