using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSO", menuName = "SO/DataSO")]
public class DataSO : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private int _coin;
    [SerializeField] private int _stackMaxCount;
    [SerializeField] private int _stackStartCount;
    [SerializeField] private int _stackUpgradeValue;

    public int Level => _level;

    public int Coin => _coin;

    public int StackMaxCount => _stackMaxCount;

    public int StackStartCount => _stackStartCount;

    public int StackUpgradeValue => _stackUpgradeValue;
}
