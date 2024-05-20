using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools 
{
    public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Assurer que la valeur se trouve dans l'intervalle de départ
        value = Mathf.Clamp(value, fromMin, fromMax);

        // Calculer la proportion de la valeur par rapport à l'intervalle de départ
        float fromRange = fromMax - fromMin;
        float toRange = toMax - toMin;
        float normalizedValue = (value - fromMin) / fromRange;

        // Appliquer la proportion à l'intervalle de destination
        float remappedValue = toMin + (normalizedValue * toRange);

        return remappedValue;
    }

}
