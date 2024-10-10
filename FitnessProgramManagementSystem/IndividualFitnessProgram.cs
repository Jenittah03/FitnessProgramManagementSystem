using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem
{
    internal class IndividualFitnessProgram : FitnessProgram
    {
        public string SubscriptionType { get; set; }
        public bool needPersonalTrainer { get; set; }

        public IndividualFitnessProgram(int id, string title, string duration, decimal price,string subscriptionType, bool needPersonalTrainer):base(id,title,duration,price)
        {
            SubscriptionType = subscriptionType;
            this.needPersonalTrainer = needPersonalTrainer;
        }

        public string DisplayIndividualFitnessProgramInfo()
        {
            string personalTrainer = needPersonalTrainer ? "Yes" : "No";
            return $"SubscriptionType: {SubscriptionType}, NeedPersonalTrainer: {personalTrainer}";
        }

        public  override string DisplayFitnessProgramInfo()
        {
            return base.DisplayFitnessProgramInfo() + this.DisplayIndividualFitnessProgramInfo();
        }
    }
}
