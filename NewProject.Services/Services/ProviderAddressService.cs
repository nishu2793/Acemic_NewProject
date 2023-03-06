using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.SignalR;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.Provider;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Services
{
    public class ProviderAddressService : IProviderAddressService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        private readonly IFacebookService _facebookService;
        public ProviderAddressService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
            IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
            IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
            ReadWriteApplicationDbContext readWriteUnitOfWorkSP, IFacebookService facebookService)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
            _facebookService = facebookService;
        }

        public async Task<List<SaveProviderAddressDto>> SaveProviderAddress(SaveProviderAddressDto request)
        {
            Guid Id = Guid.NewGuid();
            var saveProviderAddress = new ProviderAddress()
            {
                AddressId = Id,
                ProviderRegisterid = request.ProviderRegisterid,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = Id,
                Address = request.Address,
                CityId = request.CityId,
                StateId = request.StateId,
                CountryId = request.CountryId,
                ZipCode = request.ZipCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
            };

            await _readWriteUnitOfWork.ProviderAddressRepository.AddAsync(saveProviderAddress);

            await _readWriteUnitOfWork.CommitAsync();

            var data = (from ProviderAddress in _readOnlyUnitOfWork.ProviderAddressRepository.GetAllAsQuerable()
                        where ProviderAddress.AddressId == saveProviderAddress.AddressId
                        select new SaveProviderAddressDto
                        {
                            AddressId = ProviderAddress.AddressId


                        }).ToList();
            return data;
        }

        public async Task<bool> UpdateProviderAddress(UpdateProviderAddressDto request)
        {
            var data = await _readWriteUnitOfWork.ProviderAddressRepository.GetFirstOrDefaultAsync(x => x.AddressId == request.AddressId);

            if (data != null)
            {
                data.Address = request.Address;
                data.CityId = request.CityId; 
                data.StateId = request.StateId;
                data.CountryId = request.CountryId;
                data.ZipCode = request.ZipCode;
                data.Latitude = request.Latitude;
                data.Longitude = request.Longitude;
                data.UpdatedOn = request.UpdatedOn;
                data.UpdatedBy = request.UpdatedBy;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProviderAddress(DeleteProviderAddressDto request)
        {
            var data = await _readWriteUnitOfWork.ProviderAddressRepository.GetFirstOrDefaultAsync(x => x.AddressId == request.AddressId);
            if (data != null)
            {
                data.IsDeleted = true;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<List<GetProviderAddressDto>> GetAllProviderAddress()
        {
            {
                var data = (from addressTB in _readOnlyUnitOfWork.ProviderAddressRepository.GetAllAsQuerable()
                            where addressTB.IsDeleted != true
                            select new GetProviderAddressDto
                            {
                                AddressId = addressTB.AddressId,
                                ProviderRegisterid = addressTB.ProviderRegisterid,
                                Address = addressTB.Address,
                                CityId = addressTB.CityId,
                                StateId = addressTB.StateId,
                                CountryId = addressTB.CountryId,
                                ZipCode = addressTB.ZipCode,
                                Longitude = addressTB.Longitude,
                                Latitude = addressTB.Latitude,
                                CreatedBy = addressTB.CreatedBy,
                                CreatedOn = addressTB.CreatedOn,
                                UpdatedBy = addressTB.UpdatedBy,
                                UpdatedOn = addressTB.UpdatedOn,

                            }).ToList();
                return data;
            }
        }

        public async Task<List<GetProviderAddressDto>> GetProviderAddress(GetProviderAddressDto request)
        {
            var data = (from addressTB in _readOnlyUnitOfWork.ProviderAddressRepository.GetAllAsQuerable()
                        where addressTB.IsDeleted != true && addressTB.AddressId == request.AddressId
                        select new GetProviderAddressDto
                        {
                            AddressId = addressTB.AddressId,
                            ProviderRegisterid = addressTB.ProviderRegisterid,
                            Address = addressTB.Address,
                            CityId = addressTB.CityId,
                            StateId = addressTB.StateId,
                            CountryId = addressTB.CountryId,
                            ZipCode = addressTB.ZipCode,
                            Longitude = addressTB.Longitude,
                            Latitude = addressTB.Latitude,
                            CreatedBy = addressTB.CreatedBy,
                            CreatedOn = addressTB.CreatedOn,
                            UpdatedBy = addressTB.UpdatedBy,
                            UpdatedOn = addressTB.UpdatedOn,

                        }).ToList();
            return data;
        }
    }
}
