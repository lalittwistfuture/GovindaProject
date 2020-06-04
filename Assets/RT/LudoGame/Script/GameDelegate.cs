using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
namespace LudoGameTemplate{
public class GameDelegate : MonoBehaviour
{

	public delegate void RecivedMassage (string sender, string msg);


	public static event RecivedMassage onRecivedMassage;

	public static void chatRecived (string sender, string msg)
	{
		if (onRecivedMassage != null) {
			onRecivedMassage (sender, msg);	
		}
	}


	public delegate void ShowButtonPanel (JSONNode Button, GameObject goti);

	public static event ShowButtonPanel onShowButtonPanel;

	public static void showButton (JSONNode Button,GameObject goti)
	{
		
		if (onShowButtonPanel != null) {
			onShowButtonPanel (Button, goti);	
		
		}
		
	}

	public delegate void OpenGotiAction ();

		public static event OpenGotiAction onOpenGotiAction;

		public static void openGoti ()
		{
			if (onOpenGotiAction != null) {
				onOpenGotiAction ();	
			}
		}


	public delegate void OpenOTPContainer (string name);
	public static event OpenOTPContainer onOpenOTPContainer;
	public static void sendOTP (string name)
	{
		if (onOpenOTPContainer != null) {
			onOpenOTPContainer (name);	
		}
	}

	public delegate void OpenFORGETOTPContainer (string name);
	public static event OpenFORGETOTPContainer onOpenFORGETOTPContainer;
	 public static void sendFORGETOTP(string name)
    {
        if (onOpenFORGETOTPContainer != null)
        {
            onOpenFORGETOTPContainer(name);
        }
    }
	public delegate void VarifyOTP();
	public static event VarifyOTP onVarifyOTP;
	public static void doneOTP()
	{
		if(onVarifyOTP!=null)
		{
			onVarifyOTP();	
		}
	}


	public delegate void SelectAvtar (string name);
	public static event SelectAvtar onSelectAvtar;
	public static void selectAvtarName (string name)
	{
		if (onSelectAvtar != null) {
			onSelectAvtar (name);	
		}
	}



	public delegate void HideButtonPanel ();

	public static event HideButtonPanel onHideButtonPanel;

	public static void hideButton ()
	{
		if (onHideButtonPanel != null) {
			onHideButtonPanel ();	
		}
	}



	public delegate void DisableAllGoti ();

	public static event CutGotiSound onDisableAllGoti;

	public static void StartDisableAllGoti ()
	{
		if (onDisableAllGoti != null) {
			onDisableAllGoti ();	
		}
	}


	public delegate void CutGotiSound ();

	public static event CutGotiSound onCutGotiSound;

	public static void StartCutGotiSound ()
	{
		if (onCutGotiSound != null) {
			onCutGotiSound ();	
		} 
	}

	public delegate void ClappingSound ();

	public static event ClappingSound onClappingSound;

	public static void StartClappingSound ()
	{
		if (onClappingSound != null) {
			onClappingSound ();	
		}
	}

	public delegate void timerSound ();

	public static event timerSound onTimerSound;

	public static void StartTimerSound ()
	{
		if (onTimerSound != null) {
			onTimerSound ();	
		}
	}


	public delegate void HideGotipanel ();

	public static event HideGotipanel onHideGotipanel;

	public static void hideGotipanel ()
	{
		if (onHideGotipanel != null) {
			onHideGotipanel ();	
		}
	}


	public delegate void ErrorMessage (string msg);

	public static event SendTableCode onErrorMessage;

	public static void showErrorMessage (string msg)
	{
		if (onErrorMessage != null) {
			onErrorMessage (msg);	
		}
	}


	public delegate void FacebookRequestTable (string code);

	public static event SendTableCode onFacebookRequestTable;

	public static void showFacebookRequestTable (string code)
	{
		if (onFacebookRequestTable != null) {
			onFacebookRequestTable (code);	
		}
	}


	public delegate void SendTableCode (string code);

	public static event SendTableCode onSendTableCode;

	public static void showSendTableCode (string code)
	{
		if (onSendTableCode != null) {
			onSendTableCode (code);	
		}
	}


	public delegate void PlayerjoinTable ();

	public static event PlayerjoinTable onPlayerjoinTable;

	public static void showPlayerjoinTable ()
	{
		if (onPlayerjoinTable != null) {
			onPlayerjoinTable ();	
		}
	}


	//	public delegate void StartPrivateTable ();
	//	public static event StartPrivateTable onStartPrivateTable;
	//	public static void showStartPrivateTable ()
	//	{
	//		if (onStartPrivateTable != null) {
	//			onStartPrivateTable ();
	//		}
	//	}

	// define  delegate for turn
	public delegate void playerTurn (string playerId);

