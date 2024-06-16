using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

using System.IO;


public class DialogueVariables
{
    public Story globalVariablesStory;
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }
    public object variablestoStory { get; internal set; }

    public DialogueVariables(TextAsset Globals_LoadJSON)
    {
        // create the story
        Story globalVariablesStory = new Story(Globals_LoadJSON.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            //Debug.Log("Initialised");
        }

    }
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string Name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(Name))
        {
            variables.Remove(Name);
            variables.Add(Name, value);
        }
    }
    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
    public void UpdateVariable(string variableName, bool newValue)
    {
        // Check if the variable exists in the dictionary
        if (variables.ContainsKey(variableName))
        {
            // Update the variable in the dictionary as a boolean
           // variables[variableName] = newValue;

            Debug.Log($"Variable '{variableName}' updated to: {newValue}");
        }
        else
        {
            Debug.LogError($"Dictionary Variable '{variableName}' not found.");
        }
    }

}

