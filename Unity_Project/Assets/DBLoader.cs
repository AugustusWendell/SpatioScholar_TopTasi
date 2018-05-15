using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Security.Policy;
using SimpleJSON;

public class DBLoader : MonoBehaviour {
	static public List<string> fieldList;
    public string dbSource;
    DateTime lastUpdate;
    float updateTimeRemaining;
    public SpatioAsset assetPrefab;
    public SpatioButton buttonPrefab;
    public SpatioDocuments DocumentsPanel;
    public NotesPanel notesPanel;
    public GameObject addConfirmation;
    public ViewNotesPanel viewNotes;
    public Canvas UI;
	// Use this for initialization
	void Start () {
        StartCoroutine(RetrieveNotes());
    }

    void LoadData (int line_number, List<string> line)
	{
		if (line_number == 0)
		{
            fieldList = new List<string>(line);
            foreach(string str in fieldList)
                Debug.Log("FST-" + str);
		
		}else
		{
		    Dictionary<string, string> csvData = new Dictionary<string, string>();
		    for (int i = 0; i < fieldList.Count; i++)
		    {
		        if (fieldList[i] == "")
		            continue;
		        if (i >= line.Count)
		        {
                    //Debug.Log("Adding <" + fieldList[i] + ", (Empty)>");
                    csvData.Add(fieldList[i], "");
		        }else{
		            //Debug.Log("Adding <" + fieldList[i] + ", " + line[i] + ">");
		            csvData.Add(fieldList[i], line[i]);
		        }
		    }
                
            //Create a spatioasset and spatiobutton
		    SpatioAsset asset = (SpatioAsset)Instantiate(assetPrefab, Vector3.zero, Quaternion.identity);
		    SpatioButton button = (SpatioButton) Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
		    button.asset = asset.gameObject;
            RectTransform btnRect = button.GetComponent<RectTransform>();
            btnRect.anchorMin = new Vector2(0.5f, 1.0f);
            btnRect.anchorMax = new Vector2(0.5f, 1.0f);
            RectTransform assetRect = asset.GetComponent<RectTransform>();
            assetRect.offsetMax = new Vector2(Screen.height / 2, Screen.width / 2);


            DocumentsPanel.AddButton(button);
            Debug.Log(btnRect.offsetMin);
            Debug.Log(btnRect.offsetMax);
            asset.transform.SetParent(UI.transform, false);
            assetRect.offsetMin = new Vector2((Screen.width - 381) / 2, -(Screen.height + 275) / 2);
            assetRect.offsetMax = new Vector2((Screen.width + 381) / 2, -(Screen.height - 275) / 2);
            button.transform.SetParent(DocumentsPanel.transform, false);
            //Associate them with the UI
            StartCoroutine(fetchSource(asset, button, csvData["Host"]));
		    asset.SetAssetFields(csvData);
            //asset.gameObject.SetActive(false);
            
		}
	}

    IEnumerator fetchSource(SpatioAsset asset, SpatioButton button, string source)
    {
        string prestring = "https://drive.google.com/open?id=";
        Texture2D t = new Texture2D(1, 1);
        //Convert address
        if (source.StartsWith(prestring))
            source = "https://drive.google.com/uc?export=download&id=" + source.Substring(prestring.Length);
        Debug.Log(source);
        //source = "http://www.cartoonthrills.com/images/headerlogo.jpg";
        //WWW site = new WWW(source);
        t = Resources.Load<Texture2D>(source);
        yield return t;
       
        //site.LoadImageIntoTexture(t);
        Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        //Debug.Log("Texture data: " + site.text);
        
        asset.image.sprite = s;
        button.image.sprite = s;
        asset.gameObject.SetActive(false);

    }
    public IEnumerator UpdateTime()
    {
        WWWForm form = new WWWForm();
        form.AddField("operation", "status");
        WWW site = new WWW(dbSource, form);
        yield return site;
        JSONNode node = JSON.Parse(site.text);
        Debug.Log(node["data"]["MAX(posttime)"]);
        DateTime mostRecent = DateTime.ParseExact(node["data"]["MAX(posttime)"], "yyyy-MM-dd HH:mm:ss", null);
        if (mostRecent > lastUpdate)
        {
            yield return RetrieveNotes();
            lastUpdate = mostRecent;
        }
    }
    public IEnumerator AddNote(string first, string last, string brief, string full, string URL, System.DateTime date, Vector3 location)
    {
        WWWForm form = new WWWForm();
        form.AddField("operation", "insert");
        form.AddField("first", first);
        form.AddField("last", last);
        form.AddField("brief", brief);
        form.AddField("full", full);
        form.AddField("url", URL);
        form.AddField("reftime", date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        form.AddField("posttime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        form.AddField("x", location.x.ToString());
        form.AddField("y", location.y.ToString());
        form.AddField("z", location.z.ToString());
        WWW site = new WWW(dbSource, form);
        yield return site;
        Debug.Log(date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        Debug.Log(location.x.ToString());
        addConfirmation.SetActive(true);
        notesPanel.Clear();

    }
    public IEnumerator RetrieveNotes()
    {
        WWWForm form = new WWWForm();
        form.AddField("operation", "select");
        WWW site = new WWW(dbSource, form);
        yield return site;
        JSONNode node = JSON.Parse(site.text);
        viewNotes.UpdateNotes(node["data"], lastUpdate);
    }
    // Update is called once per frame
    void Update () {
	    if(updateTimeRemaining > 0)
        {
            updateTimeRemaining -= Time.deltaTime;
        }
        else
        {
            updateTimeRemaining = 5;
            StartCoroutine(UpdateTime());
        }
	}
}
