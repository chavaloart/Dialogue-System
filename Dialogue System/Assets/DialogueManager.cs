using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> names;
	private Queue<string> sentences;
    private UnityEvent[] events;

    private int dialogueIndex;

	// Use this for initialization
	void Start () {
        names = new Queue<string>();
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

        names.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }


        sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

        events = dialogue.events;

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

        string name = names.Dequeue();
        nameText.text = name;

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));

        CheckEvents();
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

    void CheckEvents()
    {
        if(events[dialogueIndex] != null)
        {
            events[dialogueIndex].Invoke();
        }

        dialogueIndex++;
    }

}
