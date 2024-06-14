using Microsoft.EntityFrameworkCore;
using PR_3.Models;
using System;

namespace PR_3.Models
{
    public class ProgramEnrolleeExample
    {
        public static void Main(string[] args)
        {

            var example = new ProgramEnrolleeExample();


            Console.WriteLine("Введите название программы обучения:");
            string programName = Console.ReadLine();


            example.GetEnrolleesForProgram(programName);
        }

        public void GetEnrolleesForProgram(string programName)
        {
            using (var context = new Pr3Context())
            {
                var enrolleesForProgram = (
                    from enrollee in context.Enrollees
                    join programEnrollee in context.ProgramEnrollees on enrollee.EnrolleeId equals programEnrollee.EnrolleeId
                    join program in context.Programs on programEnrollee.ProgramId equals program.ProgramId
                    where program.NameProgram == programName
                    select enrollee
                ).ToList();

                foreach (var enrollee in enrolleesForProgram)
                {
                    Console.WriteLine($"Enrollee ID: {enrollee.EnrolleeId}, Name: {enrollee.NameEnrollee}");
                }
            }
        }
    }
}

namespace PR_3.Models
{
    public class ProgramsWithRequiredSubject
    {
        public static void Main1(string[] args)
        {

            var example = new ProgramsWithRequiredSubject();


            Console.WriteLine("Введите название предмета ЕГЭ:");
            string subjectName = Console.ReadLine();


            example.GetProgramsForSubject(subjectName);
        }

        public void GetProgramsForSubject(string subjectName)
        {
            using (var context = new Pr3Context())
            {
                var programsWithRequiredSubject = (
                    from program in context.Programs
                    join programSubject in context.ProgramSubjects on program.ProgramId equals programSubject.ProgramId
                    join subject in context.Subjects on programSubject.SubjectId equals subject.SubjectId
                    where subject.NameSubject == subjectName
                    select program
                ).Distinct().ToList();

                if (programsWithRequiredSubject.Any())
                {
                    Console.WriteLine($"Образовательные программы, на которые для поступления необходим предмет ЕГЭ '{subjectName}':");
                    foreach (var program in programsWithRequiredSubject)
                    {
                        Console.WriteLine($"- {program.NameProgram}");
                    }
                }
                else
                {
                    Console.WriteLine($"Нет образовательных программ, на которые для поступления необходим предмет ЕГЭ '{subjectName}'.");
                }
            }
        }
    }
}

namespace PR_3.Models
{
    public class EgeSubjectStatistics
    {
        public static void Main2(string[] args)
        {

            Console.WriteLine("Введите название предмета ЕГЭ:");
            string subjectName = Console.ReadLine();


            using (var context = new Pr3Context())
            {
                var subjectStatistics = (
                    from enrolleeSubject in context.EnrolleeSubjects
                    where enrolleeSubject.Subject.NameSubject.ToLower() == subjectName.ToLower()
                    group enrolleeSubject by enrolleeSubject.Subject.NameSubject into subjectGroup
                    select new
                    {
                        SubjectName = subjectGroup.Key,
                        MinScore = subjectGroup.Min(es => es.Result),
                        MaxScore = subjectGroup.Max(es => es.Result),
                        EnrolleeCount = subjectGroup.Select(es => es.EnrolleeId).Distinct().Count()
                    }
                ).FirstOrDefault();

                if (subjectStatistics != null)
                {
                    Console.WriteLine($"Предмет: {subjectStatistics.SubjectName}");
                    Console.WriteLine($"Минимальный балл: {subjectStatistics.MinScore}");
                    Console.WriteLine($"Максимальный балл: {subjectStatistics.MaxScore}");
                    Console.WriteLine($"Количество абитуриентов: {subjectStatistics.EnrolleeCount}");
                }
                else
                {
                    Console.WriteLine("Предмет не найден.");
                }
            }
        }
    }
}




namespace PR_3
{
    class Program2
    {
        static void Main3(string[] args)
        {
            Console.WriteLine("Введите минимальный балл:");
            int minScore = Convert.ToInt32(Console.ReadLine());

            using (var context = new Pr3Context())
            {
                var programSubjects = context.ProgramSubjects
                    .Include(ps => ps.Program)
                    .Include(ps => ps.Subject)
                    .Where(ps => ps.MinResult > minScore)
                    .Select(ps => new
                    {
                        ProgramName = ps.Program.NameProgram,
                        SubjectName = ps.Subject.NameSubject,
                        MinResult = ps.MinResult
                    })
                    .ToList();

                foreach (var ps in programSubjects)
                {
                    Console.WriteLine($"Программа: {ps.ProgramName}, Предмет: {ps.SubjectName}, Минимальный балл: {ps.MinResult}");
                }
            }
        }
    }
}

