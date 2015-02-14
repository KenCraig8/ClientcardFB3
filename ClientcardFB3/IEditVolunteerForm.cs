using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public interface IEditVolunteerForm
    {
        int SelectedId { get; set; }

        DialogResult ShowDialog();
    }
}
