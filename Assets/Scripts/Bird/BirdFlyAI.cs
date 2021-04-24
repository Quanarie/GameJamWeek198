using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for AI for flying birds
/// </summary>
public abstract class BirdFlyAI : MonoBehaviour, IBirdFlyAIInitializer
{
    /// <summary>
    /// Use this function to initialize Bird Fly AI
    /// </summary>
    /// <param name="flyDirection"></param>
    /// <param name="normalFlySpeed"></param>
    public abstract void Initialize(Direction flyDirection, float normalFlySpeed);

    /// <summary>
    /// This function takes in flyMode and flies the bird defined by the mode.
    /// </summary>
    /// <param name="flyMode"></param>
    protected abstract void Fly(FlyMode flyMode);
}
