using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class SpatioImporter : AssetPostprocessor
{
    void OnPostprocessModel(GameObject g)
    {
    }
    void OnPostprocessGameObjectWithUserProperties(GameObject go, string[] propNames, System.Object[] values)
    {
		int index;
        Debug.Log("Properties: ");
        foreach (string p in propNames)
        {
            //Debug.Log(p);
        }
        if (ArrayUtility.Contains(propNames, "SpatioModel"))
        {
            GenerateSpatioModel(go, propNames, values);
        }
		if ((index = ArrayUtility.FindIndex (propNames, s => s.Equals("UDP3DSMAX"))) >= 0) {
			ParseSpatioModel(go, (string)(values[index]));
		}
		/*
		else if((maxIndex = Array.FindIndex(propNames, s => s.Equals("UDP3DSMAX"))) >= 0)
        {
			System.String propList = (System.String)values [maxIndex];
			string[] subProps = propList.Split("&cr;&lf;", System.StringSplitOptions.RemoveEmptyEntries);
			string[] props = new string[subProps.Length];
			System.Object[] vals = new System.Object[subProps.Length];
			Debug.Log (propList + "\n");
			Debug.Log (subProps + "\n");

            for (int i = 0; i < subProps.Length; i++)
            {
				//subProps[i].Substring
            }
        }*/
        GenerateSpatioModel(go, propNames, values);
    }

	void ParseSpatioModel(GameObject go, string data)
	{
 
		SpatioModel sm = null;
		string[] dataset;
		Debug.Log ("Parsing: " + data);
		string[] properties = data.Split ('\n');
		foreach (string p in properties) {
			Debug.Log ("Property: " + p);
			if (p.StartsWith ("RESTOMODEL")) {
				sm = go.AddComponent<SpatioModel> ();
				Collider collider = go.AddComponent<MeshCollider> ();
			} else if (p.StartsWith ("START")) {
				if (sm == null) {
					Debug.Log ("rm not found!");
					continue;
				}
				dataset = p.Split (' ');
				sm.start = new Date ();
				sm.start.Month = int.Parse (dataset [dataset.Length - 1].Substring (0, 2));
				sm.start.Day = int.Parse (dataset [dataset.Length - 1].Substring (2, 2));
				sm.start.Year = int.Parse (dataset [dataset.Length - 1].Substring (4, 4));
				Debug.Log ("Start Date: " + sm.start.ToString ());

			} else if (p.StartsWith ("END")) {
				if (sm == null) {
					Debug.Log ("rm not found!");
					continue;
				}
				dataset = p.Split (' ');
				sm.end = new Date ();
				sm.end.Month = int.Parse (dataset [dataset.Length - 1].Substring (0, 2));
				sm.end.Day = int.Parse (dataset [dataset.Length - 1].Substring (2, 2));
				sm.end.Year = int.Parse (dataset [dataset.Length - 1].Substring (4, 4));
				Debug.Log ("End Date: " + sm.end.ToString ());

			} else if (p.StartsWith ("NAME")) {
				if (sm == null) {
					Debug.Log ("rm not found!");
					continue;
				}
				sm.name = p.Substring (7);

				Debug.Log ("Name: " + sm.name);

			} else if (p.StartsWith ("DESCRIPTION")) {
				if (sm == null) {
					Debug.Log ("rm not found!");
					continue;
				}
				sm.Description = p.Substring (13);
				Debug.Log ("Description: " + sm.Description);

			} else if (p.StartsWith("TAG"))
			{
			    string t;
			    t = p.Substring(6);
			    sm.tags = new List<string>(t.Split(','));
			    Debug.Log("Tags: " + sm.tags.ToString());
			}
			else if (p.StartsWith("BLOCK"))
			{
			    sm.block = p.Substring(8);
			    Debug.Log("Block: '" + sm.block + "'");
		    }else if(p == null){
				Debug.Log ("Null Field");
			}else{
				Debug.Log ("First Value: " + p [0]);
			}
		}
	}
    void GenerateSpatioModel(GameObject go, string [] propNames, System.Object[] values)
    {
		/*
        //If it has SpatioModel data, load it up as a spatiomodel 
        if (propNames.Contains("SpatioModel"))
        {
            SpatioModel rm = go.AddComponent<SpatioModel>();
            Debug.Log("Values: " + values);
            for (int i = 0; i < propNames.Length; i++)
            {

                Debug.Log("(" + propNames[i] + ") Values[" + i + "] : " + values[i]);
                System.Object o = values[i];
                switch (propNames[i])
                {
                    case "Anchor":
                        Vector4 anchor = (Vector4)values[i];
                        rm.Anchor = new Vector3(anchor.x, anchor.y, anchor.z);
                        break;
					case "Tag":
						rm.Tag = (string)values [i];
						break;
                    case "Description":
                        rm.Description = (string)values[i];
                        break;
                    case "Date":
                        rm.start.Day = (int)values[i];
                        break;
                    case "Month":
                        rm.start.Month = (int)values[i];
                        break;
                    case "Year":
                        rm.start.Year = (int)values[i];
                        break;
                }
            }
        }  
		*/
    }
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
       
        foreach (var str in importedAssets)
        {
            Debug.Log("Reimported Asset: " + str);
        }
        foreach (var str in deletedAssets)
        {
            Debug.Log("Deleted Asset: " + str);
        }

        for (var i = 0; i < movedAssets.Length; i++)
        {
            Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        }
    }
}
