using Journal.Attributes;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(JournalVisibleAttribute))]
public class EnumVisibleDecoratorDrawer : PropertyDrawer
{
	/// <inheritdoc />
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (attribute is not JournalVisibleAttribute visibleAttribute) return;
		//
		// // Check if the property is null
		// if (findProperty == null)
		// {
		// 	// If it is, log an error
		// 	Debug.LogError($"Property {visibleAttribute.DataProperty} not found");
		// 	return;
		// }
		//
		// // Check if the property is an enum
		// if (findProperty.propertyType != SerializedPropertyType.Enum)
		// {
		// 	// If it is not, log an error
		// 	Debug.LogError($"Property {visibleAttribute.DataProperty} is not an enum");
		// 	return;
		// }
		//
		// Debug.Log($"Type: {property.type}");
		//
		// // Check if the enum value is equal to the value of the attribute
		// if (findProperty.enumValueIndex != (int)visibleAttribute.JournalItemType)
		// {
		// 	// If not, don't draw the property
		// 	return;
		// }
		//
		// Draw the property
		EditorGUI.PropertyField(position, property, label, true);
	}
}