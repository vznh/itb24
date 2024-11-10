using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Dropdown mapDropdown;
    public Button beginButton;
    public TextMeshProUGUI loadButtonText; // Reference to the Load Button's text component
    public MapManager mapManager;

    // Option text for creating a new map
    private const string CREATE_NEW_MAP_OPTION = "Create New Map";
    private const string SELECT_MAP_OPTION = "* Select a Map";

    void Start()
    {
        PopulateDropdown();
        mapDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Set the Begin button to be initially disabled
        SetBeginButtonState(false);
        UpdateLoadButtonText("");
    }

    // Method to populate the dropdown with map names
    public void PopulateDropdown()
    {
        mapDropdown.ClearOptions();

        // Get the list of map names from the MapManager
        List<string> mapNames = mapManager.GetMapNames();

        // Add default options
        List<string> dropdownOptions = new List<string> { SELECT_MAP_OPTION };
        dropdownOptions.AddRange(mapNames);

        // Add the "Create New Map" option at the end
        dropdownOptions.Add(CREATE_NEW_MAP_OPTION);

        // Add the options to the TMP dropdown
        mapDropdown.AddOptions(dropdownOptions);

        // Reset the dropdown to the first option
        mapDropdown.value = 0;
        OnDropdownValueChanged(0);
    }

    // Method to handle dropdown selection changes
    private void OnDropdownValueChanged(int index)
    {
        string selectedOption = mapDropdown.options[index].text;

        // Check if "Create New Map" was selected
        if (selectedOption == CREATE_NEW_MAP_OPTION)
        {
            CreateNewMap();
        }
        else if (selectedOption != SELECT_MAP_OPTION)
        {
            SetBeginButtonState(true);
            UpdateLoadButtonText($"To load: {selectedOption}");
        }
        else
        {
            SetBeginButtonState(false);
            UpdateLoadButtonText("");
        }
    }

    // Method to create a new map
    private void CreateNewMap()
    {
        string newMapName = "New Map " + (mapManager.maps.Count + 1);
        mapManager.AddNewMap(newMapName);
        Debug.Log("New map created: " + newMapName);

        // Refresh the dropdown with the new map
        PopulateDropdown();

        // Select the newly created map in the dropdown
        int newMapIndex = mapDropdown.options.FindIndex(option => option.text == newMapName);
        if (newMapIndex >= 0)
        {
            mapDropdown.value = newMapIndex;
            OnDropdownValueChanged(newMapIndex);
        }
    }

    // Method to set the Begin button's state and opacity
    private void SetBeginButtonState(bool isEnabled)
    {
        beginButton.interactable = isEnabled;
        Color buttonColor = beginButton.image.color;
        buttonColor.a = isEnabled ? 1.0f : 0.2f; // Set opacity to 100% or 20%
        beginButton.image.color = buttonColor;
    }

    // Method to update the Load button text
    private void UpdateLoadButtonText(string mapName)
    {
        if (loadButtonText != null)
        {
            loadButtonText.text = mapName;
        }
    }
}
