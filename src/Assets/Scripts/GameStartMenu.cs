using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
  [Header("UI Pages")]
  public GameObject mainMenu;
  public GameObject buildMenu;
  public GameObject community;
  public GameObject about;

  [Header("Main Menu Buttons")]
  public Button startButton;
  public Button buildButton;
  public Button communityButton;
  public Button aboutButton;
  public Button quitButton;

  public List<Button> returnButtons;

  // Start is called before the first frame update
  void Start()
  {
    EnableMainMenu();

    //Hook events
    startButton.onClick.AddListener(StartGame);
    buildButton.onClick.AddListener(EnableBuildMenu);
    communityButton.onClick.AddListener(EnableCommunity);
    aboutButton.onClick.AddListener(EnableAbout);
    quitButton.onClick.AddListener(QuitGame);

    foreach (var item in returnButtons)
    {
      item.onClick.AddListener(EnableMainMenu);
    }
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  /*
  * TODO: This has to start when the map is loaded.
  */
  public void StartGame()
  {
    HideAll();
    SceneTransitionManager.singleton.GoToSceneAsync(1);
  }

  public void HideAll()
  {
    mainMenu.SetActive(false);
    community.SetActive(false);
    about.SetActive(false);
  }

  public void EnableMainMenu()
  {
    mainMenu.SetActive(true);
    buildMenu.SetActive(false);
    community.SetActive(false);
    about.SetActive(false);
  }

  public void EnableBuildMenu()
  {
    mainMenu.SetActive(false);
    buildMenu.SetActive(true);
    community.SetActive(false);
    about.SetActive(false);
  }

  public void EnableCommunity()
  {
    mainMenu.SetActive(false);
    buildMenu.SetActive(false);
    community.SetActive(true);
    about.SetActive(false);
  }

  public void EnableAbout()
  {
    mainMenu.SetActive(false);
    buildMenu.SetActive(false);
    community.SetActive(false);
    about.SetActive(true);
  }
}
