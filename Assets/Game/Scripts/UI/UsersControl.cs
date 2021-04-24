using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsersControl : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;
    [SerializeField] Dropdown UsersDropDown;

    [SerializeField] Toggle newUserToggle;
    [SerializeField] Toggle existingUserToggle;

    [SerializeField] GameObject NewUserInput;


    [SerializeField] UserNames_SoList UserNamesList;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        UserNamesList.UserNames.Clear();
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
            }
        }
    }


    private void LoadUserNames()
    {
        foreach (var user in UserNamesList.UserNames)
        {
            if(!PlayerPrefs.HasKey(user))
            {
                PlayerPrefs.SetInt(user, 0);
            }
            Dropdown.OptionData newUser = new Dropdown.OptionData(user);
            if (!UsersDropDown.options.Contains(newUser))
            {
                UsersDropDown.options.Add(newUser);
            }
        }
    }

    public void SaveNewUser(InputField newUserNameIF)
    {
        if(!PlayerPrefs.HasKey(newUserNameIF.text))
        {
            PlayerPrefs.SetInt(newUserNameIF.text, 0);
            UserNamesList.UserNames.Add(newUserNameIF.text);
        }
    }
}

