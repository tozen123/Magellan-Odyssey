using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class User
{
    public string name;
    public string email;
    public string password;
    public string userID;


    public User() { }

    public User(string name, string email, string userID, string password)
    {
        this.name = name;
        this.email = email;
        this.userID = userID;
        this.password = password;
    }

    public void WriteUserToDatabase()
    {
        RestClient.Put($"{GlobalVariables.FIREBASE_DATABASE_URL}users/{userID}.json", this).Then(response =>
        {
            Debug.Log("User data posted successfully!");
        }).Catch(error =>
        {
            Debug.LogError($"Error posting user data: {error}");
        });
    }

    public static Task<ResponseHelper> GetRequest(string url)
    {
        var tcs = new TaskCompletionSource<ResponseHelper>();
        RestClient.Get(url).Then(tcs.SetResult).Catch(ex => tcs.SetException(new Exception(ex.Message)));
        return tcs.Task;
    }

    public static async Task<User> CheckUser(string email, string password)
    {
        try
        {
            var response = await GetRequest($"{GlobalVariables.FIREBASE_DATABASE_URL}users.json");
            Dictionary<string, User> users = JsonConvert.DeserializeObject<Dictionary<string, User>>(response.Text);

            foreach (var user in users.Values)
            {
                if (user.email.Trim() == email.Trim() && user.password.Trim() == password.Trim())
                {
                    return user;
                }
            }
        }
        catch (Exception error)
        {
            Debug.LogError($"Error retrieving user data: {error}");
        }

        return null;
    }
}
