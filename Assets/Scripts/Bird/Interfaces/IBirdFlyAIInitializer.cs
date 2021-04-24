using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBirdFlyAIInitializer
{
    void Initialize(Direction flyDirection, float normalFlySpeed);
}
