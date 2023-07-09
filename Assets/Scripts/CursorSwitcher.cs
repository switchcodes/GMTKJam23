using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CursorSwitcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	private void Awake()
	{
		if (GameObject.FindGameObjectWithTag("AudioManager") != null)
		{
			if (GameObject.FindGameObjectWithTag("AudioManagerLoaded") != null)
			{
				Destroy(GameObject.FindGameObjectWithTag("AudioManager"));
			}
			else
			{
				GameObject.FindGameObjectWithTag("AudioManager").transform.Find("BgOst").GetComponent<AudioSource>().Play();
				GameObject.FindGameObjectWithTag("AudioManager").tag = "AudioManagerLoaded";
			}
			DontDestroyOnLoad(GameObject.FindGameObjectWithTag("AudioManagerLoaded"));
		}

	}

	public void OnPointerEnter(PointerEventData eventData) {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

	public void OnPointerExit(PointerEventData eventData) {
		Cursor.SetCursor(null, hotSpot, cursorMode);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log(gameObject.tag);
		if (GameObject.FindGameObjectWithTag("AudioManager") != null)
		{
			if (gameObject.CompareTag("UIButton"))
			{
				GameObject.FindGameObjectWithTag("AudioManager").transform.Find("ButtonSFX1").GetComponent<AudioSource>().Play();
			}
			else
			{
				GameObject.FindGameObjectWithTag("AudioManager").transform.Find("ButtonSFX2").GetComponent<AudioSource>().Play();
			}
		}
		else if (GameObject.FindGameObjectWithTag("AudioManagerLoaded") != null)
		{
			if (gameObject.CompareTag("UIButton"))
			{
				GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform.Find("ButtonSFX1").GetComponent<AudioSource>().Play();
			}
			else
			{
				GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform.Find("ButtonSFX2").GetComponent<AudioSource>().Play();
			}
		}
	}
}