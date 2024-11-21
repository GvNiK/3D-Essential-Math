using UnityEditor;

[CustomEditor(typeof(CircularMovement))]
public class CircularMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CircularMovement circularMovement = (CircularMovement)target; 
        
    }
}