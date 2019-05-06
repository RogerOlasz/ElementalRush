using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public MultiplayerManager multiplayer_manager;

    public string username;
    public string password;
    public string email;
    public bool have_account = false;

    LoginWithPlayFabRequest login_request;

    // Start is called before the first frame update
    void Start()
    {
        if(have_account == false)
        {
            Register();
        }
        else if(have_account == true)
        {
            Login();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        login_request = new LoginWithPlayFabRequest();
        login_request.Username = username;
        login_request.Password = password;

        PlayFabClientAPI.LoginWithPlayFab(login_request,
        result =>
        {
            //If the account is found
            multiplayer_manager.ConnectToMaster();
            Debug.Log("You are now logged in!");
        }, error =>
        {
            // If the account is not found
            Debug.Log(error.ErrorMessage);
        }, null);
    }

    public void Register()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email = email;
        request.Username = username;
        request.Password = password;

        PlayFabClientAPI.RegisterPlayFabUser(request,
        result =>
        {
            Debug.Log("Your account has been created succesfully!");
        }, error =>
        {
            Debug.Log("Failed to create your account\n [" + error.ErrorMessage + "]");
        });
    }
}
