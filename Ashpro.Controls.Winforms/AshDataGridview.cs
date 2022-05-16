using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Ashpro.Controls.Winforms
{
    public partial class AshDataGridview : DataGridView
    {
        #region Private Variable
        DataGridViewComboBoxEditingControl cb;
        #endregion

        #region Constructor
        public AshDataGridview()
        {
            DoubleBuffered = true;
            InitializeComponent();
            this.DefaultCellStyle.SelectionBackColor = Color.LightPink;
        }
        #endregion
        #region Public Property
        public Keys LastKey
        {
            get;
            set;
        }
        public bool EnterKeyNavigation { get; set; }
        protected override void InitLayout()
        {
            base.InitLayout();
            this.EnableHeadersVisualStyles = false;
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
        }
        #endregion

        #region Override Methods
        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.FormatException)
            {
                this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                this.Rows[e.RowIndex].ErrorText = "Invalid input for column " + this.Columns[e.ColumnIndex].HeaderText;
                e.ThrowException = false;
            }
            else
            {
                base.OnDataError(displayErrorDialogIfNoHandler, e);
            }
            //if (!e.Cancel)
            //{
            //    this.Rows[e.RowIndex].ErrorText = "";
            //    MessageBox.Show("Invalid Data! ");
            //    ExceptionManager.Publish(e.Exception);
            //    this.Rows[e.RowIndex].ErrorText = "Invalid Data! " + e.Exception.Message;
            //    e.ThrowException = false;
            //}

        }
        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            this.Rows[e.RowIndex].ErrorText = "";
            base.OnCellValidating(e);
        }
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {

            base.OnEditingControlShowing(e);
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                cb = (DataGridViewComboBoxEditingControl)e.Control;
                if (cb != null)
                {


                }

            }
        }
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellEnter(e);
            this.CurrentCell.Style.BackColor = Color.LightBlue;
            if (EnterKeyNavigation)
            {
                if (this[e.ColumnIndex, e.RowIndex].ReadOnly)
                    SendKeys.Send("{TAB}");
            }
        }
        protected override void OnCellLeave(DataGridViewCellEventArgs e)
        {
            this.CurrentCell.Style.BackColor = Color.White;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.Rows.Count > 0)
            {
                Keys key = (keyData & Keys.KeyCode);
                if (key == Keys.Up || key == Keys.Down)
                {
                    return base.ProcessDialogKey(keyData);
                }
                else
                if (key == Keys.Left)
                {
                    int col = this.CurrentCell.ColumnIndex - 1;
                    for (; col >= 0; col--)
                    {
                        if (!this.Columns[col].ReadOnly && this.Columns[col].Visible)
                        { break; }
                    }
                    if (col < 0) { return true; }
                    try
                    {
                        this.CurrentCell = this.Rows[this.CurrentCell.RowIndex].Cells[col];
                    }
                    catch { }

                    return true;
                }
                if (key == Keys.Right || key == Keys.Tab || key == Keys.Enter) //Move Forward
                {
                    Trace.WriteLine("on dilog key");
                    if (key == Keys.Enter)
                    {
                        string sCurrentColumnName = this.CurrentCell.OwningColumn.Name;
                        if (sCurrentColumnName == "col_Code")
                        {
                            return false; //base.ProcessDialogKey(keyData);
                        }

                    }
                    int col = this.CurrentCell.ColumnIndex + 1;
                    for (; col < this.Columns.Count; col++) //Get next valid column
                    {
                        if (!this.Columns[col].ReadOnly && this.Columns[col].Visible)
                        { break; }
                    }
                    if (col < this.Columns.Count) // Next valid cell found in same row
                    {
                        if (col < 0) { return true; }
                        try
                        {
                            this.CurrentCell = this.Rows[this.CurrentCell.RowIndex].Cells[col];
                        }
                        catch { }
                    }
                    else //Next valid cell is in next row
                    {

                        if (this.CurrentCell.RowIndex != this.Rows.Count - 1) //Not Last Row
                        {

                            for (col = 0; col <= this.CurrentCell.ColumnIndex; col++) //Get first valid column
                            {
                                if (!this.Columns[col].ReadOnly && this.Columns[col].Visible)
                                {
                                    break;
                                }
                            }
                            if (col <= this.CurrentCell.ColumnIndex)
                            {
                                if (col < 0) { return true; }
                                try
                                {
                                    this.CurrentCell = this.Rows[this.CurrentCell.RowIndex + 1].Cells[col];
                                }
                                catch { }
                            }
                        }
                        else //Last Row
                        {
                            return base.ProcessDialogKey(keyData);
                        }
                    }

                    return true;
                }
                return base.ProcessDialogKey(keyData);
            }
            else
                return false;
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            try
            {
                if (this.Rows.Count > 0)
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        return base.ProcessDataGridViewKey(e);
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        int col = this.CurrentCell.ColumnIndex - 1;
                        for (; col >= 0; col--)
                        {
                            if (!this.Columns[col].ReadOnly && this.Columns[col].Visible)
                            { break; }
                        }
                        if (col < 0) { return true; }
                        try
                        {
                            this.CurrentCell = this.Rows[this.CurrentCell.RowIndex].Cells[col];
                        }
                        catch { }
                        return true;
                        //return base.ProcessDataGridViewKey(e);
                    }
                    else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter || e.KeyData == Keys.Tab)
                    {
                        Trace.WriteLine("on datagridview key");
                        if (e.KeyCode == Keys.Enter)
                        {
                            string sCurrentColumnName = this.CurrentCell.OwningColumn.Name;
                            if (sCurrentColumnName == "col_Code")
                            {
                                return false; //base.ProcessDialogKey(keyData);
                            }
                        }
                        int col = this.CurrentCell.ColumnIndex + 1;
                        for (; col < this.Columns.Count; col++)
                        {
                            if (!this.Columns[col].ReadOnly && this.Columns[col].Visible)
                            { break; }
                        }
                        if (col < this.Columns.Count)
                        {
                            if (col < 0) { return true; }
                            try
                            {
                                this.CurrentCell = this.Rows[this.CurrentCell.RowIndex].Cells[col];
                            }
                            catch { }
                        }
                        else
                        {
                            if (this.CurrentCell.RowIndex != this.Rows.Count - 1)
                            {
                                for (col = 0; col <= this.CurrentCell.ColumnIndex; col++)
                                {
                                    if (!this.Columns[col].ReadOnly && this.Columns[col].Visible)
                                    {
                                        break;
                                    }
                                }
                                if (col <= this.CurrentCell.ColumnIndex)
                                {
                                    if (col < 0) { return true; }
                                    try
                                    {
                                        this.CurrentCell = this.Rows[this.CurrentCell.RowIndex + 1].Cells[col];
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                return base.ProcessDataGridViewKey(e);
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return base.ProcessDataGridViewKey(e);
        }
        int WM_LBUTTONDOWN = 0x0201;
        int WM_LBUTTONDBLCLK = 0x0203;
        int MK_LBUTTON = 0x1;
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK)
                {
                    if (m.WParam.ToInt32() == MK_LBUTTON)
                    {
                        int lparam = m.LParam.ToInt32();
                        int xpos = lparam & 0x0000FFFF;
                        int ypos = lparam >> 16;
                        //if (!IsReadonlyCell(xpos, ypos)) // Commented By Lenin for not able to resize read only columns
                        //{
                        base.WndProc(ref m);
                        //}
                    }
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            catch (Exception)
            {
                base.WndProc(ref m);
            }
        }
        private bool IsReadonlyCell(int xpos, int ypos)
        {
            try
            {
                int column = 0;
                for (; column < this.ColumnCount; column++)
                {

                    if (this.GetColumnDisplayRectangle(column, true).Contains(xpos, ypos))
                    {

                        break;

                    }

                }
                if (column < this.ColumnCount)
                {
                    if (this.Columns[column].ReadOnly)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion
        #region Public Methods
        public void MoveForward()
        {
            SendKeys.Send("{Tab}");
        }
        #endregion
    }

    public class DataGridViewDateTimeCell : DataGridViewTextBoxCell
    {

        public DataGridViewDateTimeCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl =
                DataGridView.EditingControl as CalendarEditingControl;
            // Use the default row value when Value property is null.
            if (this.Value == null)
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = (DateTime)this.Value;
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.

                return typeof(DateTime);
            }
        }
        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }
    public class DataGridViewDateTimeColumn : DataGridViewColumn
    {
        public DataGridViewDateTimeColumn()
        {
            this.CellTemplate = new DataGridViewDateTimeCell();
            this.ReadOnly = false;
        }
    }
}
