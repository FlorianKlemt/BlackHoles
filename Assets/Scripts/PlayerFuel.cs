using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerFuel : MonoBehaviour {
    private int startingFuel = 1000;                            // The amount of Fuel the player starts the game with.
    private int currentFuel = 1000;                                   // The current Fuel the player has.
    public Slider FuelSlider;                                 // Reference to the UI's Fuel bar.
    public Text messageField;

    private bool warning_message_shown, no_fuel;

    void Start()
    {
        messageField.text = "";
        currentFuel = startingFuel;
        warning_message_shown = false;
        no_fuel = false;
    }

    public void addFuel(int amount)
    {
        currentFuel = Math.Min(currentFuel + amount, startingFuel);
        FuelSlider.value = currentFuel;
        messageField.text = "";
        messageField.color = Color.black;
    }

    public void useFuel(int amount)
    {
        currentFuel -= amount;
        FuelSlider.value = currentFuel;

        if (currentFuel <= 0)
        {
            messageField.color = Color.red;
            messageField.text = "Out of fuel!";
            no_fuel = true;
        }else if (currentFuel <= startingFuel  * 2/10)
        {
            if (!warning_message_shown)
            {
                warning_message_shown = true;
                messageField.color = Color.yellow;
                messageField.text = "20% fuel left!\nRecharge imediatly";
            }
        }
        else
        {
            messageField.color = Color.black;
            warning_message_shown = false;
        }
    }

    public bool out_of_fuel()
    {
        return no_fuel;
    }
}
