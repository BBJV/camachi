using UnityEngine;
using System.Collections;

public class Throw : Skill {
	
	public SkillSettingPercent[] skillSetting;
	public Transform targetPosition;
	public ThrowTrack groundObstacle;
	public AudioClip roadBlockAudio;
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		ThrowObstacle(car);	
	}
	
	
	void ThrowObstacle (CarProperty car) {
		skillUsing = true;
		if(roadBlockAudio)
		{
			AudioSource.PlayClipAtPoint(roadBlockAudio, targetPosition.position);
		}
		ThrowTrack newGroundObstacle = Instantiate(groundObstacle, targetPosition.position, targetPosition.rotation) as ThrowTrack;
		newGroundObstacle.selfCar = car;
		newGroundObstacle.speed = Mathf.Clamp(rigidbody.velocity.magnitude * 1.5f, 50.0f, rigidbody.velocity.magnitude * 1.5f);
		newGroundObstacle.liveTime = skillSetting[skillLevel - 1].skillEffectTimes;
		skillUsing = false;
	}
}
