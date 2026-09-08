using UnityEngine;
using TMPro;

public class HeightMeter : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI heightText;

    [Header("Settings")]
    public string prefix = "Height: ";
    public string suffix = " m";

    void Update()
    {
        int height = Mathf.RoundToInt(transform.position.y);
        heightText.text = prefix + height + suffix;
    }
}