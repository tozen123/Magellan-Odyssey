using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using UnityEditor;

public class User
{
    public string name;
    public string email;
    public string password;
    public string userID;

    public int adventure_points = 0;
    public int academic_points = 0;
    public string academic_rank = "Rookie";
    public string chapter_progress = "NONE";

    public User() { }

    public User(string name, string email, string userID, string password, 
        int adventure_points = 0, 
        int academic_points = 0, 
        string academic_rank = "Rookie",
        string chapter_progress = "NONE"
        )
    {
        this.name = name;
        this.email = email;
        this.userID = userID;
        this.password = password;
        this.adventure_points = adventure_points;
        this.academic_points = academic_points;
        this.academic_rank = academic_rank;
        this.chapter_progress = chapter_progress;
    }

    public void WriteUserToDatabase()
    {
    

        RestClient.Put($"{GlobalVariables.FIREBASE_DATABASE_URL}users/{userID}.json", this).Then(response =>
        {
            Debug.Log("User data posted successfully!");

            //Test
           

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
        Debug.Log("CheckUser - email: " + email);
        Debug.Log("CheckUser - password: " + password);
        try
        {
            var response = await GetRequest($"{GlobalVariables.FIREBASE_DATABASE_URL}users.json");
            Dictionary<string, User> users = JsonConvert.DeserializeObject<Dictionary<string, User>>(response.Text);
            Debug.Log(response.Text);

            foreach (var user in users.Values)
            {
                string trimmedEmail = RemoveInvisibleChars(user.email.Trim());
                string trimmedPassword = RemoveInvisibleChars(user.password.Trim());
              
                if (trimmedEmail.Equals(email))
                {
                    if (trimmedPassword.Equals(password))
                    {
                        return user;

                    }
                }
                else
                {
                    Debug.Log("Credentials do not match any entries");
                }
            }
        }
        catch (Exception error)
        {
            Debug.LogError($"Error retrieving user data: {error}");
        }

        return null;
    }

    private static string RemoveInvisibleChars(string input)
    {
        return new string(input.Where(c => !char.IsControl(c) && !char.IsWhiteSpace(c)).ToArray());
    }

}
