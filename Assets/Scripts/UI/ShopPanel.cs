using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Transform _shopContainer;
    [SerializeField] private CellShop _cellShopCard;

    [Header("Card Full View")]
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _cardName;
    [SerializeField] private TMP_Text _cardAttack;
    [SerializeField] private TMP_Text _cardHealth;
    [SerializeField] private TMP_Text _cardSpeed;
    [SerializeField] private TMP_Text _cardMana;

    private CellShop[] _cellsShop;
    private int _cellCountShopCards;

    private void OnEnable()
    {
        if (_cellsShop == null || _cellsShop.Length <= 0) InitShopCards();
        OnCellShopCardUpdate();
        OnCellShopSelected(DataBase.Instance.ShopCards[0]);
    }

    private void InitShopCards()
    {
        _cellCountShopCards = DataBase.Instance.ShopCards.Count;
        _cellsShop = new CellShop[_cellCountShopCards];

        for (int indexCard = 0; indexCard < _cellCountShopCards; indexCard++)
        {
            _cellsShop[indexCard] = Instantiate(_cellShopCard, _shopContainer);
        }

        _cellShopCard.gameObject.SetActive(false);
    }

    private void OnCellShopCardUpdate()
    {
        foreach (var cell in _cellsShop)
        {
            cell.Init(null);
            cell.gameObject.SetActive(false);
        }

        for (int indexCard = 0; indexCard < _cellCountShopCards; indexCard++)
        {
            if (indexCard < _cellsShop.Length)
            {
                _cellsShop[indexCard].Init(DataBase.Instance.ShopCards[indexCard]);
                _cellsShop[indexCard].CellBuySelected += OnCellShopBuy;
                _cellsShop[indexCard].CellFullViewSelected += OnCellShopSelected;
                _cellsShop[indexCard].CellDisabled += OnCellShopDisabled;
                _cellsShop[indexCard].gameObject.SetActive(true);
            }
        }
    }

    private void OnCellShopSelected(Card card)
    {
        _image.sprite = card.CardSprite;
        _cardName.text = card.CardName;
        _cardAttack.text = card.Attack.ToString();
        _cardHealth.text = card.Health.ToString();
        _cardSpeed.text = card.Speed.ToString();
        _cardMana.text = card.Mana.ToString();
    }

    private void OnCellShopBuy(Card card, CellShop cellShop)
    {
        TrySellCard(card, cellShop);
    }

    private void TrySellCard(Card card, CellShop cellShop)
    {
        if (card.Price <= DataBase.Instance.Money)
        {
            DataBase.Instance.BuyCard(card);
            cellShop.CellBuySelected -= OnCellShopBuy;
            _cellCountShopCards--;
            OnCellShopCardUpdate();
            OnCellShopSelected(DataBase.Instance.ShopCards[0]);
        }
    }

    private void OnCellShopDisabled(CellShop cellShop)
    {
        cellShop.CellFullViewSelected -= OnCellShopSelected;
        cellShop.CellDisabled -= OnCellShopDisabled;
    }
}
