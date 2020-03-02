using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace RimWorld
{
    // Token: 0x0200041C RID: 1052
    [StaticConstructorOnStartup]
    public class MinisolarComp : CompPowerPlant
    {
        // Token: 0x17000276 RID: 630
        // (get) Token: 0x06001238 RID: 4664 RVA: 0x0009E588 File Offset: 0x0009C988
        protected override float DesiredPowerOutput
        {
            get
            {
                return Mathf.Lerp(0f, 400f, this.parent.Map.skyManager.CurSkyGlow) * this.RoofedPowerOutputFactor;
            }
        }

        // Token: 0x17000277 RID: 631
        // (get) Token: 0x06001239 RID: 4665 RVA: 0x0009E5C8 File Offset: 0x0009C9C8
        private float RoofedPowerOutputFactor
        {
            get
            {
                int num = 0;
                int num2 = 0;
                foreach (IntVec3 c in this.parent.OccupiedRect())
                {
                    num++;
                    if (this.parent.Map.roofGrid.Roofed(c))
                    {
                        num2++;
                    }
                }
                return (float)(num - num2) / (float)num;
            }
        }

        // Token: 0x0600123A RID: 4666 RVA: 0x0009E660 File Offset: 0x0009CA60
        public override void PostDraw()
        {
            base.PostDraw();
            GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
            r.center = this.parent.DrawPos + Vector3.up * 0.1f;
   
            r.fillPercent = base.PowerOutput / 400f;

            r.margin = 0.15f;
            Rot4 rotation = this.parent.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;
            GenDraw.DrawFillableBar(r);
        }

        // Token: 0x04000B15 RID: 2837
        private const float FullSunPower = 400f;

        // Token: 0x04000B16 RID: 2838
        private const float NightPower = 0f;
    }
}