using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    //123
    public Text text;
    public string firstText;
    private static UIManager instance;
    private void Start()
    {
        instance = this;
        UpdateText();
    }

    public static void UpdateText()
    {
        instance.text.text = instance.firstText + Generator.FlagsLeft().ToString();
    }
}
