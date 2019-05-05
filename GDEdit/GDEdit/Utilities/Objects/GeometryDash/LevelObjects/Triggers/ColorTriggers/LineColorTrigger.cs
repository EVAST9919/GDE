﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Line Color trigger.</summary>
    [ObjectID(TriggerType.Line)]
    public class LineColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the Line Color trigger.</summary>
        public override int ObjectID => (int)TriggerType.Line;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => (int)SpecialColorID.Line;

        /// <summary>Initializes a new instance of the <seealso cref="LineColorTrigger"/> class.</summary>
        public LineColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="LineColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public LineColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="LineColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new LineColorTrigger());
    }
}
