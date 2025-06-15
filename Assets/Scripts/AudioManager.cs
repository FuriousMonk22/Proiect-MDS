using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManagaer : MonoBehaviour
{
    public AudioMixer mixer1;
    public AudioMixer mixer2;

    public Slider slider1;
    public Slider slider2;

    public GameObject volume_0;
    public GameObject volume_1;
    public GameObject volume_2;
    public GameObject volume_3;

    public GameObject volume_4;
    public GameObject volume_5;
    public GameObject volume_6;
    public GameObject volume_7;

    void Start()
    {
        slider1.value = PlayerPrefs.GetFloat("Volume1");
        slider1.onValueChanged.AddListener(SetVolume);
        //mixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));

        slider2.value = PlayerPrefs.GetFloat("Volume2");
        slider2.onValueChanged.AddListener(SetVolume1);

    }

    public void SetVolume(float value)
    {
        mixer1.SetFloat("Volume1", value);
        PlayerPrefs.SetFloat("Volume1", value);
    }

    public void SetVolume1(float value)
    {
        mixer2.SetFloat("Volume2", value);
        PlayerPrefs.SetFloat("Volume2", value);
    }

    private void FixedUpdate()
    {
        float dB = PlayerPrefs.GetFloat("Volume1");
        volume_0.SetActive(dB <= -80f);
        volume_1.SetActive(dB > -80f && dB <= -40f);
        volume_2.SetActive(dB > -40f && dB <= -20f);
        volume_3.SetActive(dB > -20f);

        float dB1 = PlayerPrefs.GetFloat("Volume2");
        volume_4.SetActive(dB1 <= -60f);
        volume_5.SetActive(dB1 > -60f && dB1 <= -40f);
        volume_6.SetActive(dB1 > -40f && dB1 <= -20f);
        volume_7.SetActive(dB1 > -20f);
    }
}