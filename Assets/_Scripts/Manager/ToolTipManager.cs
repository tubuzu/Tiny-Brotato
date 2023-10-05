using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolTipName
{
    None,
    ItemInfo,
    WeaponInfo,
}

public class ToolTipManager : MyMonoBehaviour
{
    public static ToolTipManager Instance;
    GameObject currentToolTip;
    public RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        rectTransform = GetComponent<RectTransform>();
    }

    protected override void Start()
    {
        base.Start();
        Cursor.visible = true;
        foreach (Transform t in transform) t.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        rectTransform.position = Input.mousePosition;
    }

    public GameObject GetToolTip(ToolTipName toolTipName)
    {
        GameObject tooltip = transform.Find(toolTipName.ToString()).gameObject;
        currentToolTip = tooltip;
        gameObject.SetActive(true);
        Vector3 mousePosition = Input.mousePosition;
        rectTransform.position = mousePosition;
        tooltip.SetActive(true);
        return tooltip;
    }

    public void HideCurrentToolTip()
    {
        currentToolTip.SetActive(false);
        gameObject.SetActive(false);
    }
}
