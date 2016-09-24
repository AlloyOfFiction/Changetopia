using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets the Angular Velocity of a Game Object and stores it in a Float Variable. NOTE: The Game Object must have a Rigid Body 2D.")]
	public class GetAngularVelocity2d : FsmStateAction {

		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		public FsmFloat velocity;
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			velocity = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetAngularVelocity();

			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoGetAngularVelocity();
		}

		void DoGetAngularVelocity()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			Rigidbody2D rigidbody = go.GetComponent<Rigidbody2D>();
			if (go == null) return;
			if (rigidbody == null) return;

			float angularVelocity = rigidbody.angularVelocity;

			velocity.Value = angularVelocity;
		}
	}
}
