

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDelegateTeenPatti
{

    public delegate void UpdsteImage(string image);

    public static event UpdsteImage onUpdsteImage;

    public static void updateImage(string image)
    {
        if (onUpdsteImage != null)
        {
            onUpdsteImage(image);
        }
    }

    public delegate void BlindChal();

    public static event BlindChal onBlindChal;

    public static void blindChal()
    {
        if (onBlindChal != null)
        {
            onBlindChal();
        }
    }


    public delegate void AddCoinSound();

    public static event AddCoinSound onAddCoinSound;

    public static void addCoinSound()
    {
        if (onAddCoinSound != null)
        {
            onAddCoinSound();
        }
    }


    public delegate void TurnChange();

    public static event TurnChange onTurnChange;

    public static void turnChange()
    {
        if (onTurnChange != null)
        {
            onTurnChange();
        }
    }

    public delegate void GameDetail(string tableID, string round);

    public static event GameDetail onGameDetail;

    public static void gameDetail(string tableID, string round)
    {
        if (onGameDetail != null)
        {
            onGameDetail(tableID,round);
        }
    }



    public delegate void PlayerConnection(string playerID, bool connction);

    public static event PlayerConnection onPlayerConnection;

    public static void connctionChange(string playerID, bool connction)
    {
        if (onPlayerConnection != null)
        {
            onPlayerConnection(playerID,connction);
        }
    }

    public delegate void ShowChal();

    public static event ShowChal onShowChal;

    public static void showChaal()
    {
        if (onShowChal != null)
        {
            onShowChal();
        }
    }


    public delegate void ChaalChal();

    public static event ChaalChal onChaalChal;

    public static void challChal()
    {
        if (onChaalChal != null)
        {
            onChaalChal();
        }
    }




	public delegate void HideSideShowPanel ();

	public static event HideSideShowPanel onHideSideShowPanel;

	public static void hideShowPanel ()
	{
		if (onHideSideShowPanel != null) {
			onHideSideShowPanel ();	
		}
	}


	public delegate void RecivedMassage (string sender, string msg);

	public static event RecivedMassage onRecivedMassage;

	public static void chatRecived (string sender, string msg)
	{
		if (onRecivedMassage != null) {
			onRecivedMassage (sender, msg);	
		}
	}


	public delegate void CollectBootAmount (int amount);

	public static event CollectBootAmount onCollectBootAmount;

	public static void collectAmount (int amount)
	{
		if (onCollectBootAmount != null) {
			onCollectBootAmount (amount);	
		}
	}

	public delegate void ShowControlPanel (int amount);

	public static event ShowControlPanel onShowControlPanel;

	public static void showPanel (int amount)
	{
		if (onShowControlPanel != null) {
			onShowControlPanel (amount);	
		}
	}

	public delegate void HideControlPanel ();

	public static event HideControlPanel onHideControlPanel;

	public static void hidePanel ()
	{
		if (onHideControlPanel != null) {
			onHideControlPanel ();	
		}
	}

	public delegate void AddTotalAmount (int amount);

	public static event AddTotalAmount onAddTotalAmount;

	public static void ShowAddTotalAmount (int amount)
	{
		if (onAddTotalAmount != null) {
			onAddTotalAmount (amount);	
		}
	}


    public delegate void ServerSyncronization(int playerID);

    public static event ServerSyncronization onServerSyncronization;

    public static void syncronizedPlayer(int playerID)
    {
        if (onServerSyncronization != null)
        {
            onServerSyncronization(playerID);
        }
    }

	public delegate void PrivateTableCode (string code);

	public static event PrivateTableCode onPrivateTableCode;

	public static void ShowPrivateTableCode (string code)
	{
		if (onPrivateTableCode != null) {
			onPrivateTableCode (code);	
		}
	}

	public delegate void TotalGameUser (int user);
	public static event TotalGameUser onTotalGameUser;
	public static void ShowTotalGameUser (int user)
	{
		if (onTotalGameUser != null) {
			onTotalGameUser (user);	
		}
	}

	public delegate void EnableSideShow ();

	public static event EnableSideShow onEnableSideShow;

	public static void ShowEnableSideShow ()
	{
		if (onEnableSideShow != null) {
			onEnableSideShow ();	
		}
	}

	public delegate void GetRoomInfo ();

	public static event GetRoomInfo onGetRoomInfo;

	public static void ShowonGetRoomInfo ()
	{
		if (onGetRoomInfo != null) {
			onGetRoomInfo ();	
		}
	}

	// ************************** Sound Delegate ********************//

	public delegate void ClappingSound ();

	public static event ClappingSound onClappingSound;

	public static void ShowClappingSound ()
	{
		if (onClappingSound != null) {
			onClappingSound ();	
		}
	}

	public delegate void DistrubuteCardSound ();

	public static event DistrubuteCardSound onDistrubuteCardSound;

	public static void ShowoDistrubuteCardSound ()
	{
		if (onDistrubuteCardSound != null) {
			onDistrubuteCardSound ();	
		}
	}

	public delegate void CollectBootSound ();

	public static event CollectBootSound onCollectBootSound;

	public static void ShowCollectBootSound ()
	{
		if (onCollectBootSound != null) {
			onCollectBootSound ();	
		}
	}

	public delegate void StartClockSound ();

	public static event StartClockSound onStartClockSound;

	public static void ShowStartClockSound ()
	{
		if (onStartClockSound != null) {
			onStartClockSound ();	
		}
	}

}
