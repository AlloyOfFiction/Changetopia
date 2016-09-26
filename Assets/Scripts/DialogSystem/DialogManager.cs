using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

	private DialogTree currentTree;
	public DialogBranch currentBranch;

	void Start(){
		if(currentTree == null){
			currentTree = new DialogTree();
		}
		currentBranch = currentTree.root;
		currentTree.addChoicesToBranch(currentBranch, new DialogBranch[]{
			new DialogBranch(0,"choice 1","choice 1 was selected"),
			new DialogBranch(0,"choice 2","choice 2 was selected"),
			new DialogBranch(0,"choice 3","choice 3 was selected")
		});
		Debug.Log(currentTree.printBranch(currentBranch));
	}

	public bool trySelectChoice(int choice){
		if(currentBranch.choiceBranches.Count > choice){
			currentBranch = currentTree.getFromID(currentBranch.choiceBranches[choice]);
			return true;
		}else{
			return false;
		}
	}


}
