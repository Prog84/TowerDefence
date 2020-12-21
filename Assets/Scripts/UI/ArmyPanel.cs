using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmyPanel : MonoBehaviour
{
    [SerializeField] private Transform _armyContainer;
    [SerializeField] private Transform _partyContainer;
    [SerializeField] private int _cellCountParty;
    [SerializeField] private Cell _cellParty;
    [SerializeField] private CellArmy _cellAllCard;

    [Header("Card Full View")]
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _cardName;
    [SerializeField] private TMP_Text _cardAttack;
    [SerializeField] private TMP_Text _cardHealth;
    [SerializeField] private TMP_Text _cardSpeed;
    [SerializeField] private TMP_Text _cardMana;
    [SerializeField] private GameObject _cardFullView;
    
    private Cell[] _cellsParties;
    private CellArmy[] _cellsAllCards;
    private int _cellCountAllCards;
    private int _cellCountPlayerCards;

    private void InitAllCards()
    {
        _cellCountAllCards = DataBase.Instance.CardBase.GetCardsCount();
        _cellCountPlayerCards = DataBase.Instance.PlayerCards.Count;
        _cellsAllCards = new CellArmy[_cellCountAllCards];

        for (int indexCard = 0; indexCard < _cellCountAllCards; indexCard++)
        {
            _cellsAllCards[indexCard] = Instantiate(_cellAllCard, _armyContainer);
        }

        _cellAllCard.gameObject.SetActive(false);
    }
    private void InitParty()
    {
        _cellsParties = new Cell[_cellCountParty];

        for (int cardIndex = 0; cardIndex < _cellCountParty; cardIndex++)
        {
            _cellsParties[cardIndex] = Instantiate(_cellParty, _partyContainer);
        }

        _cellParty.gameObject.SetActive(false);
    }

    private void OnCellSelected(Card card)
    {
        _image.sprite = card.CardSprite;
        _cardName.text = card.CardName;
        _cardAttack.text = card.Attack.ToString();
        _cardHealth.text = card.Health.ToString();
        _cardSpeed.text = card.Speed.ToString();
        _cardMana.text = card.Mana.ToString();
        _cardFullView.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_cellsAllCards == null || _cellsAllCards.Length <= 0) InitAllCards();
        OnCellAllCardUpdate();

        if (_cellsParties == null || _cellsParties.Length <= 0) InitParty();
        OnCellPartyUpdate();
    }

    private void OnCellAllCardUpdate()
    {
        _cellCountPlayerCards = DataBase.Instance.PlayerCards.Count;

        foreach (var cell in _cellsAllCards)
        {
            cell.Init(null);
            cell.gameObject.SetActive(false);
        }

        for (int indexCard = 0; indexCard < _cellCountAllCards; indexCard++)
        {
            if (indexCard < _cellCountPlayerCards)
            {
                _cellsAllCards[indexCard].Init(DataBase.Instance.PlayerCards[indexCard]);
                _cellsAllCards[indexCard].CellAllCardSelected += OnCellPartyUpdate;
                _cellsAllCards[indexCard].CellAllCardSelected += OnCellAllCardUpdate;
                _cellsAllCards[indexCard].CellFullViewSelected += OnCellSelected;
                _cellsAllCards[indexCard].CellDisabled += OnCellArmyDisabled;
                _cellsAllCards[indexCard].gameObject.SetActive(true);

            }
        }

    }

    private void OnCellPartyUpdate()
    {
        foreach (var cell in _cellsParties)
        {
            cell.Init(null);
        }

        for (int indexCard = 0; indexCard < DataBase.Instance.ActivePartyCards.Count; indexCard++)
        {
            if (indexCard < _cellsParties.Length)
            {
                _cellsParties[indexCard].Init(DataBase.Instance.ActivePartyCards[indexCard]);
                _cellsParties[indexCard].CellPartySelected += OnCellPartyUpdate;
                _cellsParties[indexCard].CellPartySelected += OnCellAllCardUpdate;
                _cellsParties[indexCard].CellDisabled += OnCellDisabled;
            }
        }

    }

    private void OnCellDisabled(Cell cell)
    {
        cell.CellPartySelected -= OnCellPartyUpdate;
        cell.CellPartySelected -= OnCellAllCardUpdate;
        cell.CellDisabled -= OnCellDisabled;
    }

    private void OnCellArmyDisabled(CellArmy cellArmy)
    {
        cellArmy.CellFullViewSelected -= OnCellSelected;
        cellArmy.CellAllCardSelected -= OnCellPartyUpdate;
        cellArmy.CellAllCardSelected -= OnCellAllCardUpdate;
        cellArmy.CellDisabled -= OnCellArmyDisabled;
    }
}