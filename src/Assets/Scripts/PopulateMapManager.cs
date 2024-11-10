using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownManager : MonoBehaviour
{
  public TMP_Dropdown mapDropdown;
  public MapManager mapManager;

  private const string CREATE_NEW_MAP_OPTION = "+ Create new map";

  void Start()
  {
    PopulateDropdown();
  }

  // Method to populate the dropdown with map names
  public void PopulateDropdown()
  {
    // Clear existing options
    mapDropdown.ClearOptions();

    // Get the list of map names from the MapManager
    List<string> mapNames = mapManager.GetMapNames();

    // Add a default "Select a Map" option
    List<string> dropdownOptions = new List<string> { "* Map not selected" };
    dropdownOptions.AddRange(mapNames);

    // Add "Create new map" option
    dropdownOptions.Add(CREATE_NEW_MAP_OPTION);

    // Add the options to the TMP dropdown
    mapDropdown.AddOptions(dropdownOptions);
  }

  // Method to handle when the user selects an option in the dropdown
  private void OnDropdownValueChanged(int index)
  {
    // Get the selected option text
    string selectedOption = mapDropdown.options[index].text;

    // Check if the "Create New Map" option was selected
    if (selectedOption == CREATE_NEW_MAP_OPTION)
    {
      CreateNewMap();
    }
    else
    {
      // Handle loading an existing map if needed
      Debug.Log("Selected map: " + selectedOption);
    }
  }

  // Method to create a new map
  private void CreateNewMap()
  {
    string newMapName = "New Map " + (mapManager.maps.Count + 1);
    mapManager.AddNewMap(newMapName);
    Debug.Log("New map created: " + newMapName);

    // Refresh the dropdown to include the newly created map
    PopulateDropdown();
  }
}
