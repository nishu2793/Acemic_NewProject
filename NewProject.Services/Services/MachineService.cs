using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
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

                        where MachineTB.Did == request.Did && MachineTB.Active == true
                        
                        select new MachineDto
                        {
                            Did = MachineTB.Did,
                            Address1 = MachineTB.Address1,
                            Address2 = MachineTB.Address2,
                            City = MachineTB.City,
                            State = MachineTB.State,
                            ZipCode = MachineTB.ZipCode,
                            Latitude = MachineTB.Latitude,
                            Longitude = MachineTB.Longitude,
                            Barcode_Number = MachineTB.Barcode_Number,
                            Active = MachineTB.Active,
                            Serial_Number= MachineTB.Serial_Number,
                            //Admin_Id = MachineTB.Admin_Id,
                            Status = MachineTB.Status
                        }).ToList();
            return data;
        }


        public async Task<List<MachineDto>> GetAllMachine()
        {
            var data = (from MachineTB in _readOnlyUnitOfWork.MachineRepository.GetAllAsQuerable()
                        where MachineTB.Did != null
                        select new MachineDto
                        {

                            Did = MachineTB.Did,
                            Address1 = MachineTB.Address1,
                            Address2 = MachineTB.Address2,
                            City = MachineTB.City,
                            State = MachineTB.State,
                            ZipCode = MachineTB.ZipCode,
                            Latitude = MachineTB.Latitude,
                            Longitude = MachineTB.Longitude,
                            Barcode_Number = MachineTB.Barcode_Number,
                            Active = MachineTB.Active,
                            Serial_Number= MachineTB.Serial_Number,
                            //Admin_Id = MachineTB.Admin_Id,
                            Status = MachineTB.Status

                        }).ToList();
            return data;
        }
    }
}
