using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<Character> _pool = new List<Character>();

    protected void Initialize(List<Card> cards, bool isPlayerCharacter)
    {
        foreach (var card in cards)
        {
            for (int i = 0; i < _capacity; i++)
            {
                Character character = Instantiate(card.Prefab, _container.transform).GetComponent<Character>();
                character.Init(card, isPlayerCharacter);
                character.gameObject.SetActive(false);

                _pool.Add(character);
            }
        }
    }

    protected bool TryGameObject(string cardName, bool isPlayerCharacter, out Character result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false && p.CardName == cardName && p.ISPlayerCharacter == isPlayerCharacter);
        return result != null;
    }

    protected Character ExpandPool(Card card, bool isPlayerCharacter, out Character character)
    {
        character = Instantiate(card.Prefab, _container.transform).GetComponent<Character>();
        character.Init(card, isPlayerCharacter);
        character.gameObject.SetActive(false);

        _pool.Add(character);

        return character;
    }
}
