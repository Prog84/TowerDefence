using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellShop : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _price;

    private Card _card;

    public event UnityAction<Card> CellFullViewSelected;
    public event UnityAction<Card, CellShop> CellBuySelected;
    public event UnityAction<CellShop> CellDisabled;

    private void Awake()
    {
        _icon.sprite = null;
    }
    public void Init(Card card)
    {
        if (card == null)
            return;
        _card = card;
        _icon.sprite = card.Icon;
        _label.text = card.CardName;
        _price.text = card.Price.ToString();

    }

    public void OnClickCellFullView()
    {
        if (_card == null)
            return;

        CellFullViewSelected?.Invoke(_card);
    }

    public void OnClickCellBuy()
    {
        if (_card == null)
            return;

        CellBuySelected?.Invoke(_card, this);
    }

    private void OnDisable()
    {
        CellDisabled?.Invoke(this);
    }
}
