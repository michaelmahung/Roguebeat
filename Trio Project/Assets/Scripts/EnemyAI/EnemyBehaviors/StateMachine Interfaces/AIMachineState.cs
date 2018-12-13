namespace TrioProject.StateMachine
{
[System.Serializable]
public abstract class AIMachineState : AIMachineInterface, AIState
{
public abstract void AddStates();

public override void Initialize ()
		{
			base.Initialize ();
			name = machine.name + "." + GetType ().ToString ();

			AddStates ();

			currentState = initialState;
			if (null == currentState) {
				throw new System.Exception ("\n" + name + ".nextState is null on Initialize()!\t Did you forget to call SetInitialState()?\n");
			}

			foreach (System.Collections.Generic.KeyValuePair<System.Type, State> pair in states) 
			{
				pair.Value.Initialize ();
			}

			onEnter = true;
			onExit = false;
		}

		public override void Execute ()
		{
			base.Execute ();

			if (onExit) {
				currentState.Exit ();
				currentState = nextState;
				nextState = null;

				onEnter = true;
				onExit = false;
			}

			if (onEnter) {
				currentState.Enter ();
				onEnter = false;
			}

			try {
				currentState.Execute ();
			} 
			catch (System.NullReferenceException e) 
			{
				if (null == initialState) 
				{
					throw new System.Exception ("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
				} 
				else 
				{
					throw new System.Exception ("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
				}
			}
		}

		public override void PhysicsExecute()
		{
		base.PhysicsExecute();
		if(!(onEnter && onExit))
		{
		try
		{
		currentState.PhysicsExecute();
		}
		catch (System.NullReferenceException e)
		{
		if (null == initialState)
		{
						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
                    }
                    else
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
                    }
                }
            }
        }


		public override void PostExecute()
        {
            base.PostExecute();

            if (!(onEnter && onExit))
            {
                try
                {
                    currentState.PostExecute();
                }
                catch (System.NullReferenceException e)
                {
                    if (null == initialState)
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
                    }
                    else
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
                    }
                }
            }
        }

		public override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);

            if (!(onEnter && onExit))
            {
                try
                {
                    currentState.OnAnimatorIK(layerIndex);
                }
                catch (System.NullReferenceException e)
                {
                    if (null == initialState)
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
                    }
                    else
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
                    }
                }
            }
        }