﻿using RP0;
using System;
using UniLinq;

namespace KerbalConstructionTime
{
    public class AirlaunchPrep : LCProject
    {
        public override string Name => Direction == PrepDirection.Mount ? Name_Mount : Name_Unmount;

        public const string Name_Mount = "Mounting to carrier";
        public const string Name_Unmount = "Unmounting";

        public enum PrepDirection { Mount, Unmount };
        public PrepDirection Direction = PrepDirection.Mount;

        protected override TransactionReasonsRP0 transactionReason => TransactionReasonsRP0.AirLaunchRollout;
        protected override TransactionReasonsRP0 transactionReasonTime => TransactionReasonsRP0.RateAirlaunch;

        public AirlaunchPrep() : base()
        {
            Direction = PrepDirection.Mount;
        }

        public AirlaunchPrep(BuildListVessel vessel, string id)
        {
            Direction = PrepDirection.Mount;
            AssociatedID = id;
            Progress = 0;

            BP = Formula.GetAirlaunchBP(vessel);
            Cost = Formula.GetAirlaunchCost(vessel);
            Mass = vessel.GetTotalMass();
            IsHumanRated = vessel.humanRated;
            VesselBP = vessel.buildPoints + vessel.integrationPoints;
            _lc = vessel.LC;
        }

        public override bool IsReversed => Direction == PrepDirection.Unmount;
        public override bool HasCost => Direction == PrepDirection.Mount;

        public override BuildListVessel.ListType GetListType() => BuildListVessel.ListType.AirLaunch;

        public void SwitchDirection()
        {
            if (Direction == PrepDirection.Mount)
                Direction = PrepDirection.Unmount;
            else
                Direction = PrepDirection.Mount;
        }
    }
}
