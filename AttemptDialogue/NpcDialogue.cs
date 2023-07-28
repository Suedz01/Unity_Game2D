using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    //Preparar as infos para mandar
    //Sentenças precisam estar todas varridas em string[]
    public CreateDialogue createDialogue;
    public LayerMask layerMask;
    [SerializeField] private bool playerHit;
    
    private List<string> _sentences = new List<string>();
    private List<Sprite> _sceneProfile = new List<Sprite>();
    private List<string> _sceneName = new List<string>();
    
    public void Start()
    {
        LoadingSpeech();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            ControlDialogue.CallCd.StartSpeech(_sentences.ToArray(), _sceneProfile.ToArray(), _sceneName.ToArray());
        }
    }

    public void FixedUpdate()
    {
        RadiusDialogue();
    }

    public void LoadingSpeech()
    {
        for (int index = 0; index < createDialogue.createDialogues.Count; index++)
        {
            switch (ControlDialogue.CallCd.enumChoice)
            {
                case ControlDialogue.Idiom.Portuguese:
                    _sentences.Add(createDialogue.createDialogues[index].sceneSpeech.portuguese);
                    break;
                case ControlDialogue.Idiom.English:
                    _sentences.Add(createDialogue.createDialogues[index].sceneSpeech.english);
                    break;
                case ControlDialogue.Idiom.Spanish:
                    _sentences.Add(createDialogue.createDialogues[index].sceneSpeech.spanish);
                    break;
            }
            _sceneName.Add(createDialogue.createDialogues[index].sceneName);
            _sceneProfile.Add(createDialogue.createDialogues[index].sceneProfile);
        }
    }
    
    //preciso carregar as variaveis para passar para o ControlDialogue
    public void RadiusDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,2f,layerMask);
        if (hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
            ControlDialogue.CallCd.EndChat();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position,2f);
    }
}
