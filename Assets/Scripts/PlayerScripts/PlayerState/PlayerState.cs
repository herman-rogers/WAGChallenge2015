using UnityEngine;
using System.Collections;

/*Abstract State Class*/
public class PlayerState {

    //Add Extra state classes here
    public static JumpingState _jumpState;
    public static SpeedVaultState _speedVault;
    public static DashVaultState _dashVault;
    public static KongState _kongVault;

    public virtual void HandlePlayerInput( bool input ) { }

    public virtual void PlayerUpdate( GameObject player, bool input ) { }
	
}