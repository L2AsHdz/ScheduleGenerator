using ScheduleCore.Models;

namespace ScheduleCore.Services.Parametro;

public class ParametroService
{
    private readonly ScheduleDB context;

    public ParametroService(ScheduleDB context)
    {
        this.context = context;
    }

    public void GetParametros()
    {
        try
        {
            ParameterManager.Instance.DefaultSchedule =
                int.Parse(context.Parametro.Where(p => p.CodigoParametro == 1).FirstOrDefault()!.Valor);

            ParameterManager.Instance.DurationPeriod =
                int.Parse(context.Parametro.Where(p => p.CodigoParametro == 2).FirstOrDefault()!.Valor);

            ParameterManager.Instance.StartHour =
                TimeOnly.Parse(context.Parametro.Where(p => p.CodigoParametro == 3).FirstOrDefault()!.Valor);

            ParameterManager.Instance.EndHour =
                TimeOnly.Parse(context.Parametro.Where(p => p.CodigoParametro == 4).FirstOrDefault()!.Valor);

            ParameterManager.Instance.MinAssignments =
                int.Parse(context.Parametro.Where(p => p.CodigoParametro == 5).FirstOrDefault()!.Valor);


            ParameterManager.Instance.RoomPriority =
                context.Parametro.Where(p => p.CodigoParametro == 6).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.CareerPriority =
                context.Parametro.Where(p => p.CodigoParametro == 7).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.TeacherPriority =
                context.Parametro.Where(p => p.CodigoParametro == 8).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.BudgegPriority =
                context.Parametro.Where(p => p.CodigoParametro == 9).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.CapacityPriority =
                context.Parametro.Where(p => p.CodigoParametro == 10).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.LastSemesterPriority =
                context.Parametro.Where(p => p.CodigoParametro == 11).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.AssignmentsPriority =
                context.Parametro.Where(p => p.CodigoParametro == 12).FirstOrDefault()!.Valor == "1";


            ParameterManager.Instance.JoinCourses =
                context.Parametro.Where(p => p.CodigoParametro == 13).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.IgnoreMinAssignments =
                context.Parametro.Where(p => p.CodigoParametro == 14).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.CreateSections =
                context.Parametro.Where(p => p.CodigoParametro == 15).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.ShowWarnings =
                context.Parametro.Where(p => p.CodigoParametro == 16).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.EvenSemester =
                context.Parametro.Where(p => p.CodigoParametro == 17).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.ObligatoryPriority =
                context.Parametro.Where(p => p.CodigoParametro == 18).FirstOrDefault()!.Valor == "1";
            
            ParameterManager.Instance.IgnoreMaxCapacity =
                context.Parametro.Where(p => p.CodigoParametro == 19).FirstOrDefault()!.Valor == "1";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}