using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3.Tests
{
    // For use in testing in place of the logic form
    public class LoginFormFake : ILoginForm
    {
        public bool Visible
        {
            get { return false; }
            set { }
        }
        public void resetForm()
        {

        }
        public void Close()
        {

        }
    }
}
