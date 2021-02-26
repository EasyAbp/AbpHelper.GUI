using System.Linq;
using AutoMapper;
using EasyAbp.AbpHelper.Core.Commands.Generate.Controller;
using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using EasyAbp.AbpHelper.Core.Commands.Generate.Localization;
using EasyAbp.AbpHelper.Core.Commands.Generate.Methods;
using EasyAbp.AbpHelper.Core.Commands.Generate.Service;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization.Dtos;

namespace EasyAbp.AbpHelper.Gui
{
    public class GuiApplicationAutoMapperProfile : Profile
    {
        public GuiApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<AbpHelperGenerateCrudInput, CrudCommandOption>()
                .ForMember(dest => dest.Exclude,
                    opt => opt.MapFrom(src => src.Exclude.SplitBySpace().ToList()));
            
            CreateMap<AbpHelperGenerateAppServiceClassInput, ServiceCommandOption>()
                .ForMember(dest => dest.Exclude,
                    opt => opt.MapFrom(src => src.Exclude.SplitBySpace().ToList()));
            
            CreateMap<AbpHelperGenerateAppServiceMethodsInput, MethodsCommandOption>()
                .ForMember(dest => dest.Exclude,
                    opt => opt.MapFrom(src => src.Exclude.SplitBySpace().ToList()))
                .ForMember(dest => dest.MethodNames,
                    opt => opt.MapFrom(src => src.MethodNames.SplitBySpace().ToList()));
            
            CreateMap<AbpHelperGenerateControllerInput, ControllerCommandOption>()
                .ForMember(dest => dest.Exclude,
                    opt => opt.MapFrom(src => src.Exclude.SplitBySpace().ToList()));
            
            CreateMap<AbpHelperGenerateLocalizationItemsInput, LocalizationCommandOption>()
                .ForMember(dest => dest.Exclude,
                    opt => opt.MapFrom(src => src.Exclude.SplitBySpace().ToList()))
                .ForMember(dest => dest.Names,
                    opt => opt.MapFrom(src => src.Names.SplitBySpace().ToList()));
        }
    }
}
