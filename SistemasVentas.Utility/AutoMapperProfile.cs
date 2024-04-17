using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVentas.DTO;
using SistemaVentas.Model;

namespace SistemasVentas.Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
            .ForMember(destino =>
             destino.RolDescripcion,
             opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre))
            .ForMember(destino =>
             destino.EsActivo,
             opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<Usuario, SesionDTO>()
            .ForMember(destino =>
             destino.RolDescripcion,
             opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre));

            CreateMap<UsuarioDTO, Usuario>()
            .ForMember(destino =>
             destino.IdRolNavigation,
             opt => opt.Ignore())
            .ForMember(destino =>
             destino.EsActivo,
             opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion Usuario



        }
    }
}
