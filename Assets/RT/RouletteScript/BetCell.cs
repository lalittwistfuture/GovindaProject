using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Roullet
{
    public enum Side
    {
        none = 0,
        top = 1,
        bottom = 2,
        left = 3,
        right = 4,
        bottom_Left = 5,
        bottom_Right = 6,
        top_Left = 7,
        top_Right = 8
    };

    public class BetCell : MonoBehaviour, IPointerDownHandler
    {
        //private Canvas canvas;
        private Camera camera1;
        private int sideValue = 12;
        private int column = 0, row = 0;
        private Side side = Side.none;
        //private GameObject chipSample;
        // Use this for initialization
        void Start()
        {
            //canvas = (Canvas)GameObject.Find ("Canvas").GetComponent<Canvas> ();
            camera1 = (Camera)GameObject.Find("Main Camera").GetComponent<Camera>();
            //chipSample = GameObject.Find ("CoinSample");
        }

        public void setIndexValue(int colum, int row)
        {
            this.column = colum;
            this.row = row;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), new Vector2(eventData.position.x, eventData.position.y), camera1, out point);
            calculatePosition(point);
        }


        void calculatePosition(Vector2 point)
        {
            // Debug.Log("Working is ff "+point);
            side = Side.none;
            if (point.x > sideValue)
            {
                if (this.column != 11)
                {
                    side = Side.left;
                    if (point.y > sideValue)
                    {
                        // Debug.Log("Side.BottomLeft");
                        side = Side.bottom_Left;
                    }
                    else if (point.y < -sideValue)
                    {
                        // Debug.Log("Side.TopLeft");
                        side = Side.top_Left;
                    }
                }
            }
            else if (point.x < -sideValue)
            {
                if (this.column != 0)
                {
                    side = Side.right;
                    // Debug.Log("Side.Right");
                    if (point.y > sideValue)
                    {
                        // Debug.Log("Side.BottomRight");
                        side = Side.bottom_Right;
                    }
                    else if (point.y < -sideValue)
                    {
                        // Debug.Log("Side.TopRight");
                        side = Side.top_Right;
                    }
                }
            }
            else if (point.y > sideValue)
            {
                // Debug.Log("Side.Bottom");
                side = Side.bottom;
            }
            else if (point.y < -sideValue)
            {
                // Debug.Log("Side.Top");
                side = Side.top;
            }

            RouletteDelegate.cellClick(this.row, this.column, side);


        }


    }
}
