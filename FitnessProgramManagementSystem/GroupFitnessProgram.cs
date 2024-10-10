using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem
{
    internal class GroupFitnessProgram : FitnessProgram
    {
        public string ScheduleName { get; set; }
        public int GroupCapacity { get; set; }


        public GroupFitnessProgram(int id, string title, string duration, decimal price, string scheduleName, int groupCapacity) : base(id, title, duration, price)
        {
            ScheduleName = scheduleName;
            this.GroupCapacity = groupCapacity;
        }

        public string DisplayGroupFitnessProgramInfo()
        {
            return $"ScheduleName: {ScheduleName}, GroupCapacity: {GroupCapacity}";
        }

        public override string DisplayFitnessProgramInfo()
        {
            return base.DisplayFitnessProgramInfo() + this.DisplayGroupFitnessProgramInfo();
        }
    }
}
