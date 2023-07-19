using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    private Slider _soundVolumeSlider;
    [SerializeField] private TextMeshProUGUI shownVolume;
    void Start()
    {
        _soundVolumeSlider = GetComponent<Slider>();
        _soundVolumeSlider.value = Mathf.Round(SceneChangeInfo.Volume * 100f) / 100f;
    }

    public void showVolume()
    {
        shownVolume.text = Convert.ToInt32(_soundVolumeSlider.value * 100f).ToString() + "%";
        var items = GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform;
        if (!items) return;
        foreach (Transform item in items)
        {
            if (item.gameObject.name == "ButtonSFX1" || item.gameObject.name == "ButtonSFX2")
            {
                item.gameObject.GetComponent<AudioSource>().volume = (Mathf.Round(_soundVolumeSlider.value * 100f) / 100f) / 3f;
            }
            else
            {
                item.gameObject.GetComponent<AudioSource>().volume = Mathf.Round(_soundVolumeSlider.value * 100f) / 100f;
            }
        }
    }
    
    public void setVolume()
    {
        SceneChangeInfo.Volume = Mathf.Round(_soundVolumeSlider.value * 100f) / 100f;
        foreach (Transform item in GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform)
        {
            if (item.gameObject.name == "ButtonSFX1" || item.gameObject.name == "ButtonSFX2")
            {
                item.gameObject.GetComponent<AudioSource>().volume = (Mathf.Round(_soundVolumeSlider.value * 100f) / 100f) / 3f;
            }
            else
            {
                item.gameObject.GetComponent<AudioSource>().volume = Mathf.Round(_soundVolumeSlider.value * 100f) / 100f;
            }
        }
    }
}
