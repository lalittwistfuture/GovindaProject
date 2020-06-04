using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags
{
	
	public const string TAG = "TAG";
	public const string START_DEAL = "START_DEAL";
	public const string SESSION_ID = "SESSION_ID";
	public const string TURN = "TURN";
	public const string CARDS = "CARDS";
	public const string PLAYER = "PLAYER";
	//public const string URL = "http://flyingfoxcasino.in/app_clone/index.php";
   // public const string URL = "http://localhost/CasinoBlueMoon/app/roulette/index.php";
 //public const string URL = "http://caswin.co/app/roulette/index.php";
 //public const string URL ="http://deathwish.in/roulette_RT/roulette/index.php";
 	public const string URL ="http://fossilco.in/roulette_RT/roulette/index.php";
	
   //public const string URL = "http://deathwish.in/caswin/index.php";

    public const string ROOM_INFO = "ROOM_INFO";
	public const string STATUS = "STATUS";

    public const string DOMAIN = "DOMAIN";
	public const string FINISH = "FINISH";
	public const string VALUE = "VALUE";
	public const string TIMER_STOP = "TIMER_STOP";

	public const string CONNECTION_STATUS = "CONNECTION_STATUS";
    public const string APP_DOWNLOAD_URL = "APP_DOWNLOAD_URL";
	public const string WINNER = "WINNER";
	public const string LEAVE_TABLE = "LEAVE_TABLE";
	public const string SORT = "SORT";
	public const string SORTED_CARD = "SORTED_CARD";
	public const string JOKER_CARD = "JOKER_CARD";
	public const string OPEN_CARD = "OPEN_CARD";
	public const string CLOSE_CARD = "CLOSE_CARD";
	public const string LEAVE_EXTRA_CARD = "LEAVE_EXTRA_CARD";
	public const string CARD_TYPE = "CARD_TYPE";
	public const string DECK = "DECK";
	public const string OPEN = "OPEN";
	public const string PLAYER_STATE = "PLAYER_STATE";
	public const string CARDS_STATE = "CARDS_STATE";

	public const string CARDS_DETAIL = "CARDS_DETAIL";
	public const string POSITION = "POSITION";
	public const string RESULT = "RESULT";
	public const string PACK_PLAYER = "PACK_PLAYER";
	public const string ALL_PLAYER = "ALL_PLAYER";
	public const string WAITING_FOR_NEXT_ROUND = "WAITING_FOR_NEXT_ROUND";
	public const string WAITING_FOR_PLAYER = "WAITING_FOR_PLAYER";

	public const string MAX_BLIND = "MAX_BLIND";

	public const string SOUND = "SOUND";
	public const string SOUND_ON = "SOUND_ON";
	public const string SOUND_OFF = "SOUND_OFF";
	public const string AVATAR = "AVATAR";



    public  const string GameType = "GameType";
    public  const string UserLimit = "UserLimit";
    public  const string GAME_ENTRY = "GAME_ENTRY";
    public  const string GAME_PRICE = "GAME_PRICE";



}

public class GameTags
{
    public const string ROOM_ID = "ROOM_ID";
    public const string PRIVATE_TABLE_TYPE = "PRIVATE_TABLE_TYPE";
    public const string CREATE_TABLE = "CREATE_TABLE";
    public const string JOIN_TABLE = "JOIN_TABLE";

    public const string GAME_TYPE = "GAME_TYPE";
    public const string PUBLIC = "PUBLIC";
    public const string PRIVATE = "PRIVATE";

    public const string MUSIC_ON = "MUSIC_ON";
    public const string SOUND_ON = "SOUND_ON";
    public const string NOTIFICATION_ON = "NOTIFICATION_ON";

    public const string FACEBOOK_FRIEND = "FACEBOOK_FRIEND";
    public const string OPPONENT_ID = "OPPONENT_ID";

