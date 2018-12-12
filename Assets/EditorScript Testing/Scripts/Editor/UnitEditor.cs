using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WoG.BattleSystem;


[CustomPropertyDrawer(typeof(MechanicParameter))]
public class CastEditor : PropertyDrawer
{    
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        SerializedProperty parameter = prop.FindPropertyRelative("Parameter");
        SerializedProperty isUpgradable = prop.FindPropertyRelative("IsUpgradable");
        SerializedProperty upgradeIndex = prop.FindPropertyRelative("UpgradeIndex");
        SerializedProperty nonUpgradableValue = prop.FindPropertyRelative("NonUpgradableValue");

        float posY = pos.y;
        Rect fieldRect = new Rect(pos.x, posY, pos.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(fieldRect, parameter, new GUIContent(parameter.displayName));

        posY += EditorGUIUtility.singleLineHeight;
        Rect fieldRect1 = new Rect(pos.x, posY, pos.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(fieldRect1, isUpgradable, new GUIContent(isUpgradable.displayName));

        posY += EditorGUIUtility.singleLineHeight;
        Rect fieldRect2 = new Rect(pos.x, posY, pos.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(fieldRect2, upgradeIndex, new GUIContent(upgradeIndex.displayName));

        posY += EditorGUIUtility.singleLineHeight;
        Rect fieldRect3 = new Rect(pos.x, posY, pos.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(fieldRect3, nonUpgradableValue, new GUIContent(nonUpgradableValue.displayName));

        //EditorGUILayout.PropertyField(parameter, new GUIContent(parameter.displayName));
        //EditorGUILayout.PropertyField(isUpgradable, new GUIContent(isUpgradable.displayName));
        //EditorGUILayout.PropertyField(upgradeIndex, new GUIContent(upgradeIndex.displayName));
        //EditorGUILayout.PropertyField(nonUpgradableValue, new GUIContent(nonUpgradableValue.displayName));
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //return EditorGUI.GetPropertyHeight(property);
        return EditorGUIUtility.singleLineHeight * 4;
    }
    
}




[CustomEditor(typeof(Unit))]
[CanEditMultipleObjects]
public class UnitEditor : Editor
{
    SerializedProperty unitInfoProp;
    SerializedProperty armatureNameProp;
    SerializedProperty timeToAttackProp;
    SerializedProperty castsProp;

    private Unit unit;

    private bool[] foldouts = new bool[5];
    
    void OnEnable()
    {
        // Setup the SerializedProperties
        unitInfoProp = serializedObject.FindProperty("UnitInfo");
        armatureNameProp = serializedObject.FindProperty("ArmatureName");
        timeToAttackProp = serializedObject.FindProperty("TimeToAttack");


        castsProp = serializedObject.FindProperty("Casts");

        unit = (Unit)target;
        
    }

    

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();


        EditorGUILayout.PropertyField(unitInfoProp, new GUIContent(unitInfoProp.displayName));
        EditorGUILayout.PropertyField(armatureNameProp, new GUIContent(armatureNameProp.displayName));
        EditorGUILayout.PropertyField(timeToAttackProp, new GUIContent(timeToAttackProp.displayName));

        //EditorGUILayout.Space();

        castsProp.arraySize = 5;

        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout) {fontStyle = FontStyle.Bold};
        GUIStyle boldStyle = new GUIStyle {fontStyle = FontStyle.Bold};

        for (int i = 0; i < castsProp.arraySize; i++)
        {
            EditorGUILayout.Space();
            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], new GUIContent($"Cast{i}"), true, myFoldoutStyle);

            if (foldouts[i])
            {
                SerializedProperty cast = castsProp.GetArrayElementAtIndex(i);

                SerializedProperty castMechName = cast.FindPropertyRelative("MechanicName");
                EditorGUILayout.PropertyField(castMechName, new GUIContent(castMechName.displayName));

                SerializedProperty castMechType = cast.FindPropertyRelative("Type");
                EditorGUILayout.PropertyField(castMechType, new GUIContent(castMechType.displayName));

                SerializedProperty param = cast.FindPropertyRelative("Param");
                EditorGUILayout.PropertyField(param, new GUIContent(param.displayName));
                

                //SerializedProperty mechanics = cast.FindPropertyRelative("Mechanics");

                //if (unit.Casts[i].Type == MechanicType.Active)
                //{
                //    SerializedProperty selection = cast.FindPropertyRelative("Selection");
                //    EditorGUILayout.PropertyField(selection, new GUIContent(selection.displayName));



                //    ShowMechanics(mechanics, MechanicType.Active, i);


                //}
                //else
                //{
                //    ShowMechanics(mechanics, MechanicType.Passive, i);
                //}

            }
            
            
        }

        
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }


    //private void ShowMechanics(SerializedProperty mechanicsProp, MechanicType active, int index)
    //{
    //    EditorGUILayout.Space();
    //    EditorGUILayout.LabelField($"Mechanics: {mechanicsProp.arraySize}", EditorStyles.boldLabel);

    //    EditorGUI.indentLevel++;
    //    for (int i = 0; i < mechanicsProp.arraySize; i++)
    //    {
    //        GUILayout.BeginHorizontal();

    //        GUILayout.Label($"Mechanic {i}");
            
    //        if (GUILayout.Button("-", GUILayout.MaxWidth(30), GUILayout.ExpandWidth(false)))
    //        {
    //            unit.Casts[index].Mechanics.RemoveAt(i);
    //        }
    //        GUILayout.EndHorizontal();
            
    //        EditorGUI.indentLevel++;
    //        SerializedProperty oneMechanic = mechanicsProp.GetArrayElementAtIndex(i);

    //        if (active == MechanicType.Active)
    //        {
    //            SerializedProperty activeType = oneMechanic.FindPropertyRelative("ActiveType");
    //            EditorGUILayout.PropertyField(activeType, new GUIContent(activeType.displayName));
    //        }
    //        else
    //        {
    //            SerializedProperty passiveType = oneMechanic.FindPropertyRelative("PassiveType");
    //            EditorGUILayout.PropertyField(passiveType, new GUIContent(passiveType.displayName));
    //        }

    //        SerializedProperty parameters = mechanicsProp.FindPropertyRelative("Parameters");


    //        EditorGUI.indentLevel--;
    //    }


    //    if (GUILayout.Button("Add Mechanic"))
    //    {
    //        mechanicsProp.arraySize++;
    //    }



    //    //EditorGUILayout.PropertyField(mechanics, new GUIContent(mechanics.displayName));

    //    EditorGUI.indentLevel--;
    //}

    //private static Dictionary<ActiveMechanicType, MechanicParamType[]> ActiveMechanicParams = new Dictionary<ActiveMechanicType, MechanicParamType[]>()
    //{
    //    [ActiveMechanicType.Damage] = new MechanicParamType[] { MechanicParamType.BuffValue, MechanicParamType.Rounds },
    //};

    //private static Dictionary<PassiveMechanicType, MechanicParamType[]> PassiveMechanicParams = new Dictionary<PassiveMechanicType, MechanicParamType[]>()
    //{
    //    [PassiveMechanicType.Heal] = new MechanicParamType[] { MechanicParamType.Chance, MechanicParamType.HealValue, },
    //};

    //private void ShowMechanicParameters(SerializedProperty parameters, MechanicType active)
    //{
    //    parameters.arraySize
    //    for (int i = 0; i < parameters.arraySize; i++)
    //    {

    //        GUILayout.Label($"Mechanic {i}");


    //        SerializedProperty oneMechanic = mechanicsProp.GetArrayElementAtIndex(i);

    //        if (active == MechanicType.Active)
    //        {
    //            SerializedProperty activeType = oneMechanic.FindPropertyRelative("ActiveType");
    //            EditorGUILayout.PropertyField(activeType, new GUIContent(activeType.displayName));
    //        }
    //        else
    //        {
    //            SerializedProperty passiveType = oneMechanic.FindPropertyRelative("PassiveType");
    //            EditorGUILayout.PropertyField(passiveType, new GUIContent(passiveType.displayName));
    //        }



    //    }

    //}


}
