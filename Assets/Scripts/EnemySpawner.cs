using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _currentMana = 0;
    [SerializeField] private float _speedMana = 5;
    [SerializeField] private float _delay = 5;

    private List<Card> _cards;
    private readonly bool _isPlayerCharacter = false;
    private bool _isDelay;
    
    private void Awake()
    {
        _cards = new List<Card>();

        for (int i = 0; i < DataBase.Instance.EnemyPartyCount; i++)
        {
            _cards.Add(DataBase.Instance.CardBase.GetCardOfID(Random.Range(0, DataBase.Instance.CardBase.GetCardsCount())));
        }
        Initialize(_cards, _isPlayerCharacter);
    }

    private void Update()
    {
        _currentMana += Time.deltaTime * _speedMana;
        CheckInstantiate();
    }

    private void CheckInstantiate()
    {
        Card randomCard = _cards[Random.Range(0, _cards.Count)];

        if (randomCard.Mana <= _currentMana && !_isDelay)
        {
            if (TryGameObject(randomCard.CardName, _isPlayerCharacter, out Character enemy))
            {
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                enemy.ResetHealth();
                SetEnemy(enemy, _spawnPoints[spawnPointNumber].position);
            }
            else
            {
                ExpandPool(randomCard, _isPlayerCharacter, out Character newEnemy);
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                SetEnemy(newEnemy, _spawnPoints[spawnPointNumber].position);
            }

            _currentMana -= _cards[0].Mana;
            StartCoroutine(DelayInstantiateEnemy());
        }
    }

    private IEnumerator DelayInstantiateEnemy()
    {
        _isDelay = true;
        yield return new WaitForSeconds(_delay);
        _isDelay = false;
    }

    private void SetEnemy(Character enemy, Vector3 spawnPoint)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }
}
