using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for handling the UI showing the user's date. Also for the start screen functionality (choosing an existing user, registering a new one)
/// </summary>


public class UsersControl : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;
    [SerializeField] Dropdown UsersDropDown;
    [SerializeField] Text UserAlreadyExistsText;

    [SerializeField] Toggle newUserToggle;
    [SerializeField] Toggle existingUserToggle;

    [SerializeField] GameObject NewUserInput;


    [SerializeField] public UserNames_SoList UserNamesList;
    [SerializeField] Text CurrentUserText;
    [SerializeField] Text Score;
    public static string currentUser;
    public static int currentScore;

    private void Start()
    {
        if (UserNamesList != null)
        {
            if (UserNamesList.isFirstRun) //clear all saved date on the first run of the APP on a device
            {
                UserNamesList.isFirstRun = false;
                UserNamesList.userScores = new Dictionary<string, int>();
                UserNamesList.UserNames.Clear();
                UserNamesList.userScores.Clear();
            }
        }

        if (UsersDropDown != null)
            UsersDropDown.onValueChanged.AddListener((int users) => currentUser = UsersDropDown.options[UsersDropDown.value].text); //register an inline function to the event onValueChanged of the dropdown. This simply saves the selected user name as the current user everytime a new user is selected from the dropdown

        if (CurrentUserText != null)
            CurrentUserText.text = currentUser; //update the text to match the current user name

        if (UserNamesList != null)
            currentScore = UserNamesList.userScores[currentUser]; //update the text to match the current user score

    }


    /// <summary>
    /// Function to be called on the event OnValueChanged on the toggles in the inspector. different behaviour according to the selected toggle.
    /// </summary>
    /// <param name="toggle">the toggle on which this function is called</param>
    public void ToggleSet(Toggle toggle)
    {
        if (toggle.isOn) //if toggle is selected
        {
            if (toggle == newUserToggle) //if it's the new user toggle
            {
                NewUserInput.SetActive(true); //activate new user input UI components
                UsersDropDown.gameObject.SetActive(false); //deactivate the existing users dropdown
            }
            if (toggle == existingUserToggle) //if it's the existing user toggle
            {
                LoadUserNames(); //load saved user names in the dropdown

                UsersDropDown.gameObject.SetActive(true); //activate the existing users dropdown
                PlayButton.SetActive(true); //activate the play button
                NewUserInput.SetActive(false); //deactivate new user input UI components
                currentUser = UsersDropDown.options[UsersDropDown.value].text; //set current user to match the selected user form the dropdown
            }
        }
    }

    /// <summary>
    /// Load existing user names to the dropdown
    /// </summary>
    private void LoadUserNames()
    {
        bool isNewUser = true;
        foreach (var user in UserNamesList.UserNames) //iterate over all the saved user names
        {
            if (!UserNamesList.userScores.ContainsKey(user)) //if this user has no score assigned
            {
                UserNamesList.userScores.Add(user, 0); //assign the default score to this user
            }

            Dropdown.OptionData newUser = new Dropdown.OptionData(user); //construct a new OptionData object with this user's name for the dropdown
            foreach (var existinguser in UsersDropDown.options) //iterate overall already filled usernames in the dropdown
            {
                if (existinguser.text == newUser.text) //if this name is already in the dropdown
                {
                    isNewUser = false; //mark it as not new
                }
            }
            if (isNewUser) // after checking all names already in the dropdown, if this user is new
            {
                UsersDropDown.options.Add(newUser); //add the name to the list
            }
        }
    }


    /// <summary>
    /// Saves the entered new user name
    /// </summary>
    /// <param name="newUserNameIF">Input field that gets the name from the user</param>
    public void SaveNewUser(InputField newUserNameIF)
    {
        if (!UserNamesList.userScores.ContainsKey(newUserNameIF.text)) //if it's a new user
        {
            UserNamesList.UserNames.Add(newUserNameIF.text); //save the new name to the list
            UserNamesList.userScores.Add(newUserNameIF.text, 0); //assign a default score of 0 to this new user
            currentUser = newUserNameIF.text; //assign this user's name to be the current user
        }
        else //if the name already exists
        {
            UserAlreadyExistsText.gameObject.SetActive(true); //show a UI warning
        }
    }

    /// <summary>
    /// Loads the game scene
    /// </summary>
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Updates the score text each time the score is updated. Called on listener on the OnGoalScored event.
    /// </summary>
    public void UpdateScoreText()
    {
        if (Score != null)
            Score.text = currentScore.ToString();
    }
}

