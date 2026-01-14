using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _inGameMenuObject;

    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _settingsMenu;

    [SerializeField] private GameObject _playMenu;
    [SerializeField] private GameObject _singlePlayerMenu;
    [SerializeField] private GameObject _multiPlayerMenu;

    [SerializeField] private GameObject _tutorialMenu;
    [SerializeField] private GameObject _characterSelectionMenu;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _optionsMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;

    [SerializeField] private GameObject _playMenuFirst;
    [SerializeField] private GameObject _singlePlayerMenuFirst;
    [SerializeField] private GameObject _multiPlayerMenuFirst;

    [SerializeField] private GameObject _tutorialFirst;
    [SerializeField] private GameObject _characterSelectionFirst;

    //[SerializeField] public static int characterIndex;
    [SerializeField] private static int characterIndex = 0;

    public static int ChosenCharacterIndex
    {
        get
        {
            return characterIndex;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("TestingArea");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Quitted the game");
    }

    private void SafeSelect(GameObject gameObject)
    {
        if (gameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    private void SafeSetActive(GameObject gameObject, bool state)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(state);
        }
    }
    
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OptionsMenuHandle()
    {
        SafeSelect(_optionsMenuFirst);
    }
    public void SettingsMenuFirst()
    {
        SafeSelect(_settingsMenuFirst);
    }
    public void PlayMenuFirst()
    {
        SafeSelect(_playMenuFirst);
    }

    public void SinglePlayerMenuFirst()
    {
        SafeSelect(_singlePlayerMenuFirst);
    }
    public void MultiPlayerMenuFirst()
    {
        SafeSelect(_multiPlayerMenuFirst);
    }

    public void CharacterSelectorFirst()
    {
        SafeSelect(_characterSelectionFirst);
    }
    public void TutorialFirst()
    {
        SafeSelect(_tutorialFirst);
    }

    /*
    public void Tutorial()
    {
        SafeSelect(_tutorialPage);
    }

    public void CharacterSelection()
    {
        SafeSelect(_characterSelection);
    }

    */



    public void OpenCharacterSelectionScreen()
    {
        SafeSetActive(_singlePlayerMenu, false);
        SafeSetActive(_characterSelectionMenu, true);

    }

    public void OpenTutorialScreen()
    {
        SafeSetActive(_playMenu, false);
        SafeSetActive(_tutorialMenu, true);
    }


    public void OpenOptionsMenuIngame()
    {
        SafeSetActive(_optionsMenu, true);
        SafeSetActive(_settingsMenu, false);
        OptionsMenuHandle();
    }

    public void OpenSettingsMenuIngame()
    {
        SafeSetActive(_optionsMenu, false);
        SafeSetActive(_settingsMenu, true);
        SettingsMenuFirst();
    }
    public void OpenOptionsMenu()
    {
        SafeSetActive(_optionsMenu, true);
        SafeSetActive(_settingsMenu, false);
        SafeSetActive(_playMenu, false);
        SafeSetActive(_singlePlayerMenu, false);
        SafeSetActive(_multiPlayerMenu, false);
        /*
        _optionsMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _playMenu.SetActive(false);
        _singlePlayerMenu.SetActive(false);
        _multiPlayerMenu.SetActive(false);
        */
        OptionsMenuHandle();
        //EventSystem.current.SetSelectedGameObject(_optionsMenuFirst);
    }

    public void OpenSettingsMenu()
    {
        SafeSetActive(_settingsMenu, true);
        SafeSetActive(_optionsMenu, false);
        SafeSetActive(_playMenu, false);
        SafeSetActive(_singlePlayerMenu, false);
        SafeSetActive(_multiPlayerMenu, false);
        /*
        _settingsMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _playMenu.SetActive(false);
        _singlePlayerMenu.SetActive(false);
        _multiPlayerMenu.SetActive(false);*/
        SettingsMenuFirst();
    }

    public void OpenPlayMenu()
    {
        SafeSetActive(_playMenu, true);
        SafeSetActive(_optionsMenu, false);
        SafeSetActive(_settingsMenu, false);
        SafeSetActive(_singlePlayerMenu, false);
        SafeSetActive(_multiPlayerMenu, false);
        SafeSetActive(_tutorialMenu, false);
        SafeSetActive(_characterSelectionMenu, false);
        /*
        _playMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _singlePlayerMenu.SetActive(false);
        _multiPlayerMenu.SetActive(false);*/
        PlayMenuFirst();

    }

    public void OpenSinglePlayerMenu()
    {
        SafeSetActive(_singlePlayerMenu, true);
        SafeSetActive(_optionsMenu, false);
        SafeSetActive(_settingsMenu, false);
        SafeSetActive(_playMenu, false);
        SafeSetActive(_multiPlayerMenu, false);
        /*
        _singlePlayerMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playMenu.SetActive(false);
        _multiPlayerMenu.SetActive(false);*/
        SinglePlayerMenuFirst();
    }

    public void OpenMultiPlayerMenu()
    {

        SafeSetActive(_multiPlayerMenu, true);
        SafeSetActive(_optionsMenu, false);
        SafeSetActive(_settingsMenu, false);
        SafeSetActive(_playMenu, false);
        SafeSetActive(_singlePlayerMenu, false);
        MultiPlayerMenuFirst();
        /*
        _multiPlayerMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playMenu.SetActive(false);
        _singlePlayerMenu.SetActive(false);*/
       // MultiPlayerMenuFirst();

    }



    public void CloseAllInGameMenus()
    {
        _optionsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _inGameMenuObject.SetActive(false);
    }


    public void CharacterIndexSettor1()
    {
        characterIndex = 1;
        Debug.Log("You have choosen character 1");
    }
    public void CharacterIndexSettor2()
    {
        characterIndex = 2;
        Debug.Log("You have choosen character 2");
    }

    public void CharacterIndexSettor3()
    {
        characterIndex = 3;
        Debug.Log("You have choosen character 3");
    }

}
