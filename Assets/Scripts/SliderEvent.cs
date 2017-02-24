using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ui = UnityEngine.UI;

public class SliderEvent : MonoBehaviour {
    public Slider type;
    public string info;
    private ui.Slider slider;
    private ui.Text text;
    private void Start()
    {
        slider = GetComponent<ui.Slider>();
        text = GetComponentInChildren<ui.Text>();
    }
    public void OnChangeValue()
    {
        if (type == Slider.WIDTH)
        {
            GameConstants.SetWidth((int)slider.value);
            text.text = info + ((int)slider.value).ToString();

        }
        else
        {
            GameConstants.SetLength((int)slider.value);
            text.text = info + ((int)slider.value).ToString();
        }
    }

}
public enum Slider
{
    LENGTH, WIDTH
}