using AutoMapper;
using Catalog.BLL.DTOs;
using Catalog.DAL.Entities;

namespace Catalog.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email));

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.employeeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.position))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.department))
                .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords));

            CreateMap<MedicalRecord, MedicalRecordDTO>()
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.recordId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.employeeId))
                .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.history))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.DeseaseType, opt => opt.MapFrom(src => src.deseaseType))
                .ForMember(dest => dest.LastCheckup, opt => opt.MapFrom(src => src.lastCheckup))
                .ForMember(dest => dest.MedicalCheckups, opt => opt.MapFrom(src => src.MedicalCheckups));

            CreateMap<MedicalCheckup, MedicalCheckupDTO>()
                .ForMember(dest => dest.CheckupId, opt => opt.MapFrom(src => src.checkupId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.doctor))
                .ForMember(dest => dest.Conclusion, opt => opt.MapFrom(src => src.conclusion));
        }
    }
}