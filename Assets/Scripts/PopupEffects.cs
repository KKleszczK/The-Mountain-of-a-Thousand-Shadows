using UnityEngine;


//Old veersion Codee
public class PopupEffects : MonoBehaviour
{
    public GameObject successPopupPrefab;
    public GameObject missPopupPrefab;

    public Transform popupSpawnPoint;
    public Canvas uiCanvas;

    public void ShowSuccess()
    {
        CreatePopup(successPopupPrefab);
    }

    public void ShowMiss()
    {
        CreatePopup(missPopupPrefab);
    }

    private void CreatePopup(GameObject prefab)
    {
        if (prefab == null || popupSpawnPoint == null || uiCanvas == null) return;

        GameObject popup = Instantiate(prefab);
        popup.transform.SetParent(uiCanvas.transform, false);

        RectTransform popupRect = popup.GetComponent<RectTransform>();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(popupSpawnPoint.position);
        popupRect.position = screenPos;

        popupRect.localScale = Vector3.one;
    }
}
