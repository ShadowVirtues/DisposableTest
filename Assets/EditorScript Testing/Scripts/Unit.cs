using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WoG.BattleSystem;
using WoG.Castle;

public class Unit : MonoBehaviour
{
    //Unit has UnitInfo ScrObject field, ArmatureName string, TimeToAutoAttack float, and then Casts array-5
    //Each cast along with mechanic info has animation info

    public UnitInfo UnitInfo;
    public string ArmatureName;
    public float TimeToAttack;

    public Cast[] Casts;






}
