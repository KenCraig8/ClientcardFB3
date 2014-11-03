using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{   
    /*I learned to do this kind of thing, where you have multiple variables all realated to each other with a struct. 
    That way all of the info can be passed into other functions easily. 
    If you would prefer not to use the struct, I can change it to work without it.
    Contains information about the email.*/
    public struct EmailInfo
    {
        public string from;
        public string to;
        public string subject;
        public string body;
        public string attachmentPath;
        public bool canceled;
    }
}
