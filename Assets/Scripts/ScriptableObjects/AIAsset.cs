using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class AIAsset : ScriptableObject
{
    public GameObject Prefab;
    public int Cost;
    public Sprite Icon;
    public string Descriptor;
}
