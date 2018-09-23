using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerUpGUIController : MonoBehaviour {

    public enum Powerup {None, SpeedPowerUp, DamagePowerUp };
    float speed_duration, damage_duration;
    float current_upgrade_used_time, current_upgrade_max_time;
    public Powerup current_upgrade;


    private Slider powerup_slider;
    private Image powerup_image, powerup_background, powerup_fill;
    public Sprite speed_sprite, damage_sprite;

    public GameObject powerup_ui;
    // Use this for initialization
    void Start () {
        current_upgrade = Powerup.None;
        PlayerController player_controller = GetComponent<PlayerController>();
        speed_duration = player_controller.speed_boost_duration;
        damage_duration = player_controller.weapon_duration;
        powerup_slider = powerup_ui.transform.Find("CurrentPowerupSlider").GetComponent<Slider>();
        powerup_image = powerup_ui.transform.Find("CurrentPowerupImage").GetComponent<Image>();
        powerup_fill = powerup_slider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();

        current_upgrade_used_time = 0;
        powerup_ui.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (current_upgrade != Powerup.None)
        {
            current_upgrade_used_time += Time.deltaTime;
            powerup_slider.value = Math.Max(0,current_upgrade_max_time-current_upgrade_used_time);
            if (powerup_slider.value <= 0)
            {
                powerup_ui.SetActive(false);
                set_current_upgrade(Powerup.None);
            }
        }
	}

    public void set_current_upgrade(Powerup powerup)
    {
        if(current_upgrade == Powerup.None)
        {
            powerup_ui.SetActive(true);
        }

        current_upgrade = powerup;
        if (current_upgrade == Powerup.SpeedPowerUp)
        {
            current_upgrade_max_time = speed_duration;
            powerup_image.sprite = speed_sprite;
            //powerup_fill.color = new Color();
        }
        else if (current_upgrade == Powerup.DamagePowerUp)
        {
            current_upgrade_max_time = damage_duration;
            powerup_image.sprite = damage_sprite;
            //powerup_fill.color = new Color();
        }
        current_upgrade_used_time = 0;
        powerup_slider.maxValue = current_upgrade_max_time;
        powerup_slider.value = current_upgrade_max_time;
    }
}
