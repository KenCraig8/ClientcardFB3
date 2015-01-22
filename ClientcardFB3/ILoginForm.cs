using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    public interface ILoginForm
    {
        bool Visible { get; set; }
        void resetForm();
        void Close();
    }
}
