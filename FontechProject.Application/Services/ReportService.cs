using AutoMapper;
using FontechProject.Application.Resources;
using FontechProject.Domain.Dto.Report;
using FontechProject.Domain.Entity;
using FontechProject.Domain.Enum;
using FontechProject.Domain.Interfaces.Repositories;
using FontechProject.Domain.Interfaces.Services;
using FontechProject.Domain.Interfaces.Validations;
using FontechProject.Domain.Result;
using FontechProject.Domain.Settings;
using FontechProject.Producer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace FontechProject.Application.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMessageProducer _messageProducer;
    private readonly IOptions<RabbitMqSettings> _rabbitMqOptions;
    private readonly IReportValidator _reportValidator;
    
    public ReportService(IBaseRepository<Report> reportRepository, IBaseRepository<User> userRepository, ILogger logger, 
        IReportValidator reportValidator, IMapper mapper, IMessageProducer messageProducer, 
        IOptions<RabbitMqSettings> rabbitMqOptions)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
        _logger = logger;
        _reportValidator = reportValidator;
        _mapper = mapper;
        _messageProducer = messageProducer;
        _rabbitMqOptions = rabbitMqOptions;
    }
    /// <inheritdoc />
    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] reports;

        try
        {
            reports = await _reportRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreateAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);

            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (!reports.Any())
        {
            _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportsNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }

        return new CollectionResult<ReportDto>()
        {
            Data = reports,
            Count = reports.Length
        };
    }
    /// <inheritdoc />
    public Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? report;
        try
        {
            report = _reportRepository.GetAll()
                .AsEnumerable()
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreateAt.ToLongDateString()))
                .FirstOrDefault(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);

            return Task.FromResult( new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            });
        }
        if (report == null)
        {
            _logger.Warning($"Отчёт с {id} не найден", id);
            return Task.FromResult( new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            });
        }

        return Task.FromResult( new BaseResult<ReportDto>()
        {
            Data = report
        });
    }
    
    /// <inheritdoc />
    
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
        var result = _reportValidator.CreateValidator(report, user);
        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        report = new Report()
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = user.Id
        };
        await _reportRepository.CreateAsync(report);
        await _reportRepository.SaveChangesAsync();
        var savedReport = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
        //_messageProducer.SendMessage(savedReport, _rabbitMqOptions.Value.RoutingKey, _rabbitMqOptions.Value.ExchangeName);
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(savedReport)
        };
    }

    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
    {
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        var result = _reportValidator.ValidateOnNull(report);
        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        _reportRepository.Remove(report);
        await _userRepository.SaveChangesAsync();
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(report)
        };
    }

    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
    {
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
        var result = _reportValidator.ValidateOnNull(report);
        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        report.Name = dto.Name;
        report.Description = dto.Description;
        var updatedReport = _reportRepository.Update(report);
        await _reportRepository.SaveChangesAsync();
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(updatedReport)
        };
    }
}