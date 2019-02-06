﻿using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a UFO portal.</summary>
    public class UFOPortal : GamemodePortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the UFO portal.</summary>
        public override int ObjectID => (int)PortalType.UFO;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.UFO;

        /// <summary>The checked property of the UFO portal that determines whether the borders of the player's gamemode will be shown or not.</summary>
        [ObjectStringMappable(ObjectParameter.PortalChecked)]
        public bool Checked
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="UFOPortal"/> class.</summary>
        public UFOPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="UFOPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new UFOPortal());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as UFOPortal;
            c.Checked = Checked;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
