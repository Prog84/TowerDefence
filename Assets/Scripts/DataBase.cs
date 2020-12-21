using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataBase : MonoBehaviour
{
    #region Singleton Database
    public static DataBase Instance { get; private set; }
    #endregion

    [SerializeField] private CardBase _cardDataBase;

    public CardBase CardBase => _cardDataBase;

    private List<Card> _activePartyCards;
    public List<Card> ActivePartyCards => _activePartyCards;

    private List<Card> _playerCards;
    public List<Card> PlayerCards => _playerCards;

    private List<Card> _shopCards;
    public List<Card> ShopCards => _shopCards;

    private int[] _playerLevels = new int[] { 1, 0, 0, 0, 0, 0 };

    public int[] PlayerLevels => _playerLevels;

    private int _playerPartyCount = 5;

    public int PlayerPartyCount => _playerPartyCount;

    private int _enemyPartyCount = 5;
    public int EnemyPartyCount => _enemyPartyCount;

    private int _money = 0;
    public int Money { get; private set; }

    public event UnityAction<int> MoneyChanged;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        Instance = this;

        _activePartyCards = new List<Card>();
        _playerCards = new List<Card>();
        _shopCards = new List<Card>();

        if (PlayerPrefs.HasKey("SaveMoney"))
        {
            LoadGameSaveMoney();
        }
        if (PlayerPrefs.HasKey("SaveLevel0"))
        {
            LoadGameSaveLevel();
        }
        if (PlayerPrefs.HasKey("GameCard0"))
        {
            LoadGameCard();
        }
        else
        {
            InitGameCards();
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyCard(Card card)
    {
        Money -= card.Price;
        MoneyChanged?.Invoke(Money);

        _playerCards.Add(card);
        _shopCards.Remove(card);

        SaveGameCard();
    }

    private void InitGameCards()
    {
        for (int i = 0; i < _playerPartyCount; i++)
        {
            _activePartyCards.Add(_cardDataBase.GetCardOfID(i));
            _playerCards.Add(_cardDataBase.GetCardOfID(i));
        }

        for (int i = _playerPartyCount; i < _cardDataBase.GetCardsCount(); i++)
        {
            _shopCards.Add(_cardDataBase.GetCardOfID(i));
        }
    }

    public void SaveGameMoney()
    {
        PlayerPrefs.SetInt("SaveMoney", Money);
    }

    private void LoadGameSaveMoney()
    {
        Money = PlayerPrefs.GetInt("SaveMoney");
    }

    public void SaveGameLevel()
    {
        for (int indexLevel = 0; indexLevel < _playerLevels.Length; indexLevel++)
        {
            PlayerPrefs.SetInt("SaveLevel" + indexLevel, _playerLevels[indexLevel]);
        }
    }

    private void LoadGameSaveLevel()
    {
        for (int indexLevel = 0; indexLevel < _playerLevels.Length; indexLevel++)
        {
            _playerLevels[indexLevel] = PlayerPrefs.GetInt("SaveLevel" + indexLevel);
        }
    }

    private void LoadGameCard()
    {
        for (int indexCard = 0; indexCard < _cardDataBase.GetCardsCount(); indexCard++)
        {
            if (PlayerPrefs.HasKey("GameCard" + indexCard))
            {
                var cardAvaible = PlayerPrefs.GetInt("GameCard" + indexCard);
                if (cardAvaible == 0)
                {
                    _shopCards.Add(_cardDataBase.GetCardOfID(indexCard));
                }
                else if (cardAvaible == 1)
                {
                    _playerCards.Add(_cardDataBase.GetCardOfID(indexCard));
                }
                else if (cardAvaible == 2)
                {
                    _playerCards.Add(_cardDataBase.GetCardOfID(indexCard));
                    _activePartyCards.Add(_cardDataBase.GetCardOfID(indexCard));
                }
            }
        }
    }

    public void SaveGameCard()
    {
        for (int indexCard = 0; indexCard < _cardDataBase.GetCardsCount(); indexCard++)
        {
            CheckCard(_cardDataBase.GetCardOfID(indexCard));
        }
    }
    /// <summary>
    /// 0 - карта не куплена, 1 карта доступна игроку, 2 карта доступна и в боевом отряде
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    private void CheckCard(Card card)
    {
        var cardAvaiblePlayer = DataBase.Instance.PlayerCards.Where(c => c.ID == card.ID).FirstOrDefault();
        if (cardAvaiblePlayer != null)
        {
            PlayerPrefs.SetInt("GameCard" + card.ID, 1);
        }
        var cardAvaibleParty = DataBase.Instance.ActivePartyCards.Where(c => c.ID == card.ID).FirstOrDefault();
        if (cardAvaibleParty != null)
        {
            PlayerPrefs.SetInt("GameCard" + card.ID, 2);
        }
        var cardAvaibleShop = DataBase.Instance.ShopCards.Where(c => c.ID == card.ID).FirstOrDefault();
        if (cardAvaibleShop != null)
        {
            PlayerPrefs.SetInt("GameCard" + card.ID, 0);
        }
    }
}
