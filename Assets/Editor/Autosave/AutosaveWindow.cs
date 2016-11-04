using UnityEngine;
using UnityEditor;

/// <summary>
/// This is a window for the AutoSave script, so we can control it
/// </summary>

public class AutosaveWindow: EditorWindow {



	int offset = 10; // GUI offset

	[MenuItem("Window/Autosave Manager")] // Register the window into unity's button hieracy
	static void Init(){
		AutosaveWindow window = (AutosaveWindow)EditorWindow.GetWindowWithRect(typeof(AutosaveWindow), new Rect(0,0,165,155)); //Make us a window and show it us
		window.titleContent = new GUIContent("Autosave");
		window.Show();
	}

	void OnGUI(){
		EditorGUI.LabelField(new Rect(offset,5,position.width-offset*2,20),"Autosave Manager");
		bool b = EditorGUI.ToggleLeft(new Rect(offset,25,position.width-offset*2,20),"Enabled",Autosave.isEnabled);
		if(b != Autosave.isEnabled){
			Autosave.enableAutoSaves(b);
			Autosave.savePrefs();
		}
		b = EditorGUI.ToggleLeft(new Rect(offset,45,position.width-offset*2,20),"Save Active Scene",Autosave.saveActiveScene);
		if(b != Autosave.saveActiveScene){
			Autosave.saveActiveScene = b;
			Autosave.savePrefs();
		}
		EditorGUI.LabelField(new Rect(offset,65,position.width-offset*2,20),"Save Interval in Seconds");
		double newSaveInterval = EditorGUI.DoubleField(new Rect(offset,85,position.width-offset*2,20),Autosave.saveTimeIntervalsSeconds);
		if(newSaveInterval != Autosave.saveTimeIntervalsSeconds){
			Autosave.changeAutoSaveInterval(newSaveInterval);
			Autosave.savePrefs();
		}
		EditorGUI.LabelField(new Rect(offset,105,position.width-offset*2,20),"Autosave Cycle Size");
		int limit = EditorGUI.IntSlider(new Rect(offset,125,position.width-offset*2,20), Autosave.autoSaveLimit,1,10);
		if(limit != Autosave.autoSaveLimit){
			Autosave.autoSaveLimit = limit;
			Autosave.savePrefs();
		}
	}

}
