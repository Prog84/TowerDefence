using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerPartyCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _priceMana;

    private Card _card;

    public event UnityAction<Card> CellSelected;
    public event UnityAction<PlayerPartyCell> CellDisabled;

    public void Init(Card card)
    {
        if (card == null)
            return;

        _card = card;
        _icon.sprite = card.Icon;
        _priceMana.text = card.Mana.ToString();
    }

    public void OnClickCell()
    {
        if (_card == null)
            return;

        CellSelected?.Invoke(_card);
    }

    private void OnDisable()
    {
        CellDisabled?.Invoke(this);
    }
}
