using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResumeSkillsFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> resumes = GenerateDummyResumes(1000);
            Stopwatch stopwatch = new Stopwatch();

            // WITHOUT improvements
            stopwatch.Start();
            List<string[]> skillsSequential = new List<string[]>();
            foreach (string resume in resumes)
            {
                skillsSequential.Add(FindSkillsInResume(resume));
            }
            stopwatch.Stop();
            Console.WriteLine($"WITHOUT improvements: {stopwatch.ElapsedMilliseconds} ms");

            // WITH improvements
            stopwatch.Restart();
            List<string[]> skillsParallel = new List<string[]>(resumes.Count);
            Parallel.ForEach(resumes, (resume) =>
            {
                skillsParallel.Add(FindSkillsInResume(resume));
            });
            stopwatch.Stop();
            Console.WriteLine($"WITH improvements: {stopwatch.ElapsedMilliseconds} ms");
        }

        public static string[] FindSkillsInResume(string resume)
        {
            // A simple regular expression to find words in uppercase or title case, assuming they represent skills
            var regex = new Regex(@"\b([A-Z][a-z]*|[A-Z]+)\b");
            var matches = regex.Matches(resume);
            return matches.Select(m => m.Value).ToArray();
        }

        private static List<string> GenerateDummyResumes(int count)
        {
            var dummyResumes = new List<string>(count);
            var random = new Random();
            var skills = new[] { "C#", "Java", "Python", "JavaScript", "HTML", "CSS", "SQL", "Ruby", "Go", "Swift" };

            for (int i = 0; i < count; i++)
            {
                var resume = "";
                for (int j = 0; j < random.Next(5, 15); j++)
                {
                    resume += skills[random.Next(skills.Length)] + " ";
                }
                dummyResumes.Add(resume);
            }

            return dummyResumes;
        }
    }
}
