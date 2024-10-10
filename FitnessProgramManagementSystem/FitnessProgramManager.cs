using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem
{
    internal class FitnessProgramManager
    {
        public List<FitnessProgram> FitnessPrograms;

        public FitnessProgramManager()
        {
            FitnessPrograms = new List<FitnessProgram>();
        }

        public void CreateFitnessProgram(int id, string title, string duration, decimal price)
        {
            FitnessProgram program = new FitnessProgram(id,title,duration,price);
            FitnessPrograms.Add(program);
            Console.WriteLine("Program created successfully");
        }

        public void ReadFitnessPrograms() 
        {
            if(FitnessPrograms.Count == 0)
            {
                Console.WriteLine("No programs available");
            }
            else
            {
                Console.WriteLine("Programs lists: ");
                foreach(var program in FitnessPrograms)
                {
                    Console.WriteLine(program.ToString());
                }
            }
        }

        public void UpdateFitnessProgram(int id, string title, string duration, decimal price)
        {
            var findedProgram = FitnessPrograms.Find(p=>p.FitnessProgramId == id);
            if (findedProgram != null)
            {
                findedProgram.Title = title;
                findedProgram.Duration = duration;
                findedProgram.Price = price;
                Console.WriteLine("Program updated successfully");
            }
            else
            {
                Console.WriteLine("No program found");
            }
        }
        public void DeleteFitnessProgram(int id)
        {
            var findedProgram = FitnessPrograms.Find(p => p.FitnessProgramId == id);
            if (findedProgram != null)
            {
                FitnessPrograms.Remove(findedProgram);
                Console.WriteLine("Program deleted successfully");


            }
            else
            {
                Console.WriteLine("No program found");
            }

        }

    }
}
