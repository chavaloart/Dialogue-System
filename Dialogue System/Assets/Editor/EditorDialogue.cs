using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomEditor(typeof(DialogueTrigger))]
public class EditorDialogue : Editor
{
    public override void OnInspectorGUI()
    {
        DialogueTrigger dialogueT = (DialogueTrigger)target;
        Dialogue dialogue = dialogueT.dialogue;



        //Dialogue Lines
        GUILayout.Label("How many dialogue lines will there be:");


        //This is here because of an error message that kept showing up.

        //The error happened because the events array had a set length, but for one frame
        //the arrays' serialized property had a different length, causing it to go out
        //of bounds.
        int savedDLines = dialogueT.dialogueLines;

        SerializedProperty dLines = serializedObject.FindProperty("dialogueLines");
        EditorGUILayout.PropertyField(dLines, new GUIContent("Dialogue Lines:"));
        serializedObject.ApplyModifiedProperties();

        //This is also for the error message that kept showing up.
        if(savedDLines > dialogueT.dialogueLines)
        {
            savedDLines = dialogueT.dialogueLines;
        }

        ChangeArraySizes(dialogueT.dialogueLines, dialogue);

        //Events are set here because they are changed in ChangeArraySizes.
        UnityEvent[] events = dialogue.events;

        GUILayout.Space(30);


        
        //Maximum Dialogue Characters
        GUILayout.Label("The maximum amount of characters each dialogue line can contain.");

        SerializedProperty maxChar = serializedObject.FindProperty("maxChars");
        EditorGUILayout.PropertyField(maxChar, new GUIContent("Maximum characters:"));
        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(30);


        for (int i = 0; i < savedDLines; i++)
        {            
            GUILayout.Label("Dialogue " + i + ":");

            //Displaying dialogue names
            
            GUILayout.Space(10);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogue").
                FindPropertyRelative("names").GetArrayElementAtIndex(i), 
                new GUIContent("Name " + i + ": "));
            serializedObject.ApplyModifiedProperties();

            GUILayout.Space(10);



            //Displaying dialogue sentences

            GUILayout.Label("Dialogue Line " + i + ": ");
            dialogue.sentences[i] = GUILayout.TextField(dialogue.sentences[i],
                dialogueT.maxChars, GUILayout.Height(100));

            GUILayout.Space(10);


            //Displaying dialogue events
            GUILayout.Label("Events - Use if you want to run a function at the " +
                "\nbeginning of this dialogue line:");

            EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogue").
                FindPropertyRelative("events").GetArrayElementAtIndex(i),
                new GUIContent("Event " + i + ": "));
            serializedObject.ApplyModifiedProperties();
            GUILayout.Space(30);
        }

    }

    void ChangeArraySizes(int length, Dialogue d)
    {    
        //Saving temporary copies
        string[] sNames = d.names;
        string[] sSentences = d.sentences;
        UnityEvent[] sEvents = d.events;

        //Making new arrays with the correct size
        d.names = new string[length];
        d.sentences = new string[length];
        d.events = new UnityEvent[length];


        //Adding back the copied values
        for (int i = 0; i < length; i++)
        {
            //We simply assign the old values...
            if (i < sNames.Length)
            {
                d.names[i] = sNames[i];
            } else if(sNames.Length > 0)
            {
                //  ...unless the new array is bigger than the old one.

                //  If this is the case, I will assign the last value of
                //  the old array, as this is what happens when you are
                //  editing arrays or lists in the Unity inspector.

                d.names[i] = sNames[sNames.Length - 1];

            } else
            {
                d.names[i] = "";
            }


            if (i < sSentences.Length)
            {
                d.sentences[i] = sSentences[i];
            }
            else if(sSentences.Length > 0)
            {
                d.sentences[i] = sSentences[sSentences.Length - 1];
            } else
            {
                d.sentences[i] = "";
            }


            if (i < sEvents.Length)
            {
                d.events[i] = sEvents[i];
            }
            else if(sEvents.Length > 0)
            {
                d.events[i] = sEvents[sEvents.Length - 1];
            } else
            {
                d.events[i] = null;
            }
        }
    }
}
