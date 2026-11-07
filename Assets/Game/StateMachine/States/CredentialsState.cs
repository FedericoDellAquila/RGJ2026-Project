using Game.StateMachine.States;

public class CredentialsState : GameState
{
    private void Awake()
    {
        Desktop desktop = MainCanvas.Instance.GetComponentInChildren<Desktop>();
        CredentialsUI credentialsUI = desktop.GetComponentInChildren<CredentialsUI>();
        StartDocumentationProcess startDocumentationProcess = desktop.GetComponentInChildren<StartDocumentationProcess>();
        Destroy(startDocumentationProcess);
        credentialsUI.gameObject.SetActive(true);

    }

    public override bool IsTransitionConditionMet()
    {
        throw new System.NotImplementedException();
    }
}
