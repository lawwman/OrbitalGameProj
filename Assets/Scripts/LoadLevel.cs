using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    public void LoadingLevel(int level)
    {
        Application.LoadLevel(level);
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
}
