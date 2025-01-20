using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GetActiveItems : MonoBehaviour
{
    public enum ColorFilter
    {
        Red,
        Green,
        Blue,
        Rainbow,
        None
    }

    public ColorFilter colorFilter;
    public Volume volume;

    private void Awake()
    {
        colorFilter = PlayerPrefs.HasKey("ColorFilter") ? (ColorFilter)PlayerPrefs.GetInt("ColorFilter") : ColorFilter.None;
        
        volume.profile.TryGet<ColorAdjustments>(out var colorAdjustment);
        colorAdjustment.colorFilter.value = colorFilter switch
        {
            ColorFilter.Red => new Color(1, 0, 0),
            ColorFilter.Green => new Color(0, 1, 0),
            ColorFilter.Blue => new Color(0, 0, 1),
            ColorFilter.None => new Color(1, 1, 1),
            ColorFilter.Rainbow => new Color(0, 0, 0),
            _ => new Color(1, 1, 1)
        };
    }

    private void Update()
    {
        if (colorFilter == ColorFilter.Rainbow)
        {
            volume.profile.TryGet<ColorAdjustments>(out var colorAdjustment);
            colorAdjustment.colorFilter.value = Color.HSVToRGB(Mathf.PingPong(3 * Time.time, 1), 1, 1);
            colorAdjustment.hueShift.value = Mathf.PingPong(3 * Time.time, 180) - 90;
        }
        else
        {
            volume.profile.TryGet<ColorAdjustments>(out var colorAdjustment);
            colorAdjustment.colorFilter.value = colorFilter switch
            {
                ColorFilter.Red => new Color(1, 0, 0),
                ColorFilter.Green => new Color(0, 1, 0),
                ColorFilter.Blue => new Color(0, 0, 1),
                ColorFilter.None => new Color(1, 1, 1),
                _ => new Color(1, 1, 1)
            };
        }

        if (colorFilter != ColorFilter.Rainbow)
        {
            volume.profile.TryGet<ColorAdjustments>(out var colorAdjustment);
            colorAdjustment.hueShift.value = 0;
        }
    }

    public void SetColorFilter(int colorFilterIndex)
    {
        colorFilter = (ColorFilter)colorFilterIndex;
        PlayerPrefs.SetInt("ColorFilter", colorFilterIndex);
        PlayerPrefs.Save();
    }
}
