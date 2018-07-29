using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeSoundHandler : MonoBehaviour {
    /// <summary>
    /// Grabs both gauges and the current value for each of them.
    /// </summary>

    //Grab the gameObject for each gauge - set them in the inspector
    [SerializeField]
    private GameObject SFXGaugeGO;
    [SerializeField]
    private GameObject MusicGaugeGO;


    private GaugeSettingsHandler SFXGaugeSettings;
    private GaugeSettingsHandler MusicGaugeSettings;

    private float sfxVolume;
    private float musicVolume;
    
    //Awake calls before both Start() and before all game logic
    private void Awake()
    {
        SFXGaugeSettings = SFXGaugeGO.GetComponent<GaugeSettingsHandler>();
        MusicGaugeSettings = MusicGaugeGO.GetComponent<GaugeSettingsHandler>();
    }
    
    //Start occurs before any game logic is accessed
    void Start ()
    {
        sfxVolume = SFXGaugeSettings.GetValue();
        musicVolume = MusicGaugeSettings.GetValue();
	}

    private void Update()
    {

        sfxVolume = SFXGaugeSettings.GetValue();
        musicVolume = MusicGaugeSettings.GetValue();

        AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        AkSoundEngine.SetRTPCValue("SFXVolume", sfxVolume);

        //Debug.Log(sfxVolume);
        //Debug.Log(musicVolume);
    }
}
