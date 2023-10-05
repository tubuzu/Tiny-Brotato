using UnityEngine;

public class Utils
{
    static public bool IsLayerInMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
}
