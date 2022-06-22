using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "SO/LevelSO")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private Level _levelPrefab;

    public Level LevelPrefab => _levelPrefab;
}