	public static event playerTurn onplayerTurn;

	public static void showPlayerTurn (string playerId)
	{
		if (onplayerTurn != null) {
			onplayerTurn (playerId);	
		}
	}

	// define  delegate for check player active or inactive
	public delegate void ActiveInactive (string playerId);

	public static event ActiveInactive onActiveInactive;

	public static void showActiveInactive (string playerId)
	{
		if (onActiveInactive != null) {
			onActiveInactive (playerId);	
		}
	}

	// define  delegate for player move
	public delegate void PlayerMove ();

	public static event PlayerMove onPlayerMove;

	public static void showPlayerMove ()
	{
		if (onPlayerMove != null) {
			onPlayerMove ();	
		}
	}

	// define  delegate for player Ladder move
	public delegate void PlayerLadderMove (int number);

	public static event PlayerLadderMove onLadderSound;

	public static void showPlayerLadderMove (int number)
	{
		if (onLadderSound != null) {
			// Debug.Log("ladder sound on in delegate");
			onLadderSound (number);	
		}
	}

	// define  delegate for player Snake move
	public delegate void PlayerSnakeMove (int number);

	public static event PlayerSnakeMove onSnakeSound;

	public static void showPlayerSnakeMove (int number)
	{
		if (onSnakeSound != null) {
			// Debug.Log("snake sound on in delegate");
			onSnakeSound (number);	
		}
	}

	public delegate void StopSelection ();

	public static event StopSelection onStopSelection;

	public static void stopAnimation ()
	{
		if (onStopSelection != null) {
			onStopSelection ();	
		}
	}



	public delegate void NumberSelection (int index);

	public static event NumberSelection onNumberSelection;

	public static void selectNumber (int index)
	{
		if (onNumberSelection != null) {
			onNumberSelection (index);	
		}
	}



	// define  delegate for rolling dices
	public delegate void RollDice (string gotiNumber);

	public static event RollDice onRollDice;

	public static void showRollDice (string gotiNumber)
	{
		if (onRollDice != null) {
			onRollDice (gotiNumber);	
		}
	}

	// define  delegate to check dropGame
	public delegate void DropGame (string playerId);

	public static event DropGame onDropGame;

	public static void showDropGame (string playerId)
	{
		if (onDropGame != null) {
			onDropGame (playerId);	
		}
	}

	// define  delegate for reconnect user
	public delegate void Reconnect (string playerId);

	public static event Reconnect onReconnect;

	public static void showReconnect (string playerId)
	{
		if (onReconnect != null) {
			onReconnect (playerId);	
		}
	}

	// define  delegate for TapDice
	public delegate void TapDice (string diceNumber);

	public static event TapDice onTapDice;

	public static void showTapDice (string diceNumber)
	{
		if (onTapDice != null) {
			onTapDice (diceNumber);	
		}
	}

	// define  delegate  to select player turn
	public delegate void currentPlayer ();

	public static event currentPlayer oncurrentPlayer;

	public static void showcurrentPlayer ()
	{
		if (oncurrentPlayer != null) {
			oncurrentPlayer ();	
		}
	}
	// define  delegate  to select player turn
	public delegate void MyOpponentPlayer ();

	public static event MyOpponentPlayer onMyOpponentPlayer;

	public static void showMyOpponentPlayer ()
	{
		if (onMyOpponentPlayer != null) {
			onMyOpponentPlayer ();	
		}
	}


	// define  delegate  to select player turn
	public delegate void MoveGoti (int number);

	public static event MoveGoti onMoveGoti;

	public static void showMoveGoti (int number)
	{
		// Debug.Log("cell number="+number);
		if (onMoveGoti != null) {
			onMoveGoti (number);	
		}
	}

	// define  delegate  to select player turn
	public delegate void CloseGoti ();

	public static event CloseGoti onCloseGoti;

	public static void showCloseGoti ()
	{
		if (onCloseGoti != null) {
			onCloseGoti ();	
		}
	}

	public delegate void RemoveNumberSix (int number);

	public static event RemoveNumberSix onRemoveNumber;

	public static void showRemoveNumber (int number)
	{
		if (onRemoveNumber != null) {
			onRemoveNumber (number);	
		}
	}

    public delegate void SocketConnectionChange(bool connected);
    public static event SocketConnectionChange onSocketConnectionChange;
    public static void changeSocketConnection(bool connected)
    {
        if (onSocketConnectionChange != null)
        {
            onSocketConnectionChange(connected);
        }
    }

    public delegate void PlayerSyncronized(string player, int[] data);
    public static event PlayerSyncronized onPlayerSyncronized;
    public static void playerData(string player,int[] data)
    {
        if (onPlayerSyncronized != null)
        {
            onPlayerSyncronized(player,data);
        }
    }



}
}