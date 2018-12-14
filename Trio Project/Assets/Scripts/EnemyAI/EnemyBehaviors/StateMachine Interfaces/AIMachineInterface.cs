/*namespace TrioProject.StateMachine
{

public interface AIMachineInterface
{

/// <summary>
/// CHANGE ALL AI REFERENCES TO NEW "STATE" REFERENCE!!!
/// </summary>
/// 
void SetInitialState<T>() where T : AI;
void SetInitialState(System.Type T);

void ChangeState<T>() where T : AI;
void ChangeState(System.Type T);

void IsCurrentState<T>() where T : AI;
void IsCurrentState(System.Type T);

void AddState<T>() where T : AI, new();
void AddState(System.Type T);

void RemoveState<T>() where T : AI;
void RemoveState(System.Type T);

bool ContainsState<T>() where T : AI;
bool ContainsState(System.Type T);

void RemoveAllStates();
string name {get; set;}


}

}*/