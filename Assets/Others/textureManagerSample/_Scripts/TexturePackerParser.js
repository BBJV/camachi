#pragma strict

public var textureAtlasInfo : TextAsset;

//public var textureAtlas : Texture;
public var textureAtlasMaterial : Material;

private var textureInfo : Vector4;
private var textureWidth : float;
private var textureHeight : float;

private var tempInfo : String;
private var _words : String[];

// -- user objects --

public var object1 : GameObject;
public var object2 : GameObject;
public var object3 : GameObject;
public var object4 : GameObject;
public var object5 : GameObject;
public var object6 : GameObject;
public var object7 : GameObject;
public var object8 : GameObject;
public var objects : GameObject[];
public var names : String[];
// -- END --

function Awake() 
{
	LoadTextureInfo();
}

function Start() 
{
	// -- Set the object and the material for the object --
	
	// call AssignMaterials( theObject : GameObject, theMaterial : String );
	
	//AssignMaterials( object1, "button_blue_256.png" );
	//AssignMaterials( object2, "button_green_256.png" );
	//AssignMaterials( object3, "button_purple_256.png" );
	//AssignMaterials( object4, "button_red_256.png" );
	//AssignMaterials( object5, "button_seagreen_256.png" );
	//AssignMaterials( object6, "button_yellow_256.png" );
	//AssignMaterials( object7, "button_blue_256.png" );
	//AssignMaterials( object8, "button_blue_256.png" );
	var index : int = 0;
	for(var child : GameObject in objects)
	{
		AssignMaterials( child, names[index] );
		index++;
	}
	// -- END --
}

function AssignMaterials( theObject : GameObject, theMaterial : String ) 
{
	// theObject.renderer.material.mainTexture = textureAtlas;
	theObject.renderer.material = textureAtlasMaterial;
	var theOffsets : Vector4 = GetTexInfo( theMaterial );
	
	// Assign material UVs to mesh UVs
	var theMesh : Mesh = theObject.GetComponent(MeshFilter).mesh as Mesh;
	
	var theUVs : Vector2[] = new Vector2[4];
	theUVs = theMesh.uv;
	
	for(var uv : Vector2 in theUVs)
	{
		uv = new Vector2((theOffsets.z - theOffsets.x) * uv.x + theOffsets.x, theOffsets.w * uv.y - theOffsets.y - theOffsets.w);
	}
	// set UV co-ordinates
//	theUVs[2] = Vector2( theOffsets.x, 0 - theOffsets.y );
//	theUVs[3] = Vector2( theOffsets.x + theOffsets.z, 0 - theOffsets.y );
//	theUVs[0] = Vector2( theOffsets.x, 0 - theOffsets.y - theOffsets.w );
//	theUVs[1] = Vector2( theOffsets.x + theOffsets.z, 0 - theOffsets.y - theOffsets.w );
	
	// Assign mesh UVs
	theMesh.uv = theUVs;
}

function LoadTextureInfo() // Read the Text File
{
	//if (textureAtlasInfo == null || textureAtlas == null)
	if (textureAtlasInfo == null || textureAtlasMaterial == null)
	{
		Debug.Log("TEXT INFO FILE or TEXTURE is NOT FOUND");
		return;
	}
	
	//textureWidth = textureAtlas.width;
	//textureHeight = textureAtlas.height;
	textureWidth = textureAtlasMaterial.mainTexture.width;
	textureHeight = textureAtlasMaterial.mainTexture.height;
	
	tempInfo = textureAtlasInfo.text;
	
	tempInfo = tempInfo.Replace("\r", "");
	tempInfo = tempInfo.Replace("\n", "");
	tempInfo = tempInfo.Replace("\t", "");
	tempInfo = tempInfo.Replace("{", "");
	tempInfo = tempInfo.Replace("}", "");
	tempInfo = tempInfo.Replace(":", "");
	tempInfo = tempInfo.Replace(",", "");
	tempInfo = tempInfo.Replace(" ", "");
	
	_words = tempInfo.Split("\""[0]);
	
}

function GetTexInfo( imageName : String ) : Vector4 // Call this to assign material UVs
{
	for (var i : int = 0; i < _words.length; i ++)
	{
		if ( String.Equals( _words[i], imageName ) )
		{
			textureInfo.x = parseFloat( _words[i + 5] ) / textureWidth;
			textureInfo.y = parseFloat( _words[i + 7] ) / textureHeight;
			textureInfo.z = parseFloat( _words[i + 9] ) / textureWidth;
			textureInfo.w = parseFloat( _words[i + 11] ) / textureHeight;
		}
	}
	
	return textureInfo;
}
