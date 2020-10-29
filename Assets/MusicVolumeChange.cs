using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeChange : MonoBehaviour
{

    public Slider slider;



    private void Start()
    {


        if(!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("Music");
        }
    }

    private void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Music");
    }

    public void VolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
    }

}
