using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class PurchaseCell : MonoBehaviour
    {

        public GameObject AddCoin;
        public GameObject Transaction_id;
        public GameObject Transaction_date;
        public GameObject status;
        // Use this for initialization
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateCell(string Total_Coin, string txn_id, string txn_date, string stu)
        {

            AddCoin.GetComponent<Text>().text = Total_Coin;
            Transaction_id.GetComponent<Text>().text = txn_id;
            Transaction_date.GetComponent<Text>().text = txn_date;
            status.GetComponent<Text>().text = stu;
        }
    }
}