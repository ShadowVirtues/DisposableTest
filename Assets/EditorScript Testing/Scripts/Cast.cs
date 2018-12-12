using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

namespace WoG.BattleSystem
{
    public enum ActiveMechanicType
    {
        Damage,
        PercentDamage,  //TouchOfDeath
        DamageFromAttack,
        StatDebuff,
        StatBuff,
        Stun,
        Sleep,
        Frost,
        Silence,
        Heal,
        PercentHeal,
        Revive,
        BuffRemoval,
        DebuffRemoval,
        HealBan,
        MagicBan,
        Shield,
        Cooldown,       //Rollback
        PassiveBuff,    //PBuff
        
        

    }

    public enum PassiveMechanicType
    {
        Revive, Crit, LifeSteal, AddElemDamage, DoTSelf, Heal, Reflect, CounterAttack, DamageReduction, //????
        Stun, Sleep, Freeze, Silence, Splash, Pierce
    }

    public enum MechanicParamType
    {
        DamageValue,
        DefensePen,
        BuffValue,
        Rounds,
        PercentDamage,
        DamageModifier,
        HealValue,
        HealPercent,
        BuffNumber,
        ShieldValue,
        Chance
    }

    public enum MechanicType { Active, Passive }
   

    public enum TargetSelection
    {
        SoloE,
        SoloA,
        VLineE,
        HLineE,
        VLineA,
        HLineA,
        Self,
        AllE,
        AllA,
        MeleeA,
        MeleeE,
        CasterA,
        CasterE,
        WarriorA,
        WarriorE,
        AssasinA,
        AssasinE,
        TankA,
        TankE,
        MageA,
        MageE,
        SupportA,
        SupportE,
        ArcherA,
        ArcherE,
        _2X2E,
        _4X4E,
        _2X2A,
        _4X4A,
        FLineA,
        SLineA,
        TLineA,
        FLineE,
        SLineE,
        TLineE,
        //First line solo
        SFLineE,
    }

    public enum Element { Physical, Water, Fire, Wind, Earth, Light, Dark, True }
    public enum UnitStat { Health, Attack, Initiative, Dodge, PhysDef, WaterDef, FireDef, WindDef, EarthDef, LightDef, DarkDef }
    public enum PassiveProcCondition { OnAttack, OnGetDamage, OnRoundEnd, OnEvade, OnDeath  }
    

    [Serializable]
    public class Cast
    {
        
        public string MechanicName;

        public MechanicType Type;

        public List<ActiveMechanic> ActiveMechanics;
        public List<PassiveMechanic> PassiveMechanics;  //Can be multiple.

        public MechanicParameter Param; //DELETE
    }

    //MechanicBase: Element,Parameters,Selection
    //Move BuffedStat to Passive
    //Add ProcCondition None


    [Serializable]
    public class ActiveMechanic
    {
        public ActiveMechanicType ActiveType;
        public TargetSelection Selection;   //Can be multiple target selections for multiple active mechanics. All except main one have to be 'Self'

        public MechanicParameter[] Parameters;

        public Element Element;
        public UnitStat BuffedStat;
        public PassiveMechanic AppliedPassive;
    }

    [Serializable]
    public class PassiveMechanic
    {
        public PassiveMechanicType PassiveType;
        public PassiveProcCondition ProcCondition;

        public MechanicParameter[] Parameters;
        public Element Element;
    }


    
    [Serializable]
    public class MechanicParameter
    {
        public MechanicParamType Parameter;
        public bool IsUpgradable;
        public int UpgradeIndex;    //Works for MagicBook too
        public int NonUpgradableValue;  
    }



}