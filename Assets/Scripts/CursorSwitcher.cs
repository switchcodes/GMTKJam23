using UnityEngine;
using UnityEngine.EventSystems;

public class CursorSwitcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	public void OnPointerEnter(PointerEventData eventData) {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

	public void OnPointerExit(PointerEventData eventData) {
		Cursor.SetCursor(null, hotSpot, cursorMode);
	}
}