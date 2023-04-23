using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class volumeSliderScript : MonoBehaviour
{
    public Slider volumeSlider;
    //public AudioMixer masterMixer;

    private void Start(){

    //    setVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        if(!PlayerPrefs.HasKey("soundVolume")){
            PlayerPrefs.SetFloat("soundVolume", 1);
            Load();
        }

        else{
            Load();
        }

    }

    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");

    }

    private void Save(){
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
    }

    // public void setVolume(float value){

    //     if(value < 1){
    //         value = .001f;
    //     }
    //     refreshSlider(value);
    //     PlayerPrefs.SetFloat("SavedMasterVolume", value);
    //     masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) + 20f);
    // }

    // public void setVolumeFromSlider (){

    //     setVolume(soundSlider.value);
    // }

    // public void refreshSlider(float value){
    //     soundSlider.value = value;
    // }
}
