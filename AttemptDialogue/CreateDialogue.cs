using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue", menuName = "new Dialogue/Dialogue Script")]
public class CreateDialogue : ScriptableObject
{
    public string createName;
    public string createSpeech;
    public Sprite createProfile;
    public List<ScriptDialogue> createDialogues = new List<ScriptDialogue>();
}
[System.Serializable]
public class ScriptDialogue
{
    public string sceneName;
    public Sprite sceneProfile;
    public Translation sceneSpeech;
}

[System.Serializable]
public class Translation
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(CreateDialogue))]
public class BuildEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CreateDialogue cDialogue = (CreateDialogue)target;
        ScriptDialogue sDialogue = new ScriptDialogue();
        sDialogue.sceneName = cDialogue.createName;
        sDialogue.sceneProfile = cDialogue.createProfile;

        Translation languages = new Translation();
        languages.english = cDialogue.createSpeech;

        sDialogue.sceneSpeech = languages;

        if (GUILayout.Button("New Speech") && cDialogue.createSpeech != "")
        {
            cDialogue.createDialogues.Add(sDialogue);
            cDialogue.createProfile = null;
            cDialogue.createName = "";
            cDialogue.createSpeech = "";
        }

    }
}
#endif