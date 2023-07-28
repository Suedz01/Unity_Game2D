using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Ator")]
    public GameObject actorPlayer;

    [Header("Criando o diálogo:")]
    public string actorName;
    public Sprite actorProfile;
    public string textSpeech;
    public AudioSource audioSpeech;
    public List<DialogueScene> dialogues = new List<DialogueScene>();
}

[System.Serializable]
public class DialogueScene
{
    public string actorNameScene;
    public Sprite actorProfileScene;
    public AudioSource audioSpeechScene;
    public Language textSpeechScene;
}

[System.Serializable]
public class Language
{
    [FormerlySerializedAs("Portuguese")] public string portuguese;
    [FormerlySerializedAs("English")] public string english;
    [FormerlySerializedAs("Spanish")] public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class DialogueBuild : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings dSettings = (DialogueSettings)target;
        DialogueScene dScene = new DialogueScene();
        dScene.actorNameScene = dSettings.actorName;
        dScene.actorProfileScene = dSettings.actorProfile;
        dScene.audioSpeechScene = dSettings.audioSpeech;

        Language language = new Language();
        language.portuguese = dSettings.textSpeech;
        dScene.textSpeechScene = language;

        if (GUILayout.Button("New Dialogue") && dSettings.textSpeech != "")
        {
            dSettings.dialogues.Add(dScene);

            dSettings.actorName = "";
            dSettings.textSpeech = "";
            dSettings.audioSpeech = null;
            dSettings.actorProfile = null;
        }
    }
}
#endif