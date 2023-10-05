// using System.Collections;
// using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerPickup : PlayerAbstract
{
    CircleCollider2D circleCollider2D;

    bool canPick = false;

    protected override void Awake()
    {
        base.Awake();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        circleCollider2D.radius = 1 + playerCtrl.PlayerStatus.PickUpRangeMultiplier / 100;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnWaveStart += OnGameStart;
        GameManager.Instance.OnWaveStop += OnGameStop;
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.OnWaveStart -= OnGameStart;
        GameManager.Instance.OnWaveStop -= OnGameStop;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("ItemDrop")) return;
        if (canPick && other.TryGetComponent(out ItemDrop itemDrop) && !itemDrop.picked) Pick(itemDrop);
    }

    public void Pick(ItemDrop itemDrop) => StartCoroutine(PickCoroutine(itemDrop));

    public IEnumerator PickCoroutine(ItemDrop itemDrop)
    {
        switch (itemDrop.profile.itemType)
        {
            case EnumManager.ItemDropType.GEM:
                yield return itemDrop.PickedCoroutine(playerCtrl.PlayerDamageReceiver.Collider.bounds.center);
                playerCtrl.PlayerStatus.ChangeGemNum(((GemDropProfile)itemDrop.profile).count, true);
                yield break;
            default: yield break;
        }
    }

    void OnGameStart()
    {
        canPick = true;
    }
    void OnGameStop()
    {
        canPick = false;
    }
}
