using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManagaer : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    public GameObject volume_0;
    public GameObject volume_1;
    public GameObject volume_2;
    public GameObject volume_3;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
        slider.onValueChanged.AddListener(SetVolume);
        //mixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat("Volume", value);
        PlayerPrefs.SetFloat("Volume", value);
    }

    private void FixedUpdate()
    {
        float dB = PlayerPrefs.GetFloat("Volume");
        volume_0.SetActive(dB <= -80f);
        volume_1.SetActive(dB > -80f && dB <= -40f);
        volume_2.SetActive(dB > -40f && dB <= -20f);
        volume_3.SetActive(dB > -20f);
    }
}