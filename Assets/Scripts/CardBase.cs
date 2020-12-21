using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Database", menuName = "Databases/Cards")]
public class CardBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Card> _cards;

    [SerializeField] private Card _currentCard;

    private int _currentIndex;

    public void CreateCard()
    {
        if (_cards == null)
            _cards = new List<Card>();
        Card card = new Card();
        _cards.Add(card);
        _currentCard = card;
        _currentIndex = _cards.Count - 1;
    }

    public void RemoveCard()
    {
        if (_cards == null)
            return;

        if (_currentCard == null)
            return;

        _cards.Remove(_currentCard);

        if (_cards.Count > 0)
        {
            _currentCard = _cards[0];
        }
        else
        {
            CreateCard();
        }
        _currentIndex = 0;
    }

    public void NextCard()
    {
        if (_currentIndex + 1 < _cards.Count)
        {
            _currentIndex++;
            _currentCard = _cards[_currentIndex];
        }
    }

    public void PrevCard()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            _currentCard = _cards[_currentIndex];
        }
    }

    public Card GetCardOfID(int id)
    {
        return _cards.Find(t => t.ID == id);
    }

    public int GetCardsCount()
    {
        return _cards.Count;
    }
}

[System.Serializable]
public class Card
{
    [SerializeField] private int _id;
    public int ID => _id;
    
    [SerializeField] private string _cardName;
    public string CardName => _cardName;
   
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField] private Sprite _cardSprite;
    public Sprite CardSprite => _cardSprite;

    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;

    [SerializeField] private int _attack;
    public int Attack => _attack;

    [SerializeField] private int _health;
    public int Health => _health;

    [SerializeField] private int _speed;
    public int Speed => _speed;

    [SerializeField] private int _mana;
    public int Mana => _mana;

    [SerializeField] private int _price;
    public int Price => _price;
}
