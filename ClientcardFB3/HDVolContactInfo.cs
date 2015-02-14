using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    /// <summary>
    /// Stores the info for a voulenteer needed for this HD routes
    /// </summary>
    public class HDVolContactInfo
    {
        public int id;
        public string name;
        public string phone;

        public HDVolContactInfo()
        {
            reset();
        }

        public void reset()
        {
            id = 0;
            name = "";
            phone = "";
        }
    }
}
