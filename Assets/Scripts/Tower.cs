using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Tower : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private bool _isPlayerTower;
    public bool ISPlayerTower => _isPlayerTower;

    private bool _isWinGame = false;
    private Animator _animator;

    public event UnityAction<bool> GameOver;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("Damage");

        if (_health <= 0)
        {
            if (_isPlayerTower)
            {
                _isWinGame = false;
            }
            else
            {
                _isWinGame = true;
            }
            _animator.SetTrigger("Death");
        }
    }

    public void Die()
    {
        GameOver?.Invoke(_isWinGame);
    }
}
