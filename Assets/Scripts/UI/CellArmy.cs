using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class CellArmy : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;

    private Card _card;

    public event UnityAction CellAllCardSelected;
    public event UnityAction<Card> CellFullViewSelected;
    public event UnityAction<CellArmy> CellDisabled;

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
        if (DataBase.Instance.ActivePartyCards.Count == DataBase.Instance.PlayerPartyCount)
        {
            _button.interactable = false;
        }
        else
        {
            var cardAvaibleParty = DataBase.Instance.ActivePartyCards.Where(c => c.ID == card.ID).FirstOrDefault();
            if(cardAvaibleParty != null)
            {
                _button.interactable = false;
            }
            else
            {
                _button.interactable = true;
            }
        }
    }

    public void OnClickCellAllCard()
    {
        if (_card == null)
            return;
        if (DataBase.Instance.ActivePartyCards.Count < 5)
        {
            DataBase.Instance.ActivePartyCards.Add(_card);
            DataBase.Instance.SaveGameCard();
            CellAllCardSelected?.Invoke();
        }
    }

    public void OnClickCellFullView()
    {
        if (_card == null)
            return;

        CellFullViewSelected?.Invoke(_card);
    }

    private void OnDisable()
    {
        CellDisabled?.Invoke(this);
    }
}
