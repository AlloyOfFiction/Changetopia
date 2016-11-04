using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

namespace UnityEditor{
	[InitializeOnLoadAttribute]
	public class Autosave {

		public static bool isEnabled = true;
		public static bool saveActiveScene = false; // If we should save the current scene file, as well as make autosave backup copies
		public static double saveTimeIntervalsSeconds = 600; // How long should we wait?
		public static int autoSaveLimit = 3; // How many autosaves at a time? Limited to 10 for space reasons, although this can be increased in AutosaveWindow

		private static double nextSaveTime;
		private static int currentAutoSave = 1;

		static Autosave(){
			loadPrefs(); // Load previous settings for Autosave system
			EditorApplication.update += OnUpdate; // Hook OnUpdate to the Editor update delegate
			SceneView.onSceneGUIDelegate += OnScene; // Hook OnScene to the Sceneview Update delegate
			nextSaveTime = EditorApplication.timeSinceStartup+saveTimeIntervalsSeconds; //set next wait time to the current time + the wait time
		}

		public static void enableAutoSaves(bool b){
			isEnabled = b;
			if(b)
				nextSaveTime = EditorApplication.timeSinceStartup+saveTimeIntervalsSeconds;
		}

		public static void changeAutoSaveInterval(double interval){
			saveTimeIntervalsSeconds = interval;
			nextSaveTime = EditorApplication.timeSinceStartup+saveTimeIntervalsSeconds;
		}

		static void OnUpdate () {
			if(isEnabled){
				double currentTime = EditorApplication.timeSinceStartup; //Grab the time
				if(currentTime > nextSaveTime){ //if the time is more than our projected next save time, so it's time to save
					string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/")); //Split up path of the active scene by "/"
					string sceneName = path[path.Length-1]; //Get the scene name
					string oldPath = string.Join("/",path); //Store the old path for later

					if(autoSaveLimit < 2){// Check if we want mutiple autosaves, this is mainly for naming conventions
						path[path.Length-1] = "Autosave_" + path[path.Length-1];
					}else{
						path[path.Length-1] = "Autosave_"+ currentAutoSave + "_" + path[path.Length-1];
						currentAutoSave++;
						if(currentAutoSave > autoSaveLimit){
							currentAutoSave = 1;
						}
					}
					string modPath = string.Join("/",path);
					if(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(),modPath)){// Save the scene under the new path, giving us a backup copy
						Debug.Log("Autosaved "+sceneName+" in "+(EditorApplication.timeSinceStartup-currentTime));
						EditorSceneManager.OpenScene(oldPath); //Open up our orignal scene
					}else{
						Debug.LogError("Autosave failed!");
					}

					modPath += "bak";
					if(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(),modPath)){// Save the scene under a .unitybak file, giving us a ignored local copy so github doesn't mess with it
						EditorSceneManager.OpenScene(oldPath);
					}else{
						Debug.LogError("Autosave failed!");
					}

					if(saveActiveScene){
						if(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene())){ //Save active scene if we asked for that

						}else{
							Debug.LogError("Autosave failed!");
						}
					}
					nextSaveTime = currentTime + saveTimeIntervalsSeconds;
				}
			}
		}

		public void OnDestroy(){
			EditorApplication.update -= OnUpdate;
		}

		public static void loadPrefs(){
			if(EditorPrefs.HasKey("AutosaveEnabled")){
				isEnabled = EditorPrefs.GetBool("AutosaveEnabled");
			}
			if(EditorPrefs.HasKey("AutosaveActiveScene")){
				saveActiveScene = EditorPrefs.GetBool("AutosaveActiveScene");
			}
			if(EditorPrefs.HasKey("AutosaveInterval")){
				saveTimeIntervalsSeconds = (double)EditorPrefs.GetInt("AutosaveInterval");
			}
			if(EditorPrefs.HasKey("AutosaveCycles")){
				autoSaveLimit = EditorPrefs.GetInt("AutosaveCycles");
			}
		}

		public static void savePrefs(){
			EditorPrefs.SetBool("AutosaveEnabled",isEnabled);
			EditorPrefs.SetBool("AutosaveActiveScene",saveActiveScene);
			EditorPrefs.SetInt("AutosaveInterval",(int)saveTimeIntervalsSeconds);
			EditorPrefs.SetInt("AutosaveCycles",autoSaveLimit);
		}

		static void OnScene(SceneView sceneView){
			Handles.BeginGUI();
			if(isEnabled)
				GUILayout.Label("Autosave Enabled"); //Small notification to say we're autosaving
			Handles.EndGUI();//lo
		}

	}
}