namespace PR_3
{
    class Program3
    {
        static void Main4(string[] args)
        {
            using (var context = new Pr3Context())
            {
                var maxPlan = context.Programs.Max(p => p.Plan);

                var programsWithMaxPlan = context.Programs
                    .Where(p => p.Plan == maxPlan)
                    .ToList();

                if (programsWithMaxPlan.Any())
                {
                    foreach (var program in programsWithMaxPlan)
                    {
                        Console.WriteLine($"Образовательная программа: {program.NameProgram}, План набора: {program.Plan}");
                    }
                }
            }
        }
    }
}

namespace PR_3
{
    class Program4
    {
        static void Main5(string[] args)
        {
            using (var context = new Pr3Context())
            {
                var additionalScores = context.Enrollees
                    .Include(e => e.EnrolleeAchievements)
                    .Select(e => new
                    {
                        EnrolleeName = e.NameEnrollee,
                        TotalAdditionalScore = e.EnrolleeAchievements.Sum(ea => ea.Achievement.Bonus)
                    })
                    .ToList();

                foreach (var score in additionalScores)
                {
                    var roundedScore = Math.Min(score.TotalAdditionalScore, 10);

                    Console.WriteLine($"Абитуриент: {score.EnrolleeName}, Дополнительные баллы: {roundedScore}");
                }
            }
        }
    }
}

namespace PR_3
{
    class Program5
    {
        static void Main6(string[] args)
        {
            using (var context = new Pr3Context())
            {
                var programApplications = context.ProgramEnrollees
                    .GroupBy(pe => pe.ProgramId)
                    .Select(g => new
                    {
                        ProgramId = g.Key,
                        ProgramName = g.FirstOrDefault().Program.NameProgram,
                        ApplicantsCount = g.Count()
                    })
                    .ToList();

                foreach (var application in programApplications)
                {
                    Console.WriteLine($"Программа: {application.ProgramName}, Количество абитуриентов: {application.ApplicantsCount}");
                }
            }
        }
    }
}

namespace PR_3
{
    class Program6
    {
        static void Main7(string[] args)
        {
            using (var context = new Pr3Context())
            {
                Console.WriteLine("Введите название первого предмета ЕГЭ:");
                string subject1 = Console.ReadLine();

                Console.WriteLine("Введите название второго предмета ЕГЭ:");
                string subject2 = Console.ReadLine();

                var programs = context.Programs
                    .Include(p => p.ProgramSubjects)
                    .Where(p => p.ProgramSubjects.Count(ps => ps.Subject.NameSubject == subject1 || ps.Subject.NameSubject == subject2) == 2)
                    .Select(p => new
                    {
                        ProgramName = p.NameProgram
                    })
                    .ToList();

                foreach (var program in programs)
                {
                    Console.WriteLine($"Образовательная программа: {program.ProgramName}");
                }
            }
        }
    }
}

    class Prog7
    {
        static void Main8(string[] args)
        {
            using (var context = new Pr3Context())
            {
                var programs = context.Programs
                    .Include(p => p.ProgramSubjects)
                    .ToList();

                var enrollees = context.Enrollees
                    .Include(e => e.EnrolleeSubjects)
                    .ToList();

                foreach (var program in programs)
                {
                    Console.WriteLine($"Образовательная программа: {program.NameProgram}");

                    foreach (var enrollee in enrollees)
                    {
                        var totalScore = program.ProgramSubjects.Sum(ps =>
                        {
                            var enrolleeSubject = enrollee.EnrolleeSubjects.FirstOrDefault(es => es.SubjectId == ps.SubjectId);
                            return enrolleeSubject != null ? enrolleeSubject.Result : 0;
                        });

                        Console.WriteLine($"Абитуриент: {enrollee.NameEnrollee}, Количество баллов: {totalScore}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }

    class Prog8
    {
        static void Main9F(string[] args)
        {
            using (var context = new Pr3Context())
            {
               var programs = context.Programs
                   .Include(p => p.ProgramSubjects)
                   .ToList();

               var enrollees = context.Enrollees
                   .Include(e => e.EnrolleeSubjects)
                   .ToList();

                foreach (var enrollee in enrollees)
                {
                    Console.WriteLine($"Абитуриент зачислен: {enrollee.NameEnrollee}");

                    foreach (var program in programs)
                    {
                        bool canBeEnrolled = program.ProgramSubjects.All(ps =>
                        {
                            var enrolleeSubject = enrollee.EnrolleeSubjects.FirstOrDefault(es => es.SubjectId == ps.SubjectId);
                            return enrolleeSubject != null && enrolleeSubject.Result >= ps.MinResult;
                        });

                        if (!canBeEnrolled)
                        {
                            Console.WriteLine($"Не может быть зачислен на программу: {program.NameProgram}");
                        }
                    }

                    Console.WriteLine();
                }
            }
        }
    }
