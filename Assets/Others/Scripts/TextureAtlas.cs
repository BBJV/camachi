using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextureAtlas : MonoBehaviour {
	protected UIAtlas.Sprite mSprite;
	[HideInInspector][SerializeField] UIAtlas mAtlas;
	[HideInInspector][SerializeField] string mSpriteName;
	[HideInInspector][SerializeField] Texture mTex;
	[HideInInspector][SerializeField] Material mMat;
	[HideInInspector][SerializeField] Mesh mMesh;
	protected Rect mOuterUV;
	bool mSpriteSet = false;
	protected bool mChanged = true;
	protected Rect mOuter;
	
	public UIAtlas atlas
	{
		get
		{
			return mAtlas;
		}
		set
		{
			if (mAtlas != value)
			{
				mAtlas = value;
				mSpriteSet = false;
				mSprite = null;
				// Update the material
				material = (mAtlas != null) ? mAtlas.spriteMaterial : null;
//
//				// Automatically choose the first sprite
				if (string.IsNullOrEmpty(mSpriteName))
				{
					if (mAtlas != null && mAtlas.spriteList.Count > 0)
					{
						sprite = mAtlas.spriteList[0];
						mSpriteName = mSprite.name;
					}
				}

				// Re-link the sprite
				if (!string.IsNullOrEmpty(mSpriteName))
				{
					string sprite = mSpriteName;
					mSpriteName = "";
					spriteName = sprite;
					mChanged = true;
					UpdateUVs(true);
				}
			}
		}
	}
	
	public string spriteName
	{
		get
		{
			return mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				// If the sprite name hasn't been set yet, no need to do anything
				if (string.IsNullOrEmpty(mSpriteName)) return;

				// Clear the sprite name and the sprite reference
				mSpriteName = "";
				mSprite = null;
				mChanged = true;
			}
			else if (mSpriteName != value)
			{
				// If the sprite name changes, the sprite reference should also be updated
				mSpriteName = value;
				mSprite = null;
				mChanged = true;
				if (mSprite != null) UpdateUVs(true);
			}
		}
	}
	
	public Texture mainTexture
	{
		get
		{
			// If the material has a texture, always use it instead of 'mTex'.
			Material mat = material;
			if (mat != null)
			{
				if (mat.mainTexture != null)
				{
					mTex = mat.mainTexture;
				}
				else if (mTex != null)
				{
					// The material has no texture, but we have a saved texture
//					if (mPanel != null) mPanel.RemoveWidget(this);

					// Set the material's texture to the saved value
//					mPanel = null;
					mMat.mainTexture = mTex;

					// Ensure this widget gets added to the panel
//					CreatePanel();
				}
			}
			return mTex;
		}
		set
		{
			if (mainTexture != value)
			{
//				if (mPanel != null) mPanel.RemoveWidget(this);

//				mPanel = null;
				mTex = value;

				if (mMat != null)
				{
					mMat.mainTexture = value;
//					CreatePanel();
				}
			}
		}
	}
	
	public Material material
	{
		get
		{
			return mMat;
		}
		set
		{
			if (mMat != value)
			{
//				if (mMat != null && mPanel != null) mPanel.RemoveWidget(this);

//				mPanel = null;
				mMat = value;
				mTex = null;
//				mMesh = null;
//				if (mMat != null) CreatePanel();
			}
		}
	}
	
	public UIAtlas.Sprite sprite
	{
		get
		{
			if (!mSpriteSet) mSprite = null;

			if (mSprite == null && mAtlas != null)
			{
				if (!string.IsNullOrEmpty(mSpriteName))
				{
					sprite = mAtlas.GetSprite(mSpriteName);
				}

				if (mSprite == null && mAtlas.spriteList.Count > 0)
				{
					sprite = mAtlas.spriteList[0];
					mSpriteName = mSprite.name;
				}

				// If the sprite has been set, update the material
				if (mSprite != null) material = mAtlas.spriteMaterial;
			}
			return mSprite;
		}
		set
		{
			mSprite = value;
			mSpriteSet = true;
			material = (mSprite != null && mAtlas != null) ? mAtlas.spriteMaterial : null;
		}
	}
	
	Mesh mesh {
		get
		{
			if(mMesh == null)
			{
				mMesh = GetComponent<MeshFilter>().mesh;
			}
			return mMesh;
		}
		set
		{
			mMesh = value;
		}
	}
	
	public Rect outerUV { get { UpdateUVs(false); return mOuterUV; } }
	
	public void UpdateUVs (bool force)
	{
		if (sprite != null && (force || mOuter != mSprite.outer))
		{
			Texture tex = mainTexture;

			if (tex != null)
			{
				mOuter = mSprite.outer;
				mOuterUV = mOuter;

				if (mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
				{
					mOuterUV = NGUIMath.ConvertToTexCoords(mOuterUV, tex.width, tex.height);
				}
//				Debug.Log("mOuterUV:" + mOuterUV);
//				AssignMaterials();
				mChanged = true;
			}
		}
	}
	
	void AssignMaterials()
	{
		// theObject.renderer.material.mainTexture = textureAtlas;
		renderer.material = material;
//		theObject.renderer.material = textureAtlasMaterial;
//		Rect theOffsets = GetTexInfo( theMaterial );
		
		// Assign material UVs to mesh UVs
		Vector2[] theUVs = new Vector2[mesh.uv.Length];
//		theUVs = mesh.uv;
//		foreach(Vector2 uv in mesh.uv)
//		{
//			Debug.Log("uv:" + uv);
//		}
//		foreach(Vector2 uv in mesh.uv1)
//		{
//			Debug.Log("uv1:" + uv);
//		}
//		foreach(Vector2 uv in mesh.uv2)
//		{
//			Debug.Log("uv2:" + uv);
//		}
		int index = 0;
		mOuterUV = sprite.outer;
		mOuterUV = NGUIMath.ConvertToTexCoords(mOuterUV, mainTexture.width, mainTexture.height);
//		Debug.Log(mOuterUV);
		foreach(Vector2 uv in mesh.uv)
		{
//			Debug.Log(uv);
			theUVs[index] = new Vector2(mOuterUV.width * uv.x + mOuterUV.x, mOuterUV.height * uv.y + mOuterUV.y);
//			uv = new Vector2((theOffsets.z - theOffsets.x) * uv.x + theOffsets.x, theOffsets.w * uv.y - theOffsets.y - theOffsets.w);
//			Debug.Log("after:" + theUVs[index]);
			index++;
		}
//		Debug.Log(theUVs[theUVs.Length - 1].y.ToString());
//	theUVs[theUVs.Length - 1] = new Vector2(0.25f, 0.0f);
//		theUVs[theUVs.Length - 2] = new Vector2(0.25f, 0.3f);
//		theUVs[theUVs.Length - 3] = new Vector2(0.0f, 0.0f);
//		theUVs[theUVs.Length - 4] = new Vector2(0.0f, 0.25f);
//		theUVs[theUVs.Length - 5] = new Vector2(0.3f, 0.0f);
//		theUVs[theUVs.Length - 6] = new Vector2(0.3f, 0.0f);
//		theUVs[theUVs.Length - 7] = new Vector2(0.3f, 0.0f);
		// Assign mesh UVs
		mesh.uv = theUVs;
		
//		Vector2[] theUVs1 = new Vector2[mesh.uv1.Length];
//		index = 0;
//		foreach(Vector2 uv in mesh.uv1)
//		{
////			Debug.Log(uv);
//			theUVs1[index] = new Vector2(mOuterUV.width * uv.x + mOuterUV.x, mOuterUV.height * uv.y + mOuterUV.y);
////			uv = new Vector2((theOffsets.z - theOffsets.x) * uv.x + theOffsets.x, theOffsets.w * uv.y - theOffsets.y - theOffsets.w);
////			Debug.Log("after:" + theUVs[index]);
//			index++;
//		}
//		
//		mesh.uv1 = theUVs1;
//		
//		Vector2[] theUVs2 = new Vector2[mesh.uv2.Length];
//		index = 0;
//		foreach(Vector2 uv in mesh.uv2)
//		{
////			Debug.Log(uv);
//			theUVs2[index] = new Vector2(mOuterUV.width * uv.x + mOuterUV.x, mOuterUV.height * uv.y + mOuterUV.y);
////			uv = new Vector2((theOffsets.z - theOffsets.x) * uv.x + theOffsets.x, theOffsets.w * uv.y - theOffsets.y - theOffsets.w);
////			Debug.Log("after:" + theUVs[index]);
//			index++;
//		}
//		
//		mesh.uv2 = theUVs2;
//		foreach(Vector2 uv in mesh.uv)
//		{
//			Debug.Log("after uv:" + uv);
//		}
//		foreach(Vector2 uv in mesh.uv1)
//		{
//			Debug.Log("after uv1:" + uv);
//		}
//		foreach(Vector2 uv in mesh.uv2)
//		{
//			Debug.Log("after uv2:" + uv);
//		}
//		GetComponent<MeshFilter>().mesh = mesh;
//		foreach(Vector2 uv in mesh.uv)
//		{
//			Debug.Log(uv);
//		}
	}
	
	void Start () {
//		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
//        Vector3[] vertices = mesh.vertices;
//        Vector2[] uvs = new Vector2[vertices.Length];
//        int i = 0;
//        while (i < uvs.Length) {
//            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
//            i++;
//        }
//        mesh.uv = uvs;
		if (!Application.isPlaying) return;
		mMesh = null;
		AssignMaterials();
	}
}
