using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] Character _character;

    private float _delay = 1f;
    private float _lastAttackTime;
    private bool _isAttackState = false;
    private int _collisionCount = 0;

    private readonly RaycastHit2D[] _results = new RaycastHit2D[10];

    private void FixedUpdate()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (_character.ISPlayerCharacter)
        {
            _collisionCount = _rigidbody2D.Cast(transform.right, _results, 0.35f);
        }
        else
        {
            _collisionCount = _rigidbody2D.Cast(-transform.right, _results, -0.35f);
        }

        if (_collisionCount == 0)
        {
            _character.Move();
        }
        else
        {
            _isAttackState = CheckAttackEnemy();

            if (!_isAttackState)
            {
                _isAttackState = CheckAttackEnemyTower();
            }
            if (!_isAttackState)
            {
                _character.Move();
            }

        }
        _lastAttackTime -= Time.deltaTime;
    }

    private bool CheckAttackEnemy()
    {
        _isAttackState = false;

        for (int i = 0; i < _collisionCount; i++)
        {
            var availableCharacter = _results[i].collider.TryGetComponent(out Character character);

            if (availableCharacter)
            {
                if (_character.ISPlayerCharacter != character.ISPlayerCharacter)
                {
                    _character.Stop();
                    _isAttackState = true;

                    if (_lastAttackTime <= 0)
                    {
                        _character.AttackCharacter(character);
                        _lastAttackTime = _delay;
                    }
                }
            }
        }
        return _isAttackState;
    }

    private bool CheckAttackEnemyTower()
    {
        for (int i = 0; i < _collisionCount; i++)
        {
            var availableTower = _results[i].collider.TryGetComponent(out Tower tower);

            if (availableTower)
            {
                if (_character.ISPlayerCharacter != tower.ISPlayerTower)
                {
                    _character.Stop();
                    _isAttackState = true;

                    if (_lastAttackTime <= 0)
                    {
                        _character.AttackTower(tower);
                        _lastAttackTime = _delay;
                    }
                }
            }
        }
        return _isAttackState;
    }
}
