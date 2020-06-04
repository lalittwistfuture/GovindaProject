using UnityEngine;
public class MenuScript : MonoBehaviour {
    public GameObject MenuPanel;
    void Start () {
        MenuPanel.SetActive (false);

    }
    // Update is called once per frame
    void Update () {

    }
    public void toggleMenu () {
        if (MenuPanel.activeSelf) {
            MenuPanel.SetActive (false);
        } else {
            MenuPanel.SetActive (true);
        }
    }
}