using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    public interface IMainForm
    {
        void refreshHDRoute();

        void setHousehold(int newHhID, int memberId);
    }
}
