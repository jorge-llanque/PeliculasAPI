﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTO
{
    public class PeliculasIndexDTO
    {
        public List<PeliculaDTO> FuturosEstrenos { get; set; }
        public List<PeliculaDTO> EnCines { get; set; }
    }
}
