namespace ClientcardFB3
{
    partial class EditAlertForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

// <Window x:Class="RichTextBoxInputPanelDemo.Window1"
//    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" Height="400" Width="600"
//    >
//  <Grid>

//    <!-- Set the styles for the tool bar. -->
//    <Grid.Resources>
//      <Style TargetType="{x:Type Button}" x:Key="formatTextStyle">
//        <Setter Property="FontFamily" Value="Palatino Linotype"></Setter>
//        <Setter Property="Width" Value="30"></Setter>
//        <Setter Property="FontSize" Value ="14"></Setter>
//        <Setter Property="CommandTarget" Value="{Binding ElementName=mainRTB}"></Setter>
//      </Style>

//      <Style TargetType="{x:Type Button}" x:Key="formatImageStyle">
//        <Setter Property="Width" Value="30"></Setter>
//        <Setter Property="CommandTarget" Value="{Binding ElementName=mainRTB}"></Setter>
//      </Style>
//    </Grid.Resources>

//    <DockPanel Name="mainPanel">

//      <!-- This tool bar contains all the editing buttons. -->
//      <ToolBar Name="mainToolBar" Height="30" DockPanel.Dock="Top">

//        <Button Style="{StaticResource formatImageStyle}" Command="ApplicationCommands.Cut" ToolTip="Cut">
//          <Image Source="Images\EditCut.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="ApplicationCommands.Copy" ToolTip="Copy">
//          <Image Source="Images\EditCopy.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="ApplicationCommands.Paste" ToolTip="Paste">
//          <Image Source="Images\EditPaste.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="ApplicationCommands.Undo" ToolTip="Undo">
//          <Image Source="Images\EditUndo.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="ApplicationCommands.Redo" ToolTip="Redo">
//          <Image Source="Images\EditRedo.png"></Image>
//        </Button>

//        <Button Style="{StaticResource formatTextStyle}" Command="EditingCommands.ToggleBold" ToolTip="Bold">
//          <TextBlock FontWeight="Bold">B</TextBlock>
//        </Button>
//        <Button Style="{StaticResource formatTextStyle}" Command="EditingCommands.ToggleItalic" ToolTip="Italic">
//          <TextBlock FontStyle="Italic" FontWeight="Bold">I</TextBlock>
//        </Button>
//        <Button Style="{StaticResource formatTextStyle}" Command="EditingCommands.ToggleUnderline" ToolTip="Underline">
//          <TextBlock TextDecorations="Underline" FontWeight="Bold">U</TextBlock>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.IncreaseFontSize" ToolTip="Grow Font">
//          <Image Source="Images\CharacterGrowFont.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.DecreaseFontSize" ToolTip="Shrink Font">
//          <Image Source="Images\CharacterShrinkFont.png"></Image>
//        </Button>

//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.ToggleBullets" ToolTip="Bullets">
//          <Image Source="Images\ListBullets.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.ToggleNumbering" ToolTip="Numbering">
//          <Image Source="Images/ListNumbering.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.AlignLeft" ToolTip="Align Left">
//          <Image Source="Images\ParagraphLeftJustify.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.AlignCenter" ToolTip="Align Center">
//          <Image Source="Images\ParagraphCenterJustify.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.AlignRight" ToolTip="Align Right">
//          <Image Source="Images\ParagraphRightJustify.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.AlignJustify" ToolTip="Align Justify">
//          <Image Source="Images\ParagraphFullJustify.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.IncreaseIndentation" ToolTip="Increase Indent">
//          <Image Source="Images\ParagraphIncreaseIndentation.png"></Image>
//        </Button>
//        <Button Style="{StaticResource formatImageStyle}" Command="EditingCommands.DecreaseIndentation" ToolTip="Decrease Indent">
//          <Image Source="Images\ParagraphDecreaseIndentation.png"></Image>
//        </Button>

//      </ToolBar>

//      <!-- By default pressing tab moves focus to the next control. Setting AcceptsTab to true allows the 
//           RichTextBox to accept tab characters. -->
//      <RichTextBox Name="mainRTB" AcceptsTab="True"></RichTextBox>
//    </DockPanel>
//  </Grid>
//</Window>
       /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAlertForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBold = new System.Windows.Forms.ToolStripButton();
            this.tsbItalics = new System.Windows.Forms.ToolStripButton();
            this.tsbUnderLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFont = new System.Windows.Forms.ToolStripButton();
            this.tsbColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.rtbAlertText = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator1,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator2,
            this.tsbBold,
            this.tsbItalics,
            this.tsbUnderLine,
            this.toolStripSeparator3,
            this.tsbFont,
            this.tsbColor,
            this.toolStripSeparator4,
            this.tsbSave,
            this.tsbCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(321, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCut.Image")));
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "Cut";
            this.tsbCut.Click += new System.EventHandler(this.tsbCut_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "toolStripButton2";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbPaste.Image")));
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "toolStripButton3";
            this.tsbPaste.Click += new System.EventHandler(this.tsbPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndo.Image")));
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbUndo.Text = "toolStripButton1";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbRedo.Image")));
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbRedo.Text = "toolStripButton4";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBold
            // 
            this.tsbBold.CheckOnClick = true;
            this.tsbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBold.Image = ((System.Drawing.Image)(resources.GetObject("tsbBold.Image")));
            this.tsbBold.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbBold.Name = "tsbBold";
            this.tsbBold.Size = new System.Drawing.Size(23, 22);
            this.tsbBold.Text = "toolStripButton5";
            this.tsbBold.CheckStateChanged += new System.EventHandler(this.tsbFontStyle_CheckStateChanged);
            // 
            // tsbItalics
            // 
            this.tsbItalics.CheckOnClick = true;
            this.tsbItalics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbItalics.Image = ((System.Drawing.Image)(resources.GetObject("tsbItalics.Image")));
            this.tsbItalics.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbItalics.Name = "tsbItalics";
            this.tsbItalics.Size = new System.Drawing.Size(23, 22);
            this.tsbItalics.Text = "toolStripButton6";
            this.tsbItalics.CheckStateChanged += new System.EventHandler(this.tsbFontStyle_CheckStateChanged);
            // 
            // tsbUnderLine
            // 
            this.tsbUnderLine.CheckOnClick = true;
            this.tsbUnderLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUnderLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbUnderLine.Image")));
            this.tsbUnderLine.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbUnderLine.Name = "tsbUnderLine";
            this.tsbUnderLine.Size = new System.Drawing.Size(23, 22);
            this.tsbUnderLine.Text = "toolStripButton7";
            this.tsbUnderLine.CheckStateChanged += new System.EventHandler(this.tsbFontStyle_CheckStateChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFont
            // 
            this.tsbFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFont.Image = ((System.Drawing.Image)(resources.GetObject("tsbFont.Image")));
            this.tsbFont.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbFont.Name = "tsbFont";
            this.tsbFont.Size = new System.Drawing.Size(23, 22);
            this.tsbFont.Text = "toolStripButton8";
            this.tsbFont.Click += new System.EventHandler(this.tsbFont_Click);
            // 
            // tsbColor
            // 
            this.tsbColor.BackColor = System.Drawing.Color.Black;
            this.tsbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbColor.Image = ((System.Drawing.Image)(resources.GetObject("tsbColor.Image")));
            this.tsbColor.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbColor.Name = "tsbColor";
            this.tsbColor.Size = new System.Drawing.Size(23, 22);
            this.tsbColor.Text = "toolStripButton9";
            this.tsbColor.Click += new System.EventHandler(this.tsbColor_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "toolStripButton10";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Lime;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 20);
            this.tsbCancel.Text = "toolStripButton1";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // rtbAlertText
            // 
            this.rtbAlertText.BackColor = System.Drawing.Color.Snow;
            this.rtbAlertText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAlertText.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbAlertText.ForeColor = System.Drawing.Color.Black;
            this.rtbAlertText.Location = new System.Drawing.Point(0, 25);
            this.rtbAlertText.Name = "rtbAlertText";
            this.rtbAlertText.ShowSelectionMargin = true;
            this.rtbAlertText.Size = new System.Drawing.Size(321, 167);
            this.rtbAlertText.TabIndex = 1;
            this.rtbAlertText.Text = "asdl;klsadl";
            // 
            // EditAlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 192);
            this.Controls.Add(this.rtbAlertText);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EditAlertForm";
            this.Text = "EditAlertForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbBold;
        private System.Windows.Forms.ToolStripButton tsbItalics;
        private System.Windows.Forms.ToolStripButton tsbUnderLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbFont;
        private System.Windows.Forms.ToolStripButton tsbColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.RichTextBox rtbAlertText;
        private System.Windows.Forms.ToolStripButton tsbCancel;
    }
}