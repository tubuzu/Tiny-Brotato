using System.Collections;
using UnityEngine;

public class ItemDrop : MyMonoBehaviour
{
    public ItemDropProfile profile;
    [SerializeField] float flyToPlayerSpeed = 5f;

    Rigidbody2D _rb;
    WaitForFixedUpdate waitForFixedUpdate = new();

    public bool picked = false;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        picked = false;
    }

    public void OnShowUp(Vector3 direction)
    {
        _rb.AddForce(direction * 10f);
    }

    public void Picked(Vector3 position)
    {
        StartCoroutine(PickedCoroutine(position));
    }
    public IEnumerator PickedCoroutine(Vector3 position)
    {
        picked = true;
        while ((position - transform.position).magnitude > 0.1f)
        {
            _rb.velocity = (position - transform.position).normalized * flyToPlayerSpeed;
            yield return waitForFixedUpdate;
        }
        _rb.velocity = Vector2.zero;
        ItemDropSpawner.Instance.Despawn(gameObject);
    }
}
