using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Roullet
{
    public class RouletteDelegate : MonoBehaviour
    {


        public static int NetworkError = ErrorType.RecoverConnection;

        public delegate void CellSelected(int row, int column, Side side);
        public static event CellSelected onCellSelected;
        public static void cellClick(int row, int column, Side side)
        {
            if (onCellSelected != null)
            {
                onCellSelected(row, column, side);
            }
        }

        public delegate void OptionSelected(string tag, GameObject obj,string imageName);
        public static event OptionSelected onOptionSelected;
        public static void optionClick(string tag, GameObject obj,string imageName)
        {
            if (onOptionSelected != null)
            {
                onOptionSelected(tag, obj,imageName);
            }
        }


        public delegate void ErrorRecieved(int type);
        public static event ErrorRecieved onErrorRecieved;
        public static void onErrorOccure(int type)
        {
            if (onErrorRecieved != null)
            {
                onErrorRecieved(type);
            }
        }


        public delegate void NumberSelected(List<int> number, GameObject obj,string imageName);
        public static event NumberSelected onNumberSelected;
        public static void numberClick(List<int> number, GameObject obj,string imageName)
        {
            if (onNumberSelected != null)
            {
                onNumberSelected(number, obj,imageName);
            }
        }

        public delegate void ClearBet();
        public static event ClearBet onClearBet;
        public static void removeAllBet()
        {
            if (onClearBet != null)
            {
                onClearBet();
            }
        }
        public delegate void StartWheelRotation();
        public static event StartWheelRotation onStartWheelRotation;
        public static void startRotation()
        {
            if (onStartWheelRotation != null)
            {
                onStartWheelRotation();
            }
        }
        public delegate void StopWheelRotation();
        public static event StopWheelRotation onStopWheelRotation;
        public static void stopRotation()
        {
            if (onStopWheelRotation != null)
            {
                onStopWheelRotation();
            }
        }

        public delegate void MoveToTable();
        public static event MoveToTable onMoveToTable;
        public static void moveToBetTable()
        {
            if (onMoveToTable != null)
            {
                onMoveToTable();
            }
        }
        public delegate void WarpChatRecived(string sender, string message);
        public static event WarpChatRecived onWarpChatRecived;
        public static void chatRecived(string sender, string message)
        {
            if (onWarpChatRecived != null)
            {
                onWarpChatRecived(sender, message);
            }
        }
        public delegate void WinChip();
        public static event WinChip onWinChip;
        public static void winnerFound()
        {
            if (onWinChip != null)
            {
                onWinChip();
            }
        }

    }
}
