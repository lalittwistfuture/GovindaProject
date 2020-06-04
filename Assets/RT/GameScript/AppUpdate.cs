using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppUpdate : MonoBehaviour {
    public void AppUpdateAction () {
        Application.OpenURL ("https://fossilco.in/RT.apk");
    }
}