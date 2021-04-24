using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsersControl : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;
    [SerializeField] Dropdown UsersDropDown;
    [SerializeField] Text UserAlreadyExistsText;

    [SerializeField] Toggle newUserToggle;
    [SerializeField] Toggle existingUserToggle;

    [SerializeField] GameObject NewUserInput;


    [SerializeField] UserNames_SoList UserNamesList;
    [SerializeField] Text CurrentUserText;

    public static string currentUser;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //if (UserNamesList != null)
        //    UserNamesList.UserNames.Clear();
        if (UsersDropDown != null)
        UsersDropDown.onValueChanged.AddListener((int users) => currentUser = UsersDropDown.options[UsersDropDown.value].text);
        if(CurrentUserText != null)
        CurrentUserText.text = currentUser;
    }

    public void ToggleSet(Toggle toggle)
    {
        if(toggle.isOn)
        {
            if(toggle == newUserToggle)
            {
                NewUserInput.SetActive(true);
                UsersDropDown.gameObject.SetActive(false);
            }
            if(toggle == existingUserToggle)
            {
                LoadUserNames();

                UsersDropDown.gameObject.SetActive(true);
                PlayButton.SetActive(true);
                NewUserInput.SetActive(false);
                currentUser = UsersDropDown.options[UsersDropDown.value].text;
            }
        }
    }


    private void LoadUserNames()
    {
        bool isNewUser = true;
        foreach (var user in UserNamesList.UserNames)
        {
            //if(!PlayerPrefs.HasKey(user))
            //{
            //    PlayerPrefs.SetInt(user, 0);
            //}
            if(!UserNamesList.userScores.ContainsKey(user))
            {
                UserNamesList.userScores.Add(user, 0);
            }
            
            Dropdown.OptionData newUser = new Dropdown.OptionData(user);
            foreach (var existinguser in UsersDropDown.options)
            {
                if(existinguser.text == newUser.text)
                {
                    isNewUser = false;
                }
            }
            if (isNewUser)
            {
                UsersDropDown.options.Add(newUser);
            }
        }
    }

    public void SaveNewUser(InputField newUserNameIF)
    {
        if(!UserNamesList.userScores.ContainsKey(newUserNameIF.text))//PlayerPrefs.HasKey(newUserNameIF.text))
        {
            UserNamesList.UserNames.Add(newUserNameIF.text);
            UserNamesList.userScores.Add(newUserNameIF.text, 0);
            currentUser = newUserNameIF.text;
        }
        else
        {
            UserAlreadyExistsText.gameObject.SetActive(true);
        }
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

