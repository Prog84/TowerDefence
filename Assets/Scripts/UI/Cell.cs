using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _emptyIcon;
    [SerializeField] private TMP_Text _label;

    private Card _card;

    public event UnityAction CellPartySelected;
    public event UnityAction<Cell> CellDisabled;

    private void Awake()
    {
        _icon.sprite = null;
    }
    public void Init(Card card)
    {
        _card = card;
        if (card == null)
        {
            _icon.sprite = _emptyIcon;
            _label.text = "";
        }
        else
        {
            _icon.sprite = card.Icon;
            _label.text = card.CardName;
        }
    }

    public void OnClickCellParty()
    {
        if (_card == null)
            return;

        DataBase.Instance.ActivePartyCards.Remove(_card);
        DataBase.Instance.SaveGameCard();
        CellPartySelected?.Invoke();
    }

    private void OnDisable()
    {
        CellDisabled?.Invoke(this);
    }
}
