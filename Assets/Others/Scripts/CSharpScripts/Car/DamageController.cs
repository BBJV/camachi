using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {
	private bool _canDamageHp = true;
	private bool _canDamageEnergy = true;
	private CarProperty cp;
	private bool repair;
	
	public void SetDamageHp (bool canDamageHp) {
		_canDamageHp = canDamageHp;
	}
	
	public void SetDamageEnergy (bool canDamageEnergy) {
		_canDamageEnergy = canDamageEnergy;
	}
	
	public void ApplyDamageHp (float damage) {
		if(_canDamageHp && !repair)
		{
			if(!cp)
				cp = GetComponent<CarProperty>();
//			cp.ReduceHealthPoint(damage);
		}
	}
	
	public void ApplyDamageEnergy (float damage) {
		if(_canDamageEnergy && !repair)
		{
			if(!cp)
				cp = GetComponent<CarProperty>();
			cp.ReduceEnergy(damage);
		}
	}
	
	void RepairCar () {
		repair = true;
	}
	
	void RepairCarFinish () {
		repair = false;
	}
}
