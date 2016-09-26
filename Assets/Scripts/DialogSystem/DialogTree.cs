using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DialogTree {

	public DialogBranch root;
	public SerializableDictionary<int,DialogBranch> branches = new SerializableDictionary<int,DialogBranch>();

	private int currentID = 0;

	public DialogTree(string startDialog = "rootDialog"){
		root = registerBranch("rootChoice",startDialog);
	}

	public DialogBranch registerBranch(string choiceText, string dialogText, IEnumerable<DialogBranch> choices = null, IEnumerable<DialogBranch.TriggerFunctions> funcs = null){
		DialogBranch tempBranch = new DialogBranch(currentID, choiceText, dialogText, choices, funcs);
		branches.Add(currentID, tempBranch);
		currentID++;
		return tempBranch;
	}

	public DialogBranch registerBranch(DialogBranch db){
		db.branchID = currentID;
		branches.Add(currentID, db);
		currentID++;
		return db;
	}

	public IEnumerable<DialogBranch> registerBranches(IEnumerable<DialogBranch> db){
		foreach(DialogBranch choice in db){
			choice.branchID = currentID;
			branches.Add(currentID, choice);
			currentID++;
		}
		return db;
	}

	public void addChoiceToBranch(DialogBranch branch, DialogBranch choice){
		branch.addChoice(registerBranch(choice).branchID);
	}

	public void addChoiceToBranch(int branch, DialogBranch choice){
		DialogBranch db = getFromID(branch);
		if(db != null){
			db.addChoice(registerBranch(choice).branchID);
		}
	}

	public void addChoicesToBranch(DialogBranch branch, IEnumerable<DialogBranch> choices){
		foreach(DialogBranch choice in choices){
			branch.addChoice(registerBranch(choice).branchID);
		}
	}

	public void addChoicesToBranch(int branch, IEnumerable<DialogBranch> choices){
		DialogBranch db = getFromID(branch);
		if(db != null){
			foreach(DialogBranch choice in choices){
				db.addChoice(registerBranch(choice).branchID);
			}
		}
	}



	public DialogBranch getFromID(int id){
		if(branches.ContainsKey(id)){
			return branches[id];
		}else{
			return null;
		}
	}

	public string printBranch(DialogBranch db){
		string dialog = "Choice Text: "+db.choiceText+"\n"+"Dialog Text: "+db.dialogText+"\n"+"Choices: "+"\n";
		foreach(int choices in db.choiceBranches){
			dialog += getFromID(choices).choiceText + "\n";
		}
		return dialog;
	}

	/*public DialogBranch searchByChoiceText(string choiceText){
		List<DialogBranch> searchNodes = new List<DialogBranch>();
		searchNodes.AddRange(getFromID(root.choiceBranches));
		while(searchNodes.Count > 0){
			DialogBranch node = searchNodes[0];
			if(node.choiceText == choiceText){
				return node;
			}
			searchNodes.AddRange(node.choiceBranches);
			searchNodes.Remove(node);
		}
		return null;
	}*/

}
