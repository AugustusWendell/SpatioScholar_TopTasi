using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class CreateSpatioWizard : ScriptableWizard
{
    public string nameField = "";
    public string folderField = "";

    [MenuItem("GameObject/SpatioSet", false, 10)]
    static void RetroSet()
    {
        DisplayWizard<CreateSpatioWizard>("Create SpatioSet", "Create");
    }

    void OnWizardCreate()
    {
        GameObject model;
        GameObject go = Instantiate(Resources.Load("SpatioSet")) as GameObject;
        SpatioSet rs = go.GetComponent<SpatioSet>();
        string[] files = Directory.GetFiles(folderField);
        if (files != null && files.Length > 0)
        {
            foreach(string filename in files)
            {
                //Check to see if it's an fbx file
                if (!filename.EndsWith(".fbx"))
                {
                    continue;
                }
            
                //Construct the full file path
                string filepath = folderField;
                filepath = filename;
                //Create a new SpatioModel and begin loading and setting up the components
                model = new GameObject();
                string model_year = "tst";
                SpatioModel sm = model.AddComponent<SpatioModel>(); //Add a SpatioModel component
                MeshFilter meshFilter = model.AddComponent<MeshFilter>(); //Add a MeshFilter Component
                MeshRenderer meshRenderer = model.AddComponent<MeshRenderer>(); //Add a MeshRenderer Component
                //Material mat = new Material(Shader.Find("Legacy/Transparent/Specular")); //Create a Material
                //AssetDatabase.CreateAsset(mat, "Assets/Materials/" + mat.name + ".mat"); //Make the Material into an asset
                //meshRenderer.material = mat; //Assign the Material to the meshRenderer
                meshFilter.mesh = AssetDatabase.LoadAssetAtPath(filepath, typeof(Mesh)) as Mesh; //Load the mesh from the fbx file
                
                //GameObject pf = PrefabUtility.CreatePrefab("Assets/SpatioModels/" + nameField + "/" + model_year + ".prefab", model); 
                model.transform.parent = go.transform;
            }
        }
        go.name = nameField;
    }

    void OnWizardUpdate()
    {
        if (nameField == "")
        {
            helpString = "";
            errorString = "Please enter a name for the SpatioSet";
            isValid = false;
        }
        else if (folderField == "")
        {
            helpString = "";
            errorString = "Please select a folder to load models from";
            isValid = false;
        }
        else if (!Directory.Exists(folderField))
        {
            helpString = "";
            errorString = "The folder path selected is not valid";
            isValid = false;
        }
        else
        {
            helpString = "All fields completed, Ready to create";
            errorString = "";
            isValid = true;
        }
    }
}