    public const string CHALLENGE_FRIEND = "CHALLENGE_FRIEND";
    public const string OFFLINE = "OFFLINE";
    public const string FB_FRIEND_ONLINE = "FB_FRIEND_ONLINE";
}


public class NetworkTags
{
	public const int CONNECTED = 1;
	public const int NOT_CONNECTED = 2;
	public const int DISCONNECTED = 3;

}

public class ClientTags
{
	public const string GAME_REQUEST = "GAME_REQUEST";
	public const string REQUEST_NEW_CARD = "REQUEST_NEW_CARD";
	public const string REQUEST_DEAL_CARD = "REQUEST_DEAL_CARD";
	public const string MY_CARD = "MY_CARD";
	public const string DECLARE = "DECLARE";
	public const string DROP = "DROP";
	public const string DONE = "DONE";
	public const string UPDATE_CARD = "UPDATE_CARD";
	public const string LEAVE_ROOM = "LEAVE_ROOM";
	public const string FINAL_CARD = "FINAL_CARD";
	public const string FINAL_CARD_DONE = "FINAL_CARD_DONE";



}

public class ServerTags
{

	public const string TOSS_CARD = "TOSS_CARD";
	public const string DEAL_START = "DEAL_START";
	public const string CARD_HISTORY = "CARD_HISTORY";
	public const string POINT = "POINT";
    public const string PLAYER_ID = "PLAYER_ID";
	public const string CARDS_INFO = "CARDS_INFO";
	public const string YOUR_NEW_CARD = "YOUR_NEW_CARD";
	public const string YOUR_DEAL_CARD = "YOUR_DEAL_CARD";
	public const string REQUEST_DECLARE = "REQUEST_DECLARE";
	public const string PACK_PLAYER = "PACK_PLAYER";
	public const string REMOVE_PLAYER = "REMOVE_PLAYER";
	public const string TOTAL_POINT = "TOTAL_POINT";
	public const string PLAYER_SCORE = "SCORE";
	//public const string PLAYER_SCORE = "SCORE";
	public const string ROOM_DATA = "ROOM_DATA";
	public const string DISPLAY_NAME = "DISPLAY_NAME";
	public const string PLAYER_STATUS = "PLAYER_STATUS";
	public const string CURRENT_TURN = "CURRENT_TURN";
	public const string SORT_BTN = "SORT_BTN";
	public const string GAME_FINISH = "GAME_FINISH";
	public const string REQUEST_SHOW_CARD = "REQUEST_SHOW_CARD";

	public const string TOSS_WINNER = "TOSS_WINNER";
	public const string INVALID_DACLARATION = "INVALID_DACLARATION";
	public const string VALID_DACLARATION = "VALID_DACLARATION";
	public const string PACK_MESSAGE = "PACK_MESSAGE";
	public const string CONNECTION = "CONNECTION";



	public const string REMOVE_SCORE_BOARD = "REMOVE_SCORE_BOARD";
	public const string REMOVE_ALL_CARDS = "REMOVE_ALL_CARDS";


    public const string LAST_POSITION = "LAST_POSITION";
    public const string VALUES = "VALUE";
    public const string READY_GOTI = "READY_GOTI";
    public const string POSITION = "POSITION";
    public const string MOVE_GOTI = "MOVE_GOTI";
    public const string FROM_POSITION = "FROM_POSITION";
    public const string TO_POSITION = "TO_POSITION";
    public const string TAG = "TAG";
    public const string GAME_REQUEST = "GAME_REQUEST";
    public const string TURN = "TURN";
    public const string PLAYER = "PLAYER";
    public const string DICE_ROLL = "DICE_ROLL";
    public const string ROOM_INFO = "ROOM_INFO";
    public const string WINNER_PLAYER = "WINNER_PLAYER";
    public const string COLOR = "COLOR";
    public const string GOTI_WIN = "GOTI_WIN";
    public const string START_DEAL = "START_DEAL";

