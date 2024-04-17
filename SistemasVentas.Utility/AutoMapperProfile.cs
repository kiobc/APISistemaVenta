using System;
using System.Collections.Generic;
using System.Globalization;
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

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Producto
            CreateMap<Producto, ProductoDTO>()
            .ForMember(destino =>
             destino.DescripcionCategoria,
             opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre))
            .ForMember(destino =>
             destino.Precio,
             opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE"))))
            .ForMember(destino =>
             destino.EsActivo,
             opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));


            CreateMap<ProductoDTO, Producto>()
     .ForMember(destino =>
      destino.IdCategoriaNavigation,
      opt => opt.Ignore())
     .ForMember(destino =>
      destino.Precio,
      opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE"))))
     .ForMember(destino =>
      destino.EsActivo,
      opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion Prodcuto


        }
    }
}
