using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessage {
	public const string ConnectionNotFound = "ConnectionError";
	public const string RoomNotFound = "ConnectionError";
	public const string RecoverConnection = "ConnectionError";
	public const string ConnectionTempraryError = "ConnectionError";
	public const string FetchingOpponent = "ConnectionError";

}

public class ErrorType {
	public const int ConnectionNotFound = 1;
	public const int RoomNotFound = 2;
	public const int RecoverConnection = 3;
	public const int ConnectionTempraryError = 4;
}
