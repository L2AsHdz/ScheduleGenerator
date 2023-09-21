namespace ScheduleCore.Models;

public class ParameterManager
{
    private static ParameterManager _instance = null;
    private static readonly object _lock = new();
    
    public int DefaultSchedule { get; set; }
    public int DurationPeriod { get; set; }
    public TimeOnly StartHour { get; set; }
    public TimeOnly EndHour { get; set; }
    public int MinAssignments { get; set; }
    
    public bool RoomPriority { get; set; }
    public bool CareerPriority { get; set; }
    public bool TeacherPriority { get; set; }
    public bool BudgegPriority { get; set; }
    public bool CapacityPriority { get; set; }
    public bool LastSemesterPriority { get; set; }
    public bool AssignmentsPriority { get; set; }
    public bool ObligatoryPriority { get; set; }
    
    public bool JoinCourses { get; set; }
    public bool IgnoreMinAssignments { get; set; }
    public bool CreateSections { get; set; }
    public bool ShowWarnings { get; set; }
    public bool EvenSemester { get; set; }
    public bool IgnoreMaxCapacity { get; set; }
    
    private ParameterManager() { }
    
    public static ParameterManager Instance
    {
        get
        {
            lock (_lock)
            {
                return _instance ??= new ParameterManager();
            }
        }
    }
}