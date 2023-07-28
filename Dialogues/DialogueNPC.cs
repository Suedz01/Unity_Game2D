using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpc : MonoBehaviour
{
    public DialogueSettings dialogues;
    public float distance;
    public LayerMask playerLayer;
    private bool _playerHit;
    private List<string> _sentences = new List<string>(); //ira armazenar localmente as listas de dialogue settings

    public void FixedUpdate()
    {
        ShowDialogue();
    }

    private void Start()
    {
        GetNpCinfo();
    }

    public void Update()
    {
        if ( Input.GetKeyDown(KeyCode.E) && _playerHit )
        {
            DialogueControl.Instance.Speech(_sentences.ToArray()); //convertendo list para array
        }
    }

    //Caçando os textos do DialogueSettings
    void GetNpCinfo()
    {
        //vai rodar o tanto de fala em 'dialogues' que tiver
        for (int i = 0; i < dialogues.dialogues.Count; i++)
        {
            switch (DialogueControl.Instance.language)
            {
                case DialogueControl.Idiom.Pt:
                    _sentences.Add(dialogues.dialogues[i].textSpeechScene.portuguese);
                    break;
                case DialogueControl.Idiom.Eng:
                    _sentences.Add(dialogues.dialogues[i].textSpeechScene.english);
                    break;
                case DialogueControl.Idiom.Spa:
                    _sentences.Add(dialogues.dialogues[i].textSpeechScene.spanish);
                    break;
            }
        }
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,distance,playerLayer);
        if (hit != null)
        {
            _playerHit = true;
        }
        else
        {
            _playerHit = false;
            DialogueControl.Instance.EndChat();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position,distance);
    }
}
