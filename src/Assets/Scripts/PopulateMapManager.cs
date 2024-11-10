using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownManager : MonoBehaviour
{
  public TMP_Dropdown mapDropdown;
  public MapManager mapManager;

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

    // Add the options to the TMP dropdown
    mapDropdown.AddOptions(dropdownOptions);
  }
}
