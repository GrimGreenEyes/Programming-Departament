using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumMapOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public enum mapOptions
    {
        matchLoose,
        matchWon,
        returnMap,
        loadGame,
        loadNextScene
    }

    public enum mapBiom
    {
        desierto,
        llanura,
        nieve,
        selva
    }

    public int biomToInt(EnumMapOptions.mapBiom biom)
    {
        switch (biom)
        {
            case mapBiom.desierto:
                return 0;
                break;
            case mapBiom.llanura:
                return 1;
                break;
            case mapBiom.nieve:
                return 2;
                break;
            case mapBiom.selva:
                return 3;
                break;
        }
        return 0;
    }
}
