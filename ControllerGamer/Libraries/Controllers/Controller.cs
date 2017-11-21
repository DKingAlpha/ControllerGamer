using ControllerGamer.Libraries.ProfileLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControllerGamer.Libraries.Controllers
{


    public delegate void ControllerEventHandler(ControllerEventArgs e);

    internal interface Controller
    {

        string GetDetail();
        string GetProductName();

        bool Start();

        bool Stop();

        void Dispose();

        void MapToProfile(Profile profile);

        void UnMapToProfile(Profile profile);

    }
}
