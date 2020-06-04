using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class EmojiScript : MonoBehaviour
{

	// Use this for initialization
	int count = 0;
	public GameObject ClosePanel;
	GameObject[]  emoji_Array;
	void Start ()
	{
		emoji_Array = GameObject.FindGameObjectsWithTag ("Emoji");
		foreach(GameObject btn in emoji_Array){
			btn.GetComponent<Button> ().onClick.AddListener (()=>EmojiOnClick(btn));
			btn.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Emoji/" + btn.name);
			//btn.getcom.onClick.AddListener(TaskOnClick);
		}
	}
		
	


	public void ShowEmojiPanel ()
	{
		count = 0;

	}



	void EmojiOnClick(GameObject btn){

		string emoji_number = btn.name;
		appwarp.SendEmoji (UserController.getInstance.Name, emoji_number);
		ClosePanel.SetActive (false);
		transform.gameObject.SetActive (false);
	}


}
}