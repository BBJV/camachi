var cc : ChooseCamera;
var changeIndex : int;

function OnTriggerEnter (other : Collider) {
	cc.cam[cc.index].camera.enabled = false;
	cc.al = cc.cam[cc.index].GetComponent(AudioListener);
	cc.al.enabled = false;
	cc.index = changeIndex;
	cc.cam[cc.index].camera.enabled = true;
	cc.al = cc.cam[cc.index].GetComponent(AudioListener);
	cc.al.enabled = true;
	
	gameObject.active = false;
}