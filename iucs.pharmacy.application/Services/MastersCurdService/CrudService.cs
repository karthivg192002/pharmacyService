using AutoMapper;
using iucs.pharmacy.application.Dto.ResponseDto;
using iucs.pharmacy.domain.Data;
using iucs.pharmacy.domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace iucs.pharmacy.application.Services.CommonCurdService
{
    public interface ICrudService<TDto, TEntity>
    {
        Task<ServiceResult<TDto>> CreateAsync(TDto dto);
        Task<ServiceResult<TDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<TDto>> UpdateAsync(Guid id, TDto dto);
        Task<ServiceResult<List<TDto>>> GetAllAsync();
    }
    public class CrudService<TDto, TEntity> : ICrudService<TDto, TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _db;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        public CrudService(AppDbContext db, IMapper mapper, ILogger logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual async Task<ServiceResult<TDto>> CreateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);

                _db.Set<TEntity>().Add(entity);
                await _db.SaveChangesAsync();

                return new ServiceResult<TDto>
                {
                    Success = true,
                    Data = _mapper.Map<TDto>(entity)
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DB error");

                return new ServiceResult<TDto>
                {
                    Success = false,
                    Message = "Database error"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");

                return new ServiceResult<TDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public virtual async Task<ServiceResult<TDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return new ServiceResult<TDto>
                    {
                        Success = false,
                        Message = "Record not found"
                    };
                }

                return new ServiceResult<TDto>
                {
                    Success = true,
                    Data = _mapper.Map<TDto>(entity)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById failed");

                return new ServiceResult<TDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public virtual async Task<ServiceResult<TDto>> UpdateAsync(Guid id, TDto dto)
        {
            try
            {
                var entity = await _db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return new ServiceResult<TDto>
                    {
                        Success = false,
                        Message = "Record not found"
                    };
                }

                // map dto into existing tracked entity
                _mapper.Map(dto, entity);

                await _db.SaveChangesAsync();

                return new ServiceResult<TDto>
                {
                    Success = true,
                    Data = _mapper.Map<TDto>(entity)
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DB error in Update");

                return new ServiceResult<TDto>
                {
                    Success = false,
                    Message = "Database error"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update failed");

                return new ServiceResult<TDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public virtual async Task<ServiceResult<List<TDto>>> GetAllAsync()
        {
            var data = await _db.Set<TEntity>().AsNoTracking().ToListAsync();

            return new ServiceResult<List<TDto>>
            {
                Success = true,
                Data = _mapper.Map<List<TDto>>(data)
            };
        }
    }
}
