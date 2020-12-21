using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private Level[] _levels;
    [SerializeField] private Transform _levelsParent;
    [SerializeField] private Level _levelTemplate;

    private void OnEnable()
    {
        for (int levelIndex = 0; levelIndex < _levels.Length; levelIndex++)
        {
            _levels[levelIndex].Init(levelIndex, DataBase.Instance.PlayerLevels[levelIndex]);
        }
    }
}
