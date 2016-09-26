using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DialogBranch {

	public string choiceText;
	public string dialogText;
	public int branchID;
	public List<int> choiceBranches = new List<int>(); //issue

	public delegate void TriggerFunctions();
	TriggerFunctions triggerFuncs;

	public DialogBranch(int id,string cT, string dT, IEnumerable<DialogBranch> choices = null, IEnumerable<TriggerFunctions> functions = null) {
		if(choices == null){
			choices = new List<DialogBranch>();
		}
		if(functions == null){
			functions = new List<DialogBranch.TriggerFunctions>();
		}
		branchID = id;
		choiceText = cT;
		dialogText = dT;
		foreach(DialogBranch choice in choices){
			choiceBranches.Add(choice.branchID);
		}
		foreach(TriggerFunctions func in functions){
			triggerFuncs += func;
		}
	}

	/*public DialogBranch(int id,string cT, string dT) {
		branchID = id;
		choiceText = cT;
		dialogText = dT;
	}*/

	public void addChoice(int db){
		choiceBranches.Add(db);
	}

	public void addChoices(IEnumerable<int> db){
		foreach(int choice in db){
			choiceBranches.Add(choice);
		}
	}

	public void trigger(){
		triggerFuncs();
	}

	public void addTrigger(TriggerFunctions func){
		triggerFuncs += func;
	}

	public void removeTrigger(TriggerFunctions func){
		triggerFuncs -= func;
	}

	public override string ToString ()
	{
		string dialog = "Choice Text: "+choiceText+"\n"+"Dialog Text: "+dialogText+"\n"+"Choices: "+"\n";
		return dialog;
	}

}
