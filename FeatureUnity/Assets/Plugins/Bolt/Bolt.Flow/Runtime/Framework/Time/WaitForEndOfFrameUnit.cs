﻿using System.Collections;
using UnityEngine;

namespace Bolt
{
    /// <summary>
    /// Delays flow by waiting until the end of the frame.
    /// </summary>
    [UnitTitle("Wait For End of Frame")]
    [UnitShortTitle("End of Frame")]
    [UnitOrder(5)]
    public class WaitForEndOfFrameUnit : WaitUnit
    {
        protected override IEnumerator Await(Flow flow)
        {
            yield return new WaitForEndOfFrame();

            yield return exit;
        }
    }
}