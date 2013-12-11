using UnityEngine;
using System.Collections;

public class MaterialController : MonoBehaviour {
	//public Material[] MyMaterial;
	//public string ShaderPath;
	private Color[] OriginalColor;
	//scene depend
	private Color BasicColor;
	private Color skillColor;
	private string _color = "_Color";
	// Use this for initialization
	void Start () {
		/*
        defaultFireballAMaterial = new Material(Shader.Find("Toon/Basic Outline"));
		defaultFireballAMaterial.name = "Default";
        Texture2D tex = Resources.Load("Cars/AC2") as Texture2D;
		defaultFireballAMaterial.SetColor("_TintColor", Color.white);
		defaultFireballAMaterial.mainTexture = tex;
		defaultFireballAMaterial.mainTextureScale = new Vector2(1.0f, 1f);
		defaultFireballAMaterial.t
		*/
		//this.renderer.material = new Material(Shader.Find("Toon/Basic Outline"));
		OriginalColor = new Color[transform.renderer.materials.Length];
		BasicColor = Camera.main.GetComponent<SceneMaterial>().BasicColor;
		skillColor = Color.white;
		//this.renderer.materials = new Material[MyMaterial.Length];
		//MyMaterial = this.renderer.materials;
		
		for(int i = 0 ; i < transform.renderer.materials.Length ; i++)
		{
			//print("MyMaterial[i] = " + MyMaterial[i].shader.name);
			//this.renderer.materials[i] = new Material(Shader.Find("Toon/Basic Outline"));//new Material(MyMaterial[i].shader);
			//this.renderer.materials[i].CopyPropertiesFromMaterial(MyMaterial[i]);
			if(transform.renderer.materials[i].HasProperty(_color))
			{
				OriginalColor[i] = transform.renderer.materials[i].color;
				transform.renderer.materials[i].color = BasicColor;
			}
		}
		//this.renderer.material = new Material(Shader.Find(ShaderPath));
		//this.renderer.material.CopyPropertiesFromMaterial(MyMaterial);
		//OriginalColor = this.renderer.material.color;
		//print("OriginalColor = " +OriginalColor);
	}
	
	void MyChangeColor(Color c){
		for(int i = 0 ; i < transform.renderer.materials.Length ; i++){
			if(transform.renderer.materials[i].HasProperty(_color))
			{
				transform.renderer.materials[i].color = c * skillColor;
			}
		}
		
		//this.renderer.material.color = new Color(OriginalColor.r/4,OriginalColor.g/4,OriginalColor.b/4);
	}
	
	void ResetColor(){
		for(int i = 0 ; i < transform.renderer.materials.Length ; i++){
			if(transform.renderer.materials[i].HasProperty(_color))
			{
				transform.renderer.materials[i].color = BasicColor * skillColor;
			}
		}
		//this.renderer.material.color = new Color(OriginalColor.r/4,OriginalColor.g/4,OriginalColor.b/4);
	}
	
	float changeTime = 0;
	Color finalColor = Color.clear;
	Color startColor = Color.clear;
	bool changingColor = false;
	IEnumerator SkillColor(Color color) {
		if(transform.renderer.material.HasProperty(_color))
		{
			startColor = transform.renderer.material.color;
		}
		finalColor = color;
		changeTime = 0;
		if(changingColor)
		{
			yield break;
		}
		changingColor = true;
		while(skillColor != finalColor)
		{
			skillColor = Color.Lerp(startColor, finalColor, changeTime);
			changeTime += Time.deltaTime;
			ResetColor();
			yield return null;
		}
		changingColor = false;
	}
}
