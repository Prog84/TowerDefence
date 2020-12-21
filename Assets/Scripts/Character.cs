using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] private int _health;
    [SerializeField] private int _attackTowerMultiplier = 2;

    private int _maxHealth;
    private int _attack;
    private int _speed;
    private bool _isDie = false;

    private bool _isPlayerCharacter;
    public bool ISPlayerCharacter => _isPlayerCharacter;
  
    private int _mana;
    public int Mana => _mana;

    private string _cardName;

    public string CardName => _cardName;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Init(Card card, bool isPlayerCharacter)
    {
        _cardName = card.CardName;
        _health = card.Health;
        _attack = card.Attack;
        _speed = card.Speed;
        _mana = card.Mana;
        _maxHealth = card.Health;
        _isPlayerCharacter = isPlayerCharacter;
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
        _isDie = false;
    }

     void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0 && !_isDie)
        {
            _isDie = true;
            _animator.SetTrigger("Death");
        }
    }

    public void AttackCharacter(Character target)
    {
        _animator.SetTrigger("Attack");
        target.TakeDamage(_attack);
    }

    public void AttackTower(Tower target)
    {  
        _animator.SetTrigger("Attack");
        target.TakeDamage(_attack * _attackTowerMultiplier);
    }

    public void Stop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
    }

    public void Move()
    {
        _rigidbody2D.velocity = transform.right * _speed;
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
