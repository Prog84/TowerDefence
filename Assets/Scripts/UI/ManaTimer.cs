using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private float _speedMana = 5;

    private float _mana;

    public float Mana => _mana;
    
    private void Start()
    {
        _timer.text = _mana.ToString();
    }

    private void Update()
    {
        _mana += Time.deltaTime * _speedMana;
        _timer.text = Math.Truncate(_mana).ToString();
    }

    public void ReducePlayerMana(int countMana)
    {
        _mana -= countMana;
    }
}
