using FontechProject.Domain.Entity;
using FontechProject.Domain.Result;

namespace FontechProject.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Report>
{
    /// <summary>
    /// Проверяется название отчёта: если отчёт с данным названием уже создан, то создать отчёт нельзя
    /// Если у создаваемого отчёта нет id пользователя в БД, то отчёт создать нельзя 
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Report report, User user);
}