    public const string CHATTING_START = "CHATTING_START";
    public const string CHAT_MSG = "CHAT_MSG";
    public const string PLAYER_NAME = "PLAYER_NAME";

    public const string EMOJI = "EMOJI";
    public const string EMOJI_NUMBER = "EMOJI_NUMBER";
    public const string TURN_MISS = "TURN_MISS";





}
public class DeviceTags
{

    public const string VALUES = "VALUE";
    public const string DICE_ROLL_DONE = "DICE_ROLL_DONE";
    public const string PLAYER = "PLAYER";
    public const string SELECT_GOTI = "SELECT_GOTI";

    public const string COIN = "COIN";
    public const string DISPLAY_NAME = "DISPLAY_NAME";
    public const string PIC = "PIC";
    public const string TOTAL_MATCH = "TOTAL_MATCH";
    public const string WON_MATCH = "WON_MATCH";
    public const string ENTRY_FEE = "ENTRY_FEE";

}




public class Game
{
	public const string Type = "GameType";
	public const int CASH = 1;
	public const int TOURNAMENT = 2;
	public const int PRACTICE = 3;


	public const string Verient = "Verient";
	public const int POOL = 3;
	public const int DEAL = 2;
	public const int POINT = 1;


	public const string VerientType = "VerientType";
	public const int Best_of_3 = 3;
	public const int Best_of_6 = 6;
	public const int Best_of_2 = 2;
	public const int Pool_101 = 101;
	public const int Pool_201 = 201;
	public const int NONE = 0;

	public const string Table = "Table";
	public const int Table_2 = 2;
	public const int Table_6 = 6;

	public const string Price = "Price";
	public const string GameType = "GameType";
	public const string DOMAIN = "DOMAIN";
	public const int Ludo = 1;
	public const int Roulette = 2;
	public const int TeenPatti = 3;

	public const string GameMode = "GameMode";
	public const int Public = 1;
	public const int Private = 2;
	public const string TableMode = "TableMode";
	public const int Create = 1;
	public const int Join = 2;
}

public class PlayerDetails
{
	
	public const string Name = "Plyer_name";
	public const string RealName = "DISPLAY_NAME";
	public const string Coin = "coin";
    public const string ConnectionId = "ConnectionId";
    public const string LoginId = "LoginId";
	public const string Password = "Password";
	public const string Ludo_booster = "have_ludo_booster";
	public const string RealMoney = "RealMoney";
	public const string Picture = "Picture";
    public const string Email = "Email";
    public const string Bet_Amount = "BET_Amount";
    public const string Mobile = "Mobile";
    //	public const string Plyer_name = "Plyer_name";
    //	public const string Plyer_name = "Plyer_name";
    //	public const string Plyer_name = "Plyer_name";



}

public class GetPlayerDetailsTags
{
    public const string PLAYER_NAME = "DISPLAY_NAME";
    public const string PLAYER_MOBILE = "Mobile";
    public const string PLAYER_ID = "ConnectionId";
    public const string PLAYER_EMAIL = "Email";
    public const string PLAYER_PASSWORD = "Password";
    public const string PLAYER_IMAGE = "Picture";
//    public const string LUDO_BOOSTER = "have_ludo_booster";
    public const string PLAYER_COIN = "coin";
    public const string PLAYER_FBID = "PLAYER_FBID";
    public const string TOTAL_MATCH = "TOTAL_MATCH";
    public const string WON_MATCH = "WON_MATCH";
    public const string REFREL_CODE = "REFREL_CODE";
    public const string GET_BONUS = "GET_BONUS";
    public const string FB_FRND_ID = "FB_FRND_ID";
    public const string ROOM_ID = "ROOM_ID";
    public const string PARENT_ID = "PARENT_ID";
    public const string PLAYER_REFERAL = "PLAYER_REFERAL";
    public const string PLAYER_REFERALCOIN = "PLAYER_REFERALCOIN";
    

}







