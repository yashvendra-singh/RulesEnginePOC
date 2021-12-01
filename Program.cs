using System;
using RulesEnginePOC.Model;
using RulesEngine.Models;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using RulesEngine.Extensions;

namespace RulesEnginePOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var visit1 = new VisitWorklistSummary
            {
                VisitKey = 25,
                AdmitDate = DateTime.Now.AddDays(-3),
                FacilityKey = 1,
                CDIPriorityKey =  -30,
                ChiefComplaint = "Chest Pressure",
                DischargeDate = null,
                PatientKey = 3342,
                PatientName = "Adam Roberts",
                PriorityFactor = new PriorityFactor { CDIPriorityFactorKey = 11}
            };

            var inputs = new dynamic[]
                {
                    visit1
                };

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "Rules.json", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
                throw new Exception("Rules not found.");

            var fileData = File.ReadAllText(files[0]);
            var workflow = JsonConvert.DeserializeObject<List<Workflow>>(fileData);

            var bre = new RulesEngine.RulesEngine(workflow.ToArray(), null);

            List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync("ASDRG", inputs).Result;

            resultList.OnSuccess((eventName) => {
                Console.WriteLine($"ASDRG priorty factor is applicable with factor value {eventName}.");
            });

            resultList.OnFail(() => {
                Console.WriteLine(resultList[0].ExceptionMessage);
            });
        }
    }
}
