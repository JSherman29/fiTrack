using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using SQLite;
using System.Linq;

namespace fiTrack
{
    class DataAccess
    {
        private const string dbName = "fittrack_db.db3";

        private static SQLiteConnection db = null;

        private static SQLiteConnection Database
        {
            get
            {
                if (db == null)
                    db = new SQLiteConnection(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName));

                return db;
            }
        }

        public static void CreateTables()
        {
            Database.CreateTable<Exercise>();
            Database.CreateTable<Routine>();
            Database.CreateTable<RoutineMapping>();
        }

        #region Exercise Methods

        public static int SaveExercise(Exercise exercise)
        {
            try
            {
                return Database.Insert(exercise);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public static List<Exercise> GetExercises()
        {
            try
            {
                return Database.Table<Exercise>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Exercise>();
        }

        public static List<Exercise> GetRoutineExercises(Routine routine)
        {
            List<Exercise> exercises = new List<Exercise>();

            try
            {
                List<RoutineMapping> mappings =
                    Database.Query<RoutineMapping>($"SELECT * FROM RoutineMappings WHERE RoutineId = {routine.Id} ORDER BY Order;");

                foreach (RoutineMapping mapping in mappings)
                {
                    exercises.Add(GetExerciseById(mapping.ExerciseId));
                }

                return exercises;
            }
            catch (Exception ex)
            {

            }

            return exercises;
        }

        public static Exercise GetExerciseById(int id)
        {
            try
            {
                return Database.Get<Exercise>(id);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static void DeleteExercise(int id)
        {
            try
            {
                Database.Delete<Exercise>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateExercise(Exercise exercise)
        {
            try
            {
                Database.Update(exercise);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Exercise Methods

        #region Routine Methods

        public static void SaveRoutine(Routine routine)
        {
            try
            {
                Database.Insert(routine);
            }
            catch (Exception ex)
            {

            }
        }

        public static void UpdateRoutine(Routine routine)
        {
            try
            {
                Database.Update(routine);
            }
            catch (Exception ex)
            {

            }
        }

        public static void DeleteRoutine(Routine routine)
        {
            try
            {
                Database.Execute($"DELETE FROM RoutineMappings WHERE RoutineId = {routine.Id};");
                Database.Delete(routine);
            }
            catch (Exception ex)
            {

            }
        }

        public static List<Routine> GetRoutines()
        {
            try
            {
                return Database.Table<Routine>().ToList();
            }
            catch (Exception ex)
            {

            }

            return new List<Routine>();
        }

        public static void SaveMapping(RoutineMapping mapping)
        {
            try
            {
                Database.Insert(mapping);
            }
            catch (Exception ex)
            {

            }
        }

        public static void DeleteMapping(RoutineMapping mapping)
        {
            try
            {
                Database.Execute($"DELETE FROM RoutineMappings WHERE RoutineId = {mapping.RoutineId} AND ExerciseId = {mapping.ExerciseId};");
            }
            catch (Exception ex)
            {

            }
        }

        public static Routine GetTodaysRoutine()
        {
            try
            {
                ScheduleOrdering ordering = Database.Query<ScheduleOrdering>($"SELECT * FROM Cycle WHERE Order = {GetCurrentWeek()};").FirstOrDefault();
                Schedule schedule = Database.Query<Schedule>($"SELECT * FROM Schedule WHERE ScheduleId = {ordering.ScheduleId};").FirstOrDefault();

                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return Database.Get<Routine>(schedule.Sunday);
                    case DayOfWeek.Monday:
                        return Database.Get<Routine>(schedule.Monday);
                    case DayOfWeek.Tuesday:
                        return Database.Get<Routine>(schedule.Tuesday);
                    case DayOfWeek.Wednesday:
                        return Database.Get<Routine>(schedule.Wednesday);
                    case DayOfWeek.Thursday:
                        return Database.Get<Routine>(schedule.Thursday);
                    case DayOfWeek.Friday:
                        return Database.Get<Routine>(schedule.Friday);
                    case DayOfWeek.Saturday:
                        return Database.Get<Routine>(schedule.Saturday);
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        #endregion Routine Methods

        public static (int? Max, int? Last, int? Mode) GetMaxLastModeWeight(int id)
        {
            try
            {
                List<Set> sets = Database.Query<Set>($"SELECT * FROM Journal WHERE EID = {id} ORDER BY Date DESC;");
                int? max = sets.Max(x => x.Weight);
                int? last = sets[0].Weight;
                int? mode = sets.GroupBy(x => x.Weight).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();

                return (max, last, mode);
            }
            catch (Exception ex)
            {

            }

            return (0, 0, 0);
        }

        public static int GetCurrentWeek()
        {
            try
            {
                Configuration config = Database.Table<Configuration>().FirstOrDefault();
                TimeSpan span = DateTime.Now.Subtract(config.CycleStart);
                return (span.Days / 7) % config.CycleLength + 1;
            }
            catch (Exception ex)
            {

            }

            return -1;
        }

        public static void SaveConfiguration(Configuration config)
        {
            try
            {
                Database.DeleteAll<Configuration>();
                Database.Insert(config);
            }
            catch (Exception ex)
            {

            }
        }

        public static Configuration GetConfiguration()
        {
            try
            {
                return Database.Table<Configuration>().FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
