using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class RedeemCell : MonoBehaviour
    {

        //	public GameObject AvailCoin;
        public GameObject RedeemCoin;
        //	public GameObject Trnsaction_ID;
        public GameObject Trnsaction_Date;
        public GameObject Status;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateCell(string Redeem_coin, string txn_date, string stu)
        {
            //AvailCoin.GetComponent<Text> ().text = Available_coin;
            RedeemCoin.GetComponent<Text>().text = Redeem_coin;
            Status.GetComponent<Text>().text = stu;
            Trnsaction_Date.GetComponent<Text>().text = txn_date;
        }

    }
}
