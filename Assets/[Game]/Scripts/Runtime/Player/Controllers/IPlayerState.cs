
public interface IPlayerState
{
    public void EnterState(PlayerStateManager playerStateManager);
    public void UpdateState(PlayerStateManager playerStateManager);
    public void ExitState(PlayerStateManager playerStateManager);
}
