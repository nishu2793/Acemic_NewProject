using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.Provider;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Services
{
    public class MachineService : IMachineService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public MachineService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             ReadWriteApplicationDbContext readWriteUnitOfWorkSP)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
        }

        public async Task<List<MachineDto>> GetMachine(MachineDto request)
        {
            var data = (from MachineTB in _readOnlyUnitOfWork.MachineRepository.GetAllAsQuerable()

                        where MachineTB.BarcodeNumber == request.BarcodeNumber && MachineTB.Active == true && MachineTB.IsDeleted != true

                        select new MachineDto
                        {
                            Did = MachineTB.Did,
                            ProviderId = MachineTB.ProviderId,
                            Address1 = MachineTB.Address1,
                            Address2 = MachineTB.Address2,
                            City = MachineTB.City,
                            State = MachineTB.State,
                            ZipCode = MachineTB.ZipCode,
                            Latitude = MachineTB.Latitude,
                            Longitude = MachineTB.Longitude,
                            BarcodeNumber = MachineTB.BarcodeNumber,
                            Active = MachineTB.Active,
                            SerialNumber = MachineTB.SerialNumber,
                            Status = MachineTB.Status,
                            CreatedOn = MachineTB.CreatedOn
                        }).ToList();
            return data;
        }


        public async Task<List<MachineDto>> GetAllMachine()
        {
            var data = (from MachineTB in _readOnlyUnitOfWork.MachineRepository.GetAllAsQuerable()
                        where MachineTB.Did != null && MachineTB.IsDeleted != true
                        select new MachineDto
                        {
                            Did = MachineTB.Did,
                            ProviderId = MachineTB.ProviderId,
                            Address1 = MachineTB.Address1,
                            Address2 = MachineTB.Address2,
                            City = MachineTB.City,
                            State = MachineTB.State,
                            ZipCode = MachineTB.ZipCode,
                            Latitude = MachineTB.Latitude,
                            Longitude = MachineTB.Longitude,
                            BarcodeNumber = MachineTB.BarcodeNumber,
                            Active = MachineTB.Active,
                            SerialNumber = MachineTB.SerialNumber,
                            Status = MachineTB.Status,
                            CreatedOn = MachineTB.CreatedOn

                        }).ToList();
            return data;
        }

        public async Task<List<MachineDto>> SaveMachine(MachineDto request)
        {
            Guid Id = Guid.NewGuid();
            var saveMachineData = new MachineTable()
            {
                Did = Id,
                ProviderId = request.ProviderId,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                BarcodeNumber = request.BarcodeNumber,
                Active = request.Active,
                SerialNumber = request.SerialNumber,
                Status = request.Status,
                CreatedOn = DateTime.Now,
                
            };

            await _readWriteUnitOfWork.MachineRepository.AddAsync(saveMachineData);

            await _readWriteUnitOfWork.CommitAsync();

            var data = (from Machine in _readOnlyUnitOfWork.MachineRepository.GetAllAsQuerable()
                        where Machine.Did == saveMachineData.Did
                        select new MachineDto
                        {
                            Did = Machine.Did
                        }).ToList();
            return data;
        }
        public async Task<bool> UpdateMachine(MachineDto request)
        {
            var data = await _readWriteUnitOfWork.MachineRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.Address1 = request.Address1;
                data.Address2 = request.Address2;
                data.City = request.City;
                data.State = request.State;
                data.ZipCode = request.ZipCode;
                data.Latitude = request.Latitude;
                data.Longitude = request.Longitude;
                data.BarcodeNumber = request.BarcodeNumber;
                data.Active = request.Active;
                data.SerialNumber = request.SerialNumber;
                data.Status = request.Status;
                data.UpdatedOn = DateTime.UtcNow;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMachine(DeleteMachineDto request)
        {
            var data = await _readWriteUnitOfWork.MachineRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.IsDeleted = true;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
    }
}
