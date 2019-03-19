using AAI.VDTSimulator.Weather;
using UnityEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(WeatherParticle))]
public class WeatherParticleEditor : Editor
{

	private WeatherParticle _weatherParticle;
	private SerializedProperty _minMaxStartSize;
	private void OnEnable()
	{
		_weatherParticle = (WeatherParticle) target;
		_minMaxStartSize = serializedObject.FindProperty("_minMaxStartSize_Min");


	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		if (GUILayout.Button("Generate Data"))
		{
			_weatherParticle.PopulateData();
		}
		EditorGUILayout.LabelField("Start Size (Min ~ Max)");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Vector2Field("Min Values: ", _minMaxStartSize.vector2Value);
		EditorGUILayout.EndHorizontal();
		DrawDefaultInspector();
		serializedObject.ApplyModifiedProperties();
	}
}
