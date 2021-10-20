using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if __DEBUG_AVAILABLE__

using UnityEditor;

#endif



public class GameManager : MonoBehaviour
{
    public Transform[] dialogComon;
    public Transform[] dialogCharacter;
    public Transform dialogText;

    [System.Serializable]
    public struct DialogData
    {
        public int character;
        public string text;
    };

    public DialogData[] dialogsData;

    bool showingDialog;

    TextMeshPro dialogTextC;

    int dialogIndex;
    
    KeyCode[] debugKey = { KeyCode.S, KeyCode.T, KeyCode.A, KeyCode.R, };
    int debugKeyProgress = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        showingDialog = false;
        dialogIndex = 0;

        dialogTextC = dialogText.GetComponent<TextMeshPro>();
    }
    
    
#if __DEBUG_AVAILABLE__

    void OnDrawGizmos()
    {
        if(Switches.debugMode && Switches.debugDialogs)
        {
            Handles.color = Color.white;
            Handles.Label(dialogText.position - Vector3.up * 1.0f, "Dialog Id: " + dialogIndex); 
        }
        
    }

#endif




    // Update is called once per frame
    void Update()
    {
#if __DEBUG_AVAILABLE__


        if(Switches.debugMode && Switches.debugDialogs)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                showingDialog = true;
                dialogIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                dialogIndex = (dialogIndex +1) % dialogsData.Length; // Si llega al �ltimo volver� a empezar
            }
        }
#endif

        if (showingDialog)
        {
            for(int i = 0; i < dialogComon.Length; i++){
                dialogComon[i].gameObject.SetActive(true);}
            
            for (int i = 0; i < dialogCharacter.Length; i++){
                dialogCharacter[i].gameObject.SetActive(false);}

            int character = dialogsData[dialogIndex].character;
            string text = dialogsData[dialogIndex].text;

            dialogCharacter[character].gameObject.SetActive(true);
            dialogTextC.text = text;
            

            if (Input.GetKeyDown(KeyCode.Return))
                showingDialog = false;            
        }
        else
        {
            for (int i = 0; i < dialogComon.Length; i++){
                dialogComon[i].gameObject.SetActive(false);}

            for (int i = 0; i < dialogCharacter.Length; i++){
                dialogCharacter[i].gameObject.SetActive(false);}
        }

#if __DEBUG_AVAILABLE__

        // Debug
        if(!Switches.debugMode)
        {
            if(Input.GetKeyDown(debugKey[debugKeyProgress]))
            {
                debugKeyProgress++;
                if(debugKeyProgress == debugKey.Length)
                {
                    Switches.debugMode = true;
                    Debug.Log("Debug mode on");
                }
            }
        }
#endif
    }

    public void OnTriggerDialog(int index)
    {
        showingDialog = true;
        dialogIndex = index;
    }

    public bool IsShowingDialog()
    {
        return showingDialog;
    }
}