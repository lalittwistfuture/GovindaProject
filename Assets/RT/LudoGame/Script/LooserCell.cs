using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class LooserCell : MonoBehaviour
{

	public GameObject playerName;
	public GameObject PlayerImage;
	public GameObject PlayerRank;



	public void updateLoserCell(string name,string rank){
		playerName.GetComponent<Text> ().text = name;

	//	PlayerRank.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("");
		
	}


}
}