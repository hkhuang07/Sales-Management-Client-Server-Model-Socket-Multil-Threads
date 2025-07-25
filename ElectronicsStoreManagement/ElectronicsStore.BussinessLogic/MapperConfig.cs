using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ElectronicsStore.BusinessLogic
{
    public static class MapperConfig
    {
        private static IMapper? _mapper;

        public static IMapper Initialize()
        {
            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                _mapper = config.CreateMapper();
            }

            return _mapper;
        }
    }
}
