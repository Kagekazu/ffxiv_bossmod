﻿using System;

namespace BossMod
{
    // attribute that specifies group count and names for group assignment property
    [AttributeUsage(AttributeTargets.Field)]
    public class GroupDetailsAttribute : Attribute
    {
        public string[] Names;

        public GroupDetailsAttribute(string[] names)
        {
            Names = names;
        }
    }

    // config node property that allows assigning party roles to arbitrary named groups
    // typically you would use derived classes that provide validation
    public class GroupAssignment
    {
        public int[] Assignments = new int[(int)PartyRolesConfig.Role.Unassigned]; // role -> group id

        public int this[PartyRolesConfig.Role r]
        {
            get => Assignments[(int)r];
            set => Assignments[(int)r] = value;
        }

        public virtual bool Validate() => true;
    }

    // assignments to two light parties with THMR split
    public class GroupAssignmentLightParties : GroupAssignment
    {
        public GroupAssignmentLightParties()
        {
            this[PartyRolesConfig.Role.MT] = this[PartyRolesConfig.Role.H1] = this[PartyRolesConfig.Role.M1] = this[PartyRolesConfig.Role.R1] = 0;
            this[PartyRolesConfig.Role.OT] = this[PartyRolesConfig.Role.H2] = this[PartyRolesConfig.Role.M2] = this[PartyRolesConfig.Role.R2] = 1;
        }

        public override bool Validate()
        {
            for (int i = 0; i < (int)PartyRolesConfig.Role.Unassigned; i += 2)
                if (Assignments[i] < 0 || Assignments[i] >= 2 || Assignments[i + 1] < 0 || Assignments[i + 1] >= 2 || Assignments[i] == Assignments[i + 1])
                    return false;
            return true;
        }
    }

    // assignments to four tank/healer+DD pairs
    public class GroupAssignmentDDSupportPairs : GroupAssignment
    {
        public GroupAssignmentDDSupportPairs()
        {
            this[PartyRolesConfig.Role.MT] = this[PartyRolesConfig.Role.R1] = 0;
            this[PartyRolesConfig.Role.H1] = this[PartyRolesConfig.Role.M1] = 1;
            this[PartyRolesConfig.Role.OT] = this[PartyRolesConfig.Role.R2] = 2;
            this[PartyRolesConfig.Role.H2] = this[PartyRolesConfig.Role.M2] = 3;
        }

        public override bool Validate()
        {
            ulong mask = 0; // bits 0-3 - support for group N, bits 4-7 - dd for group (N-4)
            Action<int, int> addToMask = (group, offset) =>
            {
                if (group is >= 0 and < 4)
                    BitVector.SetVector64Bit(ref mask, group + offset);
            };
            for (int i = 0; i < 4; ++i)
                addToMask(Assignments[i], 0);
            for (int i = 4; i < 8; ++i)
                addToMask(Assignments[i], 4);
            return mask == 0xff;
        }
    }
}
