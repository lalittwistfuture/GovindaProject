using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class GameType : MonoBehaviour
{

	public Text Type;
	public Text EntryFee;
	public Text winnerCoin;

	


	public void updateGameType(){

		Type.text = "1 on 1";

	}

	public void selectCoin ()
	{
		GameConstantData.entryFee = int.Parse(EntryFee.text);
		GameConstantData.winingAmount = int.Parse(winnerCoin.text);
		SceneManager.LoadSceneAsync ("GameScene");
	}

}
}