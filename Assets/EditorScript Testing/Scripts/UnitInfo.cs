using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace WoG.Castle
{
    public enum AttackElement { Physical, Dark, Light, Earth, Wind, Fire, Water }

    public enum ValueKind { Point, Percent }

    [CreateAssetMenu(fileName = "[ItemName]", menuName = "Unit/UnitInfo")]
    public class UnitInfo : ScriptableObject
    {
        public bool Implemented; //DELETE TESTFUNC
        public string Name; //Purely to see which Unit this is
        public string UnitID;


        [Header("Sprites")]
        public Sprite BigSprite;
        public Sprite SmallSprite;

        [Header("Stats")]
        public int Health;
        public int Attack;
        public int Evasion;
        public int Speed;
        public int PhysDef;
        public int LightDef;
        public int DarkDef;
        public int EarthDef;
        public int AirDef;
        public int FireDef;
        public int WaterDef;

        [Header("Other")]
        public AttackElement AttackElement;

        [Header("Casts")]
        public UnitCast[] Casts;
    }

    [Serializable]
    public class UnitCast
    {
        public string Name;
        public Sprite Icon;
        public MechanicInfoUnitCast[] Mechanics;
    }

    [Serializable]
    public class MechanicInfoUnitCast
    {
        public Sprite Icon;
        public string DescriptionKey;
        public ValueKind ValueKind;
        public int[] Values;
    }


}

