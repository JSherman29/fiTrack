using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace fiTrack
{
    [Table("Exercises")]
    public class Exercise
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string PrimaryMuscle { get; set; }
        public bool HasWeight { get; set; }
        public bool HasReps { get; set; }
        public bool HasTime { get; set; }
        public bool HasDistance { get; set; }
    }

    [Table("Routines")]
    public class Routine
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
    }

    [Table("RoutineMappings")]
    public class RoutineMapping
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int RoutineId { get; set; }
        public int ExerciseId { get; set; }
        public int Order { get; set; }
    }

    [Table("Journal")]
    public class Set
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull]
        public DateTime Date { get; set; }
        public int ExerciseId { get; set; }
        public int? Weight { get; set; }
        public int? Reps { get; set; }
        public double? Distance { get; set; }
        public double? Time { get; set; }
    }

    [Table("Schedule")]
    public class Schedule
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int? Sunday { get; set; }
        public int? Monday { get; set; }
        public int? Tuesday { get; set; }
        public int? Wednesday { get; set; }
        public int? Thursday { get; set; }
        public int? Friday { get; set; }
        public int? Saturday { get; set; }
    }

    [Table("Cycle")]
    public class ScheduleOrdering
    {
        public int Order { get; set; }
        public int ScheduleId { get; set; }
    }

    [Table("Configuration")]
    public class Configuration
    {
        public int CycleLength { get; set; }
        public DateTime CycleStart { get; set; }
    }
}
