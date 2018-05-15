using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Authenticator : MonoBehaviour {
    public string username;
    public string password;
    public string dbSource;
    public InputField usernameInput;
    public InputField passwordInput;

	// Use this for initialization
	void Start () {
		
	}
	
    public IEnumerator Authenticate()
    {
        WWWForm form = new WWWForm();
        form.AddField("operation", "authenticate");
        form.AddField("username", username);
        form.AddField("password", password);
        WWW site = new WWW(dbSource, form);
        yield return site;
        JSONNode node = JSON.Parse(site.text);
        if (node["status"] == "authenticated")
            SceneManager.LoadSceneAsync("example", LoadSceneMode.Single);
        else if (node["status"] == "denied")
            Debug.Log("Denied.");
        else
            Debug.Log("Error.");

    }

    public void OnClick()
    {
        username = usernameInput.text;
        password = passwordInput.text;
        StartCoroutine(Authenticate());
    }
    // Update is called once per frame
    void Update () {
    }
}
