using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Security.Policy;

public class CSVLoader : MonoBehaviour {
	static public List<string> fieldList;
    public SpatioAsset assetPrefab;
    public SpatioButton buttonPrefab;
    public SpatioDocuments DocumentsPanel;
    public Canvas UI;
	// Use this for initialization
	IEnumerator Start () {

        /*WWW site = new WWW("https://drive.google.com/uc?export=download&id=0B2qlqqSX9ZDgbGhUdUJxMV9VVE0");
		yield return site;
        */
        //Using a local source
        TextAsset CSVasset;
        CSVasset = Resources.Load("list") as TextAsset;
        
        string CSVText;
        CSVText = CSVasset.text;
        fgCSVReader.LoadFromString(CSVText, LoadData);
        yield return CSVasset;

    }

	void LoadData (int line_number, List<string> line)
	{
		if (line_number == 0)
		{
            fieldList = new List<string>(line);
		
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
	// Update is called once per frame
	void Update () {
	
	}
}
