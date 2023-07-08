using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum AttackType
    {
        LASER,
        PELLET,
        ROCKET
    }

    public enum BombType
    {
        ROCKET,
        PLANE,
        PLANES
    }

    [Header("Type")]
    public AttackType BaseAttack;
    public AttackType SpecialAttack;
    public BombType BombAttack;

    [Header("Prefabs")]
    public GameObject BasePrefab;
    public GameObject SpecialPrefab;
    public GameObject BombPrefab;

    [Header("Damage")]
    public float BaseDamage;
    public float SpecialDamage;

    [Header("Cooldown")]
    public float BaseCooldown = 0.2f;
}
