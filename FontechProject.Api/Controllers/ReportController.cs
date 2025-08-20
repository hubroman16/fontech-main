using Asp.Versioning;
using FontechProject.Domain.Dto.Report;
using FontechProject.Domain.Interfaces.Services;
using FontechProject.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FontechProject.Api.Controllers;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportController : ControllerBase
{
   private readonly IReportService _reportService;

   public ReportController(IReportService reportService)
   {
      _reportService = reportService;
   }

   [HttpGet("reports/{userId}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReportsAsync(long userId)
   {
      var response = await _reportService.GetReportsAsync(userId);
      if (response.IsSuccess)
      {
         return Ok(response);
      }

      return BadRequest(response);
   } 
   
   [HttpGet("{id}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<BaseResult<ReportDto>>> GetReportByIdAsync(long id)
   {
      var response = await _reportService.GetReportByIdAsync(id);
      if (response.IsSuccess)
      {
         return Ok(response);
      }

      return BadRequest(response);
   }
   /// <summary>
   /// Удаление отчёта с заданными параметрами
   /// </summary>
   /// <param name="dto"></param>
   /// <remarks>
   /// Sample request:
   /// 
   ///     DELETE    
   ///     {
   ///        "id" : "1",
   ///     }
   /// 
   /// </remarks>
   /// <response code="200">Если отчёт удалён</response>
   /// <response code="400">Если отчёт не был удалён</response>
   [HttpDelete("{id}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<BaseResult<ReportDto>>> Delete(long id)
   {
      var response = await _reportService.DeleteReportAsync(id);
      if (response.IsSuccess)
      {
         return Ok(response);
      }

      return BadRequest(response);
   }
   /// <summary>
   /// Создание отчёта с заданными параметрами
   /// </summary>
   /// <param name="dto"></param>
   /// <remarks>
   /// Request for create report:
   /// 
   ///     POST    
   ///     {
   ///        "name" : "Report #1",
   ///        "description" : "Test report",
   ///        "userId" : 1
   ///      }
   /// 
   /// </remarks>
   /// <response code="200">Если отчёт создался</response>
   /// <response code="400">Если отчёт не был создан</response>
   [HttpPost]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody] CreateReportDto dto)
   {
      var response = await _reportService.CreateReportAsync(dto);
      if (response.IsSuccess)
      {
         return Ok(response);
      }

      return BadRequest(response);
   }
   /// <summary>
   /// Обновление отчёта с заданными параметрами
   /// </summary>
   /// <param name="dto"></param>
   /// <remarks>
   /// Sample request:
   /// 
   ///     PUT   
   ///     {
   ///        "userId" : 1,
   ///        "name" : "Report #1",
   ///        "description" : "Test report"
   ///      }
   /// 
   /// </remarks>
   /// <response code="200">Если отчёт обновлён</response>
   /// <response code="400">Если отчёт не обновлён</response>
   [HttpPut]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<BaseResult<ReportDto>>> Update([FromBody] UpdateReportDto dto)
   {
      var response = await _reportService.UpdateReportAsync(dto);
      if (response.IsSuccess)
      {
         return Ok(response);
      }

      return BadRequest(response);
   }
   
}