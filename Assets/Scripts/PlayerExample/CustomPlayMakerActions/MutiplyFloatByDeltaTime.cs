using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Mutiplies float value by Time.deltaTime or Time.fixedDeltaTime")]
	public class MutiplyFloatByDeltaTime : FsmStateAction {

		public enum TimeDelta{
			DeltaTime,
			FixedDeltaTime
		}

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatValue;
		public TimeDelta timeDelta;
		public bool everyFrame;

		public override void Reset()
		{
			floatValue = null;
			timeDelta = TimeDelta.DeltaTime;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			
			DoMutiplication();

			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoMutiplication();
		}

		public override void OnFixedUpdate ()
		{
			DoMutiplication(true);
		}

		void DoMutiplication(bool fixedUpdateCall = false)
		{
			if(timeDelta == null) return;
			if(floatValue == null) return;
			if(timeDelta == TimeDelta.DeltaTime && !fixedUpdateCall){
				floatValue.Value *= Time.deltaTime;
			}else if(timeDelta == TimeDelta.FixedDeltaTime && fixedUpdateCall){
				floatValue.Value *= Time.fixedDeltaTime;
			}
		}
	}
}