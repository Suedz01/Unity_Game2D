using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlDialogue : MonoBehaviour
{
    public enum Idiom
    {
        Portuguese,
        English,
        Spanish
    }
    public Idiom enumChoice;
    public GameObject dialogueObj;
    public Text actorName;
    public Text speechActor;
    public Image actorProfile;

    
    private bool _isTalking;
    private string[] _sentences;
    private Sprite[] _profiles;
    private string[] _name;

    private int _index = 0;
    public float typingSpeed = 0.1f;

    public static ControlDialogue CallCd;
    
    void Awake()
    {
        CallCd = this;
    }
    public void StartSpeech(string[] txt, Sprite[] sprites, string[] actorNames)
    {
        if (!_isTalking)
        {
            _isTalking = true;

            dialogueObj.SetActive(true);
            
            _sentences = txt;
            _profiles = sprites;
            _name = actorNames;

            LoadingInfos();
        }
    }
    IEnumerator TypeText()
    {
        foreach (char letter in _sentences[_index])
        {
            speechActor.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextPhrase()
    {
        if (speechActor.text == _sentences[_index])
        {
            if (_index < _sentences.Length - 1)
            {
                _index++;
                
                speechActor.text = "";
                actorName.text = "";

                LoadingInfos();
            }
            else
            {
                EndChat();
            }
        }
    }

    public void LoadingInfos()
    {
        actorName.text = _name[_index];
        actorProfile.sprite = _profiles[_index];
        StartCoroutine(TypeText());
    }

    public void EndChat()
    {
        _index = 0;
        speechActor.text = "";
        actorName.text = "";
        _isTalking = false;
        dialogueObj.SetActive(false);
    }
}
