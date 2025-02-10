using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Transform t = (Transform)target;

        if (GUILayout.Button("Reset Transforms"))
        {
            Undo.RegisterCompleteObjectUndo(t, "Reset Transforms " + t.name);
            t.transform.position = Vector3.zero;
            t.transform.rotation = Quaternion.identity;
            t.transform.localScale = Vector3.one;
        }

        // Replicate the standard transform inspector gui
        EditorGUIUtility.labelWidth = 1;
        EditorGUIUtility.fieldWidth = 1;
        EditorGUI.indentLevel = 0;
        Vector3 position = EditorGUILayout.Vector3Field("Position", t.localPosition);
        Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", t.localEulerAngles);
        Vector3 scale = EditorGUILayout.Vector3Field("Scale", t.localScale);
        if (GUI.changed)
        {
            t.localPosition = FixIfNaN(position);
            t.localEulerAngles = FixIfNaN(eulerAngles);
            t.localScale = FixIfNaN(scale);
        }
    }
    private Vector3 FixIfNaN(Vector3 v)
    {
        if (float.IsNaN(v.x))
        {
            v.x = 0;
        }
        if (float.IsNaN(v.y))
        {
            v.y = 0;
        }
        if (float.IsNaN(v.z))
        {
            v.z = 0;
        }
        return v;
    }
}