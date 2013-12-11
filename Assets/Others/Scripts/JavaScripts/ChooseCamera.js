var cam : GameObject[];
var al : AudioListener;
var index : int = 0;

function Update () {
	if(Input.GetKeyDown(KeyCode.P))
	{
		if(index + 1 < cam.Length)
		{
			cam[index].camera.enabled = false;
			al = cam[index].GetComponent(AudioListener);
			al.enabled = false;
			index++;
			cam[index].camera.enabled = true;
			al = cam[index].GetComponent(AudioListener);
			al.enabled = true;
		}
		else
		{
			cam[index].camera.enabled = false;
			al = cam[index].GetComponent(AudioListener);
			al.enabled = false;
			index = 0;
			cam[index].camera.enabled = true;
			al = cam[index].GetComponent(AudioListener);
			al.enabled = true;
		}
	}
}