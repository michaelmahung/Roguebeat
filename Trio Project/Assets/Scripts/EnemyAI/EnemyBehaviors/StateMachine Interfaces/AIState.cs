/*#define TRIO_STATEMACHINE_VERBOSE
namespace TrioProject.StateMachine
{
[System.Serializable]
public abstract class AIState : AIStateInterface
{
public virtual void Execute (){} // Creates a virtual Execute function that can called somewhere else using this script. By being virtual, this function can be overridden and ignored if desired.
public virtual void PhysicsExecute (){} // Not sure yet what this does
public virtual void PostExecute (){} // Not sure yet what this does

public virtual void OnAnimatorIK(int layerIndex){} // Not sure yet what this does

public virtual void Initialize ()
		{
			#if (TRIO_STATEMACHINE_VERBOSE)
			UnityEngine.Debug.Log();
			#endif // TRIO_STATEMACHINE_VERBOSE
		}

		public virtual void Enter ()
		{
			#if (TRIO_STATEMACHINE_VERBOSE)
			UnityEngine.Debug.Log();
			#endif // TRIO_STATEMACHINE_VERBOSE
		}

		public virtual void Exit ()
		{
			#if(TRIO_STATEMACHINE_VERBOSE)
			UnityEngine.Debug.Log();
			#endif // TRIO_STATEMACHINE_VERBOSE
		}

		public T GrabMachine<T> () where T : AIMachineInterface
		{
			try 
			{
				return (T)machine;

			} 
			catch (System.InvalidCastException e) 
			{
				if (typeof(T) == typeof(AIMachineState) || typeof(T).IsSubclassOf (typeof(AIMachineState))) {
					throw new System.Exception (machine.name + " .GrabMachine() cannot return the type you requested!\tYour machine is derived from AIMachineBehavior not AIMachineState!" + e.Message);
				}
				else if (typeof(T) == typeof(AIMachineBehavior) || typeof(T).IsSubclassOf(typeof(AIMachineBehavior)))
				{
				throw new System.Exception (machine.name + ".GrabMachine() cannot return the type you requested!\t Your machine is derived from AIMachineState not AIMachineBehavior!" + e.Message);
				}
				else
				{
				throw new System.Exception(machine.name + ".GrabMachine() cannot return the type you requested!\n" + e.Message);
				}
				}
				}

				internal AIMachineInterface machine { get; set; }

				public bool IsActive { get { return machine.IsCurrentState (GetType ()); } }*
			//	}
			//	}
            */