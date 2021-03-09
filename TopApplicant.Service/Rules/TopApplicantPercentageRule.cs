using System;
using System.Collections.Generic;
using TopApplicant.Data.Models;

namespace TopApplicant.Service.Rules
{
    public sealed class TopApplicantPercentageRule
    {
        public int RetrieveTopApplicantPercentage(SkillsetModel applicantSkillset, List<ApplicantModel> contenders, SkillsetModel requiredSkillset)
        {
            // Step One: Add Required Skills, Matching Applicant Skills, And Matching Contender Skills To Seperate Lists
            var employerSkillsetRequirements = new List<string>();
            var applicantMatchedSkillset = new List<string>();
            var contenderMatchingSkillsets = new List<List<string>>();

            foreach (var contender in contenders)
            {
                var contenderMatchingSkillset = new List<string>();

                AddMatchingSkillsToApplicantAndContenders(applicantSkillset.FrontendSkillset, contender.Skillset.FrontendSkillset, requiredSkillset.FrontendSkillset, contenderMatchingSkillset);
                AddMatchingSkillsToApplicantAndContenders(applicantSkillset.BackendSkillset, contender.Skillset.BackendSkillset, requiredSkillset.BackendSkillset, contenderMatchingSkillset);
                AddMatchingSkillsToApplicantAndContenders(applicantSkillset.DatabaseSkillset, contender.Skillset.DatabaseSkillset, requiredSkillset.DatabaseSkillset, contenderMatchingSkillset);

                contenderMatchingSkillsets.Add(contenderMatchingSkillset);
            }

            void AddMatchingSkillsToApplicantAndContenders(dynamic applicantSkillset, dynamic contenderSkillset, dynamic requiredSkillset, List<string> contenderMatchingSkillset)
            {
                foreach (var conSkill in contenderSkillset.GetType().GetProperties())
                {
                    foreach (var appSkill in applicantSkillset.GetType().GetProperties())
                    {
                        foreach (var reqSkill in requiredSkillset.GetType().GetProperties())
                        {
                            if (reqSkill.GetValue(requiredSkillset) && !employerSkillsetRequirements.Contains(reqSkill.Name))
                                employerSkillsetRequirements.Add(reqSkill.Name);
                            if (appSkill.Name == reqSkill.Name && appSkill.GetValue(applicantSkillset) && reqSkill.GetValue(requiredSkillset) && !applicantMatchedSkillset.Contains(appSkill.Name))
                                applicantMatchedSkillset.Add(appSkill.Name);
                            if (conSkill.Name == reqSkill.Name && conSkill.GetValue(contenderSkillset) && reqSkill.GetValue(requiredSkillset) && !contenderMatchingSkillset.Contains(conSkill.Name))
                                contenderMatchingSkillset.Add(conSkill.Name);
                        }
                    }
                }
            }

            // Step Two: Find Matching Skill Percentages Of All Contenders And Store Them
            var contenderMatchingPercentages = new List<double>();

            foreach (var contenderMatchingSkillset in contenderMatchingSkillsets)
            {
                var contenderMatchingPercentage = Math.Round((double)contenderMatchingSkillset.Count / employerSkillsetRequirements.Count * 100, 2);

                contenderMatchingPercentages.Add(contenderMatchingPercentage);
            }

            // Step Three: Determine Applicant's Top Applicant Percentage
            var applicantRankNumber = contenderMatchingPercentages.Count;
            var applicantMatchingPercentage = Math.Round((double)applicantMatchedSkillset.Count / employerSkillsetRequirements.Count * 100, 2);

            foreach (var contenderMatchingPercentage in contenderMatchingPercentages)
                if (applicantMatchingPercentage > contenderMatchingPercentage)
                    applicantRankNumber--;

            if (applicantRankNumber < contenderMatchingPercentages.Count * .10)
                return 10;
            if (applicantRankNumber < contenderMatchingPercentages.Count * .25)
                return 25;
            if (applicantRankNumber < contenderMatchingPercentages.Count * .50)
                return 50;
            else
                return 0;
        }
    }
}
