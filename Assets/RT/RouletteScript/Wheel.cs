using UnityEngine;
using System.Collections;

public class WheelClass : MonoBehaviour {
	Vector3 wheelRotVector;

	RectTransform wheel;
	RectTransform ball;
	float ballangle= 0f;
	private int[] number = { 0,32,15,19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5 ,
		24, 16, 33, 1,20, 14, 31,9, 22, 18, 29, 7, 28, 12, 35, 3, 26};
	private int winNumber;
	private float offset = 360f / 37f;
	private float radius = 123f;
	int counter = 0;
	int previousCounter = -1;
	bool isRotate = false;
	bool isBallRotate = false;
	float time;

	void Start () {
		wheel = GameObject.Find ("wheel").GetComponent<RectTransform> ();
		ball = GameObject.Find ("ball").GetComponent<RectTransform> ();
		reset ();

	}
		
	void FixedUpdate(){
			if (isRotate) {
				wheelRotVector += new Vector3 (0f, 0f, 2f);
				wheel.transform.eulerAngles = wheelRotVector;

			if (isBallRotate) {
				ballangle += offset / 2;
				if (ballangle >= 360f) {
					ballangle = ballangle - 360f;
				}

				int index = (int)(ballangle / offset);
				if (number [index] == winNumber) {
					if (previousCounter != counter) {
						counter++;
						previousCounter = counter;
						radius -= 35 / counter;
					}
				} else {
					previousCounter = -1;
				}
				if (counter == 4) {
					ballangle = index * offset + offset / 2;
					isBallRotate = false;
					time = Time.time;
				}
				float x = radius * Mathf.Sin (Mathf.Deg2Rad * ballangle);
				float y = radius * Mathf.Cos (Mathf.Deg2Rad * ballangle);
				ball.transform.localPosition = new Vector3 (x, y, 0f);

			} else {
				if (Time.time - time > 2) {
					isRotate = false;
				}
			}
				
			}
	}

	public void reset(){
		ballangle = 0;
		wheelRotVector = Vector3.zero;
		wheel.transform.eulerAngles = wheelRotVector;
		winNumber = -1;
		isBallRotate = false;
		isRotate = false;
		radius = 123f;
		counter = 0;
		previousCounter = -1;

	}

	public void startWheel(){
		reset ();
		isBallRotate = true;
		isRotate = true;
		winNumber = number[Random.Range (0, number.Length - 1)];
		winNumber = 5;
	}
}
