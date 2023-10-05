// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyAbstract
{
    [Header("Enemy values")]
    [SerializeField] private int point = 50;
    public bool IsBoss = false;

    // private SpriteRenderer _spriteRenderer;

    // protected override void Start()
    // {
    //     base.Start();
    //     _spriteRenderer = enemyCtrl.Model.GetComponent<SpriteRenderer>();
    // }

    public int GetPoints() => point;
}