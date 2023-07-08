using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollToBottom : MonoBehaviour {

	private RectTransform _rectTransform;
	private ScrollRect _scrollRect;
	
	// Start is called before the first frame update
	void Start() {
		_rectTransform = gameObject.GetComponent<RectTransform>();
		_scrollRect = gameObject.GetComponent<ScrollRect>();
	}

	// Update is called once per frame
	void Update() {
		var scrollValue = 1 + _rectTransform.anchoredPosition.y / _scrollRect.content.rect.height;
		_scrollRect.verticalScrollbar.value = scrollValue;
	}
}