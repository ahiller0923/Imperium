using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int level;
    public float health;

    public float dex;
    public float dexterity
    {
        get { return dex + (Mathf.Abs(resolve) * .1f); }
    }

    public float intel;
    public float intelligence
    {
        get { return intel + (Mathf.Abs(resolve) * .1f); }
    }

    public float moveSpeed;

    public float attackSpeed;

    public float projectileSpeed;

    public float arm;
    public float armor
    {
        get { return arm + (Mathf.Abs(resolve) * .01f); }
    }

    public float mr;
    public float magicResist
    {
        get { return mr + (Mathf.Abs(resolve) * .01f); }
    }

    public float resolve;



    // Methods
    public float CalculatePhysicalDamageDealt(float baseDamage)
    {
        return (1 + (dexterity * .01f)) * baseDamage;
    }

    public float CalculateMagicDamageDealt(float baseDamage)
    {
        return (1 + (intelligence * .01f)) * baseDamage;
    }

    public float CalculateDamageTaken(float physicalDamage, float magicDamage)
    {
        return physicalDamage * (1 - (armor * .01f)) + magicDamage * (1 - (magicResist * .01f));
    }
}
