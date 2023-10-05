using UnityEngine;

public class RectTransformFollowMouse : MonoBehaviour
{
    public RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        rectTransform.position = mousePosition;
    }
}
