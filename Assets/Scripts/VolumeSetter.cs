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
    }
    
    public void setVolume()
    {
        SceneChangeInfo.Volume = Mathf.Round(_soundVolumeSlider.value * 100f) / 100f;
    }
}
