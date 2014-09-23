using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
public class CSDGColumn : DataGridViewColumn
{
    public CSDGColumn() : base(new CSDGCell())
    {
    }

    public override DataGridViewCell CellTemplate
    {
        get { return base.CellTemplate; }
        set
        {
            // Ensure that the cell used for the template is a CSDGCell.
            if (value != null &&
                !value.GetType().IsAssignableFrom(typeof(CSDGCell)))
            {
                throw new InvalidCastException("Must be a CSDGCell");
            }
            base.CellTemplate = value;
        }
    }
}

public class CSDGCell : DataGridViewTextBoxCell
{
    public enum CSDGCellTypeEnum { Combo = 0, Date = 1, Text = 2, Label = 3, ComboText = 4 };
    private CSDGCellTypeEnum csdgCellType = CSDGCellTypeEnum.Text;
    private string[] cboItems;
    public CSDGCell() : base()
    {
    }

    public CSDGCellTypeEnum CSDGCellType
    {
        get { return csdgCellType; }
        set
        {
            csdgCellType = value;
            switch (csdgCellType)
            {
                case CSDGCellTypeEnum.Combo:
                case CSDGCellTypeEnum.ComboText:
                    this.ReadOnly = false;
                    break;
                case CSDGCellTypeEnum.Date:
                    this.ReadOnly = false;
                    this.Style.Format = "d"; //Short Date Format
                    break;
                case CSDGCellTypeEnum.Label:
                    this.ReadOnly = true;
                    break;
                default:
                    this.ReadOnly = false;
                    break;
            }
        }
    }

    public void SetCboItems(string[] itemList)
    {
        cboItems = itemList;
    }

    public String[] GetCboItems()
    {
        return cboItems;
    }

    public void SetCboTextFromIndex(int newIndex)
    {
        this.Value = cboItems[newIndex];
    }

    public void SetCboTextFromChar(string newValue)
    {
        for (int i = 0; i < cboItems.Length; i++)
        {
            if (cboItems[i].StartsWith(newValue) == true)
            {
                this.Value = cboItems[i];
                break;
            }
        }
    }

    public int GetCboIndex()
    {
        int curIndex = -1;
        for (int i = 0; i < cboItems.Length; i++)
        {
            if (this.EditedFormattedValue.ToString() == cboItems[i])
            {
                curIndex = i;
                break;
            }
        }
        return curIndex;
    }

    public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Set the value of the editing control to the current cell value.
        switch (csdgCellType)
        {
            case CSDGCellTypeEnum.Combo:
            case CSDGCellTypeEnum.ComboText:
                base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
                DataGridViewComboBoxEditingControl ctl2 = DataGridView.EditingControl as DataGridViewComboBoxEditingControl;
                ctl2.DropDownStyle = ComboBoxStyle.DropDownList;
                ctl2.Items.Clear();
                ctl2.Items.AddRange(cboItems);
                if (this.Value == null)
                {
                    ctl2.SelectedIndex  = -1;
                }
                else
                {
                    for (int i = 0; i < ctl2.Items.Count; i++)
	                {
		                if (ctl2.Items[i].ToString() == this.Value.ToString())    
                        {
                            ctl2.SelectedIndex = i;
                        }
	                }
                }
                break;
            case CSDGCellTypeEnum.Date:
                base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
                CalendarEditingControl ctl = DataGridView.EditingControl as CalendarEditingControl;
                // Use the default row value when Value property is null.
                if (this.Value == null)
                {
                    ctl.Value = (DateTime)this.DefaultNewRowValue;
                }
                else
                {
                    ctl.Value = Convert.ToDateTime(this.Value);
                }
                break;
            case CSDGCellTypeEnum.Label:
                break;
            default:
                base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
                DataGridViewTextBoxEditingControl ctl1 = DataGridView.EditingControl as DataGridViewTextBoxEditingControl;
                if (this.Value == null)
                {
                    ctl1.Text = "";
                }
                else
                {
                    ctl1.Text = this.Value.ToString();
                }
                break;
        }
    }

    public override Type EditType
    {           // Return the type of the editing control that CSDGCell uses.
        get 
        {
            switch (csdgCellType)
            {
                case CSDGCellTypeEnum.Combo:
                case CSDGCellTypeEnum.ComboText:
                    return typeof(DataGridViewComboBoxEditingControl);
                case CSDGCellTypeEnum.Date: return typeof(CalendarEditingControl);
                case CSDGCellTypeEnum.Label: return null;
                default: return typeof(DataGridViewTextBoxEditingControl); 
            }
        }
    }

    public override Type ValueType
    {           // Return the type of the value that CSDGCell contains.
        get
        {
            switch (csdgCellType)
            {
                case CSDGCellTypeEnum.Combo:
                case CSDGCellTypeEnum.ComboText: 
                    return typeof(Object);
                case CSDGCellTypeEnum.Date: return typeof(DateTime);
                default: return typeof(String);
            }
        }
    }

    public override object DefaultNewRowValue
    {           // Use the current date and time as the default value.
        get { return DateTime.Now; }
    }
}

class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
{
    DataGridView dataGridView;
    private bool valueChanged = false;
    int rowIndex;

    public CalendarEditingControl()
    {
        this.Format = DateTimePickerFormat.Short;
    }

    // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
    // property.
    public object EditingControlFormattedValue
    {
        get { return this.Value.ToShortDateString(); }
        set {
            if (value is String)
            {
                try
                {       // This will throw an exception of the string is null, empty, or not in the format of a date.
                    this.Value = DateTime.Parse((String)value);
                }
                catch
                {       // In the case of an exception, just use the default value so we're not left with a null value.
                    this.Value = DateTime.Now;
                }
            }
        }
    }

    // Implements the 
    // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
    public object GetEditingControlFormattedValue(
        DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    // Implements the 
    // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
    public void ApplyCellStyleToEditingControl(
        DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
        this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
    }

    // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
    // property.
    public int EditingControlRowIndex
    {
        get
        {
            return rowIndex;
        }
        set
        {
            rowIndex = value;
        }
    }

    // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
    // method.
    public bool EditingControlWantsInputKey(
        Keys key, bool dataGridViewWantsInputKey)
    {
        // Let the DateTimePicker handle the keys listed.
        switch (key & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
    // method.
    public void PrepareEditingControlForEdit(bool selectAll)
    {
        // No preparation needs to be done.
    }

    // Implements the IDataGridViewEditingControl
    // .RepositionEditingControlOnValueChange property.
    public bool RepositionEditingControlOnValueChange
    {
        get
        {
            return false;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingControlDataGridView property.
    public DataGridView EditingControlDataGridView
    {
        get
        {
            return dataGridView;
        }
        set
        {
            dataGridView = value;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingControlValueChanged property.
    public bool EditingControlValueChanged
    {
        get
        {
            return valueChanged;
        }
        set
        {
            valueChanged = value;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingPanelCursor property.
    public Cursor EditingPanelCursor
    {
        get
        {
            return base.Cursor;
        }
    }

    protected override void OnValueChanged(EventArgs eventargs)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        valueChanged = true;
        this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        base.OnValueChanged(eventargs);
    }
}

