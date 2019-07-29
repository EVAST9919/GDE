﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a GRND Color trigger.</summary>
    [ObjectID(TriggerType.GRND)]
    public class GRNDColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the GRND Color trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.GRND;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => (int)SpecialColorID.GRND;

        /// <summary>Initializes a new instance of the <seealso cref="GRNDColorTrigger"/> class.</summary>
        public GRNDColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="GRNDColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public GRNDColorTrigger(float duration, bool tintGround = false)
            : base(duration, false, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="GRNDColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GRNDColorTrigger());
    }
}
