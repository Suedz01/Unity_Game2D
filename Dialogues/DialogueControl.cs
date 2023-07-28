using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    //controlar as infos que serão passadas para o UI
    //Corrotina
    //isShowing interligado com script do NPC
    
    [System.Serializable]
    public enum Idiom   //adicionando as traduções
    {
        Pt,
        Eng,
        Spa
    }

    public Idiom language;  //acessar enum
    public GameObject dialogueObj;
    public Text actorName;
    public Text speechText;
    public Image actorProfile;
    public AudioSource actorVoice;

    [Header("Settings")]
    private bool _isShowing;

    public bool IsShowing
    {
        get { return _isShowing; }
        set { _isShowing = value; }
    }
    
    private string[] _sentences;
    private int _index;
    public float typeSpeed;

    public static DialogueControl Instance;

    void Awake()
    {
        Instance = this;
    }
    
    //Pular para a próxima frase
    public void NextSentence()
    {
        if (speechText.text == _sentences[_index])
        {
            if (_index < _sentences.Length - 1)
            {
                _index++;
                speechText.text = ""; //evita erro
                StartCoroutine(TypeText()); //Digita a nova frase após a alteração do index
            }
            else //quando terminam os textos
            {
                EndChat();
            }
        }
    }
    
    //Chamar a fala
    //O Script do NPC irá alimentar sentences
    public void Speech(string[] txt)
    {
        if (!_isShowing)
        {
            dialogueObj.SetActive(true);
            _sentences = txt;
            StartCoroutine(TypeText());
            NextSentence();
            _isShowing = true;
        }
    }

    public void EndChat()
    {
        _index = 0;
        speechText.text = "";
        dialogueObj.SetActive(false);
        _sentences = null; //é necessário esvaziar as falar, vai que você fala com outro NPC (hein?)
        _isShowing = false;
    }
    IEnumerator TypeText()
    {
        foreach (char letter in _sentences[_index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
