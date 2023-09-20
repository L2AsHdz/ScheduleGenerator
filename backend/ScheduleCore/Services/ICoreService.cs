using ScheduleCore.Models;
using ScheduleCore.Models.ViewModels;

namespace ScheduleCore.Services;

public interface ICoreService
{
    IEnumerable<CursoHorarioViewModel> Execute();
}