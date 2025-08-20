using FontechProject.Domain.Dto.Report;
using FontechProject.Domain.Result;

namespace FontechProject.Domain.Interfaces.Services;
/// <summary>
/// Сервис, отвечающий за работу доменной части отчета
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Метод для получения отчётов пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

    /// <summary>
    /// Получение отчёта по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);
    
    /// <summary>
    /// Создание отчёта с заданными параметрами
    /// </summary>
    /// <param name="dto"></param>
    ///<returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);

    /// <summary>
    /// Удаление отчёта по индетификатору
    /// </summary>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> DeleteReportAsync(long id);
    
    /// <summary>
    /// Обновление отчёта 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}