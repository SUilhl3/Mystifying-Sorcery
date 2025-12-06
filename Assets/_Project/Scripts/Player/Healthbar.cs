using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

   
}
