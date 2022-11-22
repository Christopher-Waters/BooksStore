using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookToReturnDto>()
               .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
               .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
               .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
               .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
               .ForMember(d => d.AuthorPseudonym, o => o.MapFrom(s => s.Author.AuthorPseudonym))
               .ForMember(d => d.CoverImageUrl, o => o.MapFrom(s => s.CoverImageUrl)).ReverseMap();

            CreateMap<Book, CreateBookDto>()
               .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
               .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
               .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
               .ForMember(d => d.CoverImageUrl, o => o.MapFrom(s => s.CoverImageUrl))
               .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
               .ReverseMap();

            CreateMap<Book, UpdateBookDto>()
               .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
               .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
               .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
               .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
               .ForMember(d => d.CoverImageUrl, o => o.MapFrom(s => s.CoverImageUrl))
               .ReverseMap();   
        }
    }
}