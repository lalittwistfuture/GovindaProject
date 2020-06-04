using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate{
public class CellScript : MonoBehaviour
{
	  
	public int cellIndex;
	public bool isSafe =true;

	// Use this for initialization
	void Start ()
	{
		cellIndex = int.Parse (transform.gameObject.name);
	}

	// Update is called once per frame
	void Update ()
	{
	

	}


}}
