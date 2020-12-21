using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : ObjectPool
{
    [SerializeField] private ManaTimer _timer;
    [SerializeField] private PlayerPartyCell[] _cells;
    [SerializeField] private Transform _spawnPoint;

    private readonly bool _isPlayerCharacter = true;

    private void OnEnable()
    {
        InitPlayerCards();
        Initialize(DataBase.Instance.ActivePartyCards, _isPlayerCharacter);
    }

    private void InitPlayerCards()
    {
        for (int indexCard = 0; indexCard < _cells.Length; indexCard++)
        {
            _cells[indexCard].Init(DataBase.Instance.ActivePartyCards[indexCard]);
            _cells[indexCard].CellSelected += OnSelectionButtonClick;
            _cells[indexCard].CellDisabled += OnCellDisabled;
        }
    }

    private void TryInstantiatePlayerCard(Card card)
    {
        if (card.Mana <= _timer.Mana)
        {
            if (TryGameObject(card.CardName, _isPlayerCharacter, out Character character))
            {
                character.ResetHealth();
                SetCharacter(character);
            }
            else
            {
                ExpandPool(card, _isPlayerCharacter, out Character newCharacter);
                SetCharacter(newCharacter);
            }
            _timer.ReducePlayerMana(card.Mana);
        }
    }

    private void OnSelectionButtonClick(Card card)
    {
        TryInstantiatePlayerCard(card);
    }

    private void OnCellDisabled(PlayerPartyCell cell)
    {
        cell.CellSelected -= OnSelectionButtonClick;
        cell.CellDisabled -= OnCellDisabled;
    }

    private void SetCharacter(Character character)
    {
        character.gameObject.SetActive(true);
        character.transform.position = _spawnPoint.position;
    }
}
