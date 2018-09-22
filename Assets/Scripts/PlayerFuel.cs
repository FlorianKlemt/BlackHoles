using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour {
    private int startingFuel = 1000;                            // The amount of Fuel the player starts the game with.
    private int currentFuel = 1000;                                   // The current Fuel the player has.
    public Slider FuelSlider;                                 // Reference to the UI's Fuel bar.
    public Text messageField;


    void Start()
    {
        messageField.text = "";
        currentFuel = startingFuel;
    }

    public void useFuel(int amount)
    {
        // Reduce the current Fuel by the damage amount.
        currentFuel -= amount;

        // Set the Fuel bar's value to the current Fuel.
        FuelSlider.value = currentFuel;

        if (currentFuel <= 0)
        {
            messageField.text = "Out of fuel!";
        }
    }
}
