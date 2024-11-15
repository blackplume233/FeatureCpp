﻿using System.Collections;

namespace Bolt
{
    /// <summary>
    /// Delays flow by waiting until the next frame.
    /// </summary>
    [UnitTitle("Wait For Next Frame")]
    [UnitShortTitle("Next Frame")]
    [UnitOrder(4)]
    public class WaitForNextFrameUnit : WaitUnit
    {
        protected override IEnumerator Await(Flow flow)
        {
            yield return null;

            yield return exit;
        }
    }
}