using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VisualNovel : MonoBehaviour
{
    [SerializeField]
    private Peaker[] peakers;
    [SerializeField]
    private Dialogue[] dialogs;
    [SerializeField]
    private bool isAutoStart = true;
    private bool isFirst = true;

    private float typingSpeed = 0.05f;
    private bool isTypingEffect = false;

    private int currentDialogIndex = -1;
    private int currentSpeakerIndex = 0;

    private void Setup()
    {
        for(int i = 0; i<peakers.Length; ++i)
        {
            SetActiveObjects(peakers[i], false);
            peakers[i].spriteRenderer.gameObject.SetActive(true);
        }
    }

    public bool UpdateDialog()
    {
        if(isFirst == true)
        {
            Setup();
            if (isAutoStart) SetNextDialog();

            isFirst = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect == true)
            {
                isTypingEffect = false;

                StopCoroutine("OnTypingText");
                peakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
                peakers[currentDialogIndex].objectArrow.SetActive(true);

                return false;
            }
            if(dialogs.Length>currentDialogIndex+1)
            {
                SetNextDialog();
            }
            else
            {
                for(int i = 0; i<peakers.Length; ++i)
                {
                    SetActiveObjects(peakers[i], false);
                    peakers[i].spriteRenderer.gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    private void SetNextDialog()
    {
        SetActiveObjects(peakers[currentSpeakerIndex], false);

        currentDialogIndex++;

        currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

        SetActiveObjects(peakers[currentSpeakerIndex], true);

        peakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;

        peakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;

        StartCoroutine("OnTypingText");

    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;

        while (index <= dialogs[currentDialogIndex].dialogue.Length)
        {
            peakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
        
        }
        isTypingEffect = false;

        peakers[currentSpeakerIndex].objectArrow.SetActive(false);
    }

    private void SetActiveObjects(Peaker peaker,bool visable)
    {
        peaker.textName.gameObject.SetActive(visable);
        peaker.textDialogue.gameObject.SetActive(visable);

        peaker.objectArrow.SetActive(false);

        Color color = peaker.spriteRenderer.color;
        color.a = visable == true ? 1 : 0.2f;
        peaker.spriteRenderer.color = color;
    }

  


  [System.Serializable]
  public struct Peaker
    {
        public SpriteRenderer spriteRenderer;
        public Image imageDialog;
        public Text textName;
        public Text textDialogue;
        public GameObject objectArrow;
    }
  [System.Serializable]
  public struct Dialogue
    {
        public int speakerIndex;
        public string name;
        [TextArea(3, 5)]
        public string dialogue;
    }
   
  
 

}
