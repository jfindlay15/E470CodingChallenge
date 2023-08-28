using AutoMapper;
using CodingExercise.Dtos;
using CodingExercise.Models;

namespace CodingExercise.Mapper
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleDto, Vehicle>().ReverseMap();
        }
    }
}
