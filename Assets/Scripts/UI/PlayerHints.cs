using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Player hints", menuName = "Player hints")]
public class PlayerHints : ScriptableObject
{
    [SerializeField] private List<LevelHint> levelHints;

    public List<LevelHint> LevelHints { get => levelHints; }
}

[Serializable]
public class LevelHint
{
    [SerializeField] private GameObject hint;
    [SerializeField] private int minLevel;

    public GameObject Hint { get => hint; }
    public int MinLevel { get => minLevel; }
}
