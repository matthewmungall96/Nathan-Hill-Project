// Copyright © Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEngine.Events;
using System;

namespace PixelCrushers.EmeraldAISupport
{

    public delegate void EmeraldAIDelegate(EmeraldAI.EmeraldAISystem emerald_AI);

    [Serializable]
    public class EmeraldAIEvent : UnityEvent<EmeraldAI.EmeraldAISystem> { }

    [Serializable]
    public class EmeraldPlayerHealthEvent : UnityEvent<EmeraldAI.Example.EmeraldAIPlayerHealth> { }

    /// <summary>
    /// Utility functions for working with Emerald AI.
    /// </summary>
    public static class EmeraldAIUtility
    {

        /// <summary>
        /// Applies a delegate function to all active Emerald AIs who belong to the specified faction.
        /// </summary>
        public static void ApplyToFaction(int faction, EmeraldAIDelegate delegateFunction)
        {
            var all = GameObject.FindObjectsOfType<EmeraldAI.EmeraldAISystem>();
            for (int i = 0;  i < all.Length; i++)
            {
                var ai = all[i];
                if (ai.enabled && ai.CurrentFaction == faction)
                {
                    delegateFunction(ai);
                }
            }
        }

    }
}