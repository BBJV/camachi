using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit TextureAtlas.
/// </summary>

[CustomEditor(typeof(TextureAtlas))]
public class TextureAtlasInspector : Editor {
	protected TextureAtlas mWidget;
	static protected bool mUseShader = false;

	bool mInitialized = false;
	protected bool mAllowPreview = true;

	/// <summary>
	/// Register an Undo command with the Unity editor.
	/// </summary>

	void RegisterUndo()
	{
		NGUIEditorTools.RegisterUndo("Widget Change", mWidget);
	}

	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		mWidget = target as TextureAtlas;

		if (!mInitialized)
		{
			mInitialized = true;
//			OnInit();
		}

		NGUIEditorTools.DrawSeparator();

		// Check to see if we can draw the widget's default properties to begin with
		if (OnDrawProperties())
		{
			// Draw all common properties next
			DrawCommonProperties();
		}
	}
	
	protected TextureAtlas mSprite;

	/// <summary>
	/// Atlas selection callback.
	/// </summary>

	void OnSelectAtlas (MonoBehaviour obj)
	{
		if (mSprite != null)
		{
			NGUIEditorTools.RegisterUndo("Atlas Selection", mSprite);
			bool resize = (mSprite.atlas == null);
			mSprite.atlas = obj as UIAtlas;
//			if (resize) mSprite.MakePixelPerfect();
			EditorUtility.SetDirty(mSprite.gameObject);
		}
	}

	/// <summary>
	/// Convenience function that displays a list of sprites and returns the selected value.
	/// </summary>

	static public string SpriteField (UIAtlas atlas, string field, string name, params GUILayoutOption[] options)
	{
		List<string> sprites = atlas.GetListOfSprites();
		return (sprites != null && sprites.Count > 0) ? NGUIEditorTools.DrawList(field, sprites.ToArray(), name, options) : null;
	}

	/// <summary>
	/// Convenience function that displays a list of sprites and returns the selected value.
	/// </summary>

	static public string SpriteField (UIAtlas atlas, string name, params GUILayoutOption[] options)
	{
		return SpriteField(atlas, "Sprite", name, options);
	}

	/// <summary>
	/// Draw the atlas and sprite selection fields.
	/// </summary>

	protected bool OnDrawProperties ()
	{
		mSprite = mWidget;
//		Debug.Log(mSprite);
		ComponentSelector.Draw<UIAtlas>(mSprite.atlas, OnSelectAtlas);
		if (mSprite.atlas == null) return false;

		string spriteName = SpriteField(mSprite.atlas, mSprite.spriteName);

		if (mSprite.spriteName != spriteName)
		{
			NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
			mSprite.spriteName = spriteName;
//			mSprite.MakePixelPerfect();
			EditorUtility.SetDirty(mSprite.gameObject);
		}
		return true;
	}

	/// <summary>
	/// Draw the sprite texture.
	/// </summary>

	protected void OnDrawTexture ()
	{
		Texture2D tex = mSprite.mainTexture as Texture2D;

		if (tex != null)
		{
			// Draw the atlas
			EditorGUILayout.Separator();
			Rect rect = NGUIEditorTools.DrawSprite(tex, mSprite.outerUV, mUseShader ? mSprite.atlas.spriteMaterial : null);

			// Draw the selection
			NGUIEditorTools.DrawOutline(rect, mSprite.outerUV, new Color(0.4f, 1f, 0f, 1f));

			// Sprite size label
			string text = "Sprite Size: ";
			text += Mathf.RoundToInt(Mathf.Abs(mSprite.outerUV.width * tex.width));
			text += "x";
			text += Mathf.RoundToInt(Mathf.Abs(mSprite.outerUV.height * tex.height));

			rect = GUILayoutUtility.GetRect(Screen.width, 18f);
			EditorGUI.DropShadowLabel(rect, text);
		}
	}
	
	protected void DrawCommonProperties ()
	{
#if UNITY_3_4
		PrefabType type = EditorUtility.GetPrefabType(mWidget.gameObject);
#else
		PrefabType type = PrefabUtility.GetPrefabType(mWidget.gameObject);
#endif

		NGUIEditorTools.DrawSeparator();

		// Depth navigation
//		if (type != PrefabType.Prefab)
//		{
//			GUILayout.BeginHorizontal();
//			{
//				EditorGUILayout.PrefixLabel("Depth");
//
//				int depth = mWidget.depth;
//				if (GUILayout.Button("Back")) --depth;
//				depth = EditorGUILayout.IntField(depth, GUILayout.Width(40f));
//				if (GUILayout.Button("Forward")) ++depth;
//
//				if (mWidget.depth != depth)
//				{
//					NGUIEditorTools.RegisterUndo("Depth Change", mWidget);
//					mWidget.depth = depth;
//				}
//			}
//			GUILayout.EndHorizontal();
//		}
//
//		Color color = EditorGUILayout.ColorField("Color Tint", mWidget.color);
//
//		if (mWidget.color != color)
//		{
//			NGUIEditorTools.RegisterUndo("Color Change", mWidget);
//			mWidget.color = color;
//		}

		// Depth navigation
//		if (type != PrefabType.Prefab)
//		{
//			GUILayout.BeginHorizontal();
//			{
//				EditorGUILayout.PrefixLabel("Correction");
//
//				if (GUILayout.Button("Make Pixel-Perfect"))
//				{
//					NGUIEditorTools.RegisterUndo("Make Pixel-Perfect", mWidget.transform);
//					mWidget.MakePixelPerfect();
//				}
//			}
//			GUILayout.EndHorizontal();
//		}
//
//		UIWidget.Pivot pivot = (UIWidget.Pivot)EditorGUILayout.EnumPopup("Pivot", mWidget.pivot);
//
//		if (mWidget.pivot != pivot)
//		{
//			NGUIEditorTools.RegisterUndo("Pivot Change", mWidget);
//			mWidget.pivot = pivot;
//		}
//
		if (mAllowPreview && mWidget.mainTexture != null)
		{
			GUILayout.BeginHorizontal();
			{
				UISettings.texturePreview = EditorGUILayout.Toggle("Preview", UISettings.texturePreview, GUILayout.Width(100f));

				/*if (UISettings.texturePreview)
				{
					if (mUseShader != EditorGUILayout.Toggle("Use Shader", mUseShader))
					{
						mUseShader = !mUseShader;

						if (mUseShader)
						{
							// TODO: Remove this when Unity fixes the bug with DrawPreviewTexture not being affected by BeginGroup
							Debug.LogWarning("There is a bug in Unity that prevents the texture from getting clipped properly.\n" +
								"Until it's fixed by Unity, your texture may spill onto the rest of the Unity's GUI while using this mode.");
						}
					}
				}*/
			}
			GUILayout.EndHorizontal();

			// Draw the texture last
			if (UISettings.texturePreview) OnDrawTexture();
		}
	}
}
