using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;

    private void Start()
    {
        _money.text = DataBase.Instance.Money.ToString();
        DataBase.Instance.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        DataBase.Instance.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _money.text = money.ToString();
    }
}
