using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SigPadCtrl
{
    public partial class SigPadInputCtrl : UserControl
    {
        Font sigFont;
        Boolean havePad = false;
        Boolean haveSignature = false;
        Bitmap signBitMap;
        int lcdMaxWidth;
        
        short widthButtonLeft;
        short widthButtonRight;
        int yPosButtons;
        private const int delta = 2;
        public struct promptItem
        {
            public string Prompt;
            public string RightButton;
        }

        List<promptItem> promptList = new List<promptItem>();
        int promptType = 0;
        int screenId = -1;

        public SigPadInputCtrl()
        {
            InitializeComponent();
            statLabel.Text = "";
        }

        public Boolean HaveSigPad()
        {
            return havePad;
        }
        public int AddPromptItem(string prompt, string rightbutton)
        {
            promptItem tmp = new promptItem();
            tmp.Prompt = prompt;
            tmp.RightButton = rightbutton;
            promptList.Add(tmp);
            return promptList.Count;
        }

        public void ClearPromptList()
        {
            promptList.Clear();
        }

        public Boolean initSigPad()
        {
            havePad = false;
            statLabel.Text = "Initialize Signature Pad";
            if (sigPlusNET1.TabletConnectQuery() == true)
            {
                havePad = true;
                Object rm = Properties.Resources.ResourceManager.GetObject("Sign");
                signBitMap = (Bitmap)rm;
                try
                {
                    sigPlusNET1.SetTabletState(1);
                    sigPlusNET1.ClearTablet();
                    refreshLCD();
                    sigPlusNET1.SetSigString("");
                    sigPlusNET1.SetTimeStamp("");
                    sigPlusNET1.SetDisplayTimeStamp(true);
                    sigPlusNET1.SetTabletState(0);
                    sigFont = new Font("Arial", 9.75F, FontStyle.Regular);
                    BackLight(false);
                    statLabel.Text = "Signature Pad Ready";
                }
                catch (Exception)
                {
                    havePad = false;
                }
            }
            screenId = -1;
            haveSignature = false;
            return havePad;
        }

        public int State
        {
            get { return sigPlusNET1.GetTabletState(); }
        }

        public void StartCapture()
        {
            try
            {
                haveSignature = false;
                sigPlusNET1.SetTabletState(1);     //Turns tablet on to collect signature
                sigPlusNET1.SetLCDCaptureMode(2);  //Sets mode so ink will not disappear after a few seconds
                sigPlusNET1.SetSigString("");
                sigPlusNET1.SetTimeStamp("");
                sigPlusNET1.SetDisplayTimeStamp(true);
                refreshLCD();
                sigPlusNET1.SetSaveSigInfo(true);
                sigPlusNET1.BackColor = Color.LightYellow;
                //Check for LCD size in pixels.
                uint lcdSize = sigPlusNET1.LCDGetLCDSize();
                lcdMaxWidth = (int)(lcdSize & 0xFFFF);
                BackLight(true);
                showPromptScreen(0);
            }
            catch (Exception)
            {
            }
        }

        public void refreshLCD()
        {
            try
            {
                sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            }
            catch (Exception)
            {
            }
        }

        private void setFont(int Height, int Width, int Weight, short Italic, short Underline, float Points, string FontName)
        {
            try
            {
                sigFont = new Font(FontName, Points, FontStyle.Regular);
            }
            catch (Exception)
            {
            }
        }

        private void showPromptScreen(int newScreenId)
        {
            try
            {
                if (newScreenId < promptList.Count)
                {
                    screenId = newScreenId;
                    statLabel.Text = "Prompt " + (screenId + 1).ToString() + " of " + promptList.Count.ToString();
                    sigPlusNET1.ClearTablet();
                    refreshLCD();
                    writeLCDMsg(promptList[screenId].Prompt);
                    //Left Button
                    yPosButtons = 45;
                    widthButtonLeft = Convert.ToInt16(sigPlusNET1.LCDStringWidth(sigFont, "YES") + delta);
                    sigPlusNET1.LCDWriteString(0, 2, 100, yPosButtons, sigFont, "YES");
                    sigPlusNET1.KeyPadAddHotSpot(0, 1, 98, (short)(yPosButtons - 2), widthButtonLeft, 16);
                    //Right Button
                    widthButtonRight = Convert.ToInt16(sigPlusNET1.LCDStringWidth(sigFont, promptList[screenId].RightButton) + delta);
                    sigPlusNET1.LCDWriteString(0, 2, 200, yPosButtons, sigFont, promptList[screenId].RightButton);
                    sigPlusNET1.KeyPadAddHotSpot(1, 1, 198, (short)(yPosButtons - 2), widthButtonRight, 17);

                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.LCDSetWindow(0, 0, 1, 1);
                    sigPlusNET1.SetSigWindow(1, 0, 0, 1, 1); //Sets the area where ink is permitted in the SigPlus object
                    sigPlusNET1.SetTabletState(1);
                    promptType = 0;
                }
                else
                {
                    showSignScreen();
                }
            }
            catch (Exception)
            {
            }
        }

        private void showSignScreen()
        {
            try
            {
                statLabel.Text = "Prompt Signature";
                sigPlusNET1.LCDSendGraphic(1, 2, 0, 18, signBitMap);    //load bmp into background memory for display on lcd
                sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
                sigPlusNET1.ClearTablet();
                sigPlusNET1.KeyPadClearHotSpotList();
                widthButtonLeft = Convert.ToInt16(sigPlusNET1.LCDStringWidth(sigFont, "Clear") + delta);
                yPosButtons = 2;
                sigPlusNET1.LCDWriteString(0, 2, 10, yPosButtons, sigFont, "Clear");
                sigPlusNET1.KeyPadAddHotSpot(2, 1, 10, (short)(yPosButtons - 1), widthButtonLeft, 17); //For CLEAR button
                sigPlusNET1.LCDWriteString(0, 2, 200, yPosButtons, sigFont, "OK");
                widthButtonRight = Convert.ToInt16(sigPlusNET1.LCDStringWidth(sigFont, "OK") + delta);
                sigPlusNET1.KeyPadAddHotSpot(3, 1, 197, (short)(yPosButtons - 1), widthButtonRight, 17); //For OK button


                sigPlusNET1.LCDSetWindow(0, 20, 240, 40);
                sigPlusNET1.SetSigWindow(1, 0, 20, 240, 40); //Sets the area where ink is permitted in the SigPlus object
                sigPlusNET1.SetLCDCaptureMode(2);
                promptType = 1;
            }
            catch (Exception)
            {
            }
        }

        private void writeLCDMsg(string data)
        {
            try
            {
                //sigPlusNET1.LCDWriteString(0, 2, 0, 0, sigFont, data.Substring(0,25));
                string[] words = data.Split(new char[] { ' ' });
                string writeData = "", tempData = "";

                int lineWidth, i;
                short lineHeight;
                short yPos = 0;
                lineHeight = (short)sigPlusNET1.LCDStringHeight(sigFont, data);
                for (i = 0; i < words.Length; i++)
                {
                    tempData += words[i];

                    lineWidth = sigPlusNET1.LCDStringWidth(sigFont, tempData);

                    if (lineWidth < lcdMaxWidth)
                    {
                        writeData = tempData;
                        tempData += " ";

                        lineWidth = sigPlusNET1.LCDStringWidth(sigFont, tempData);
                        if (lineWidth < lcdMaxWidth)
                        {
                            writeData = tempData;
                        }
                    }
                    else
                    {
                        sigPlusNET1.LCDWriteString(0, 2, 0, yPos, sigFont, writeData);

                        tempData = "";
                        writeData = "";
                        yPos += lineHeight;
                        i--;
                    }
                }

                if (writeData != "")
                {
                    sigPlusNET1.LCDWriteString(0, 2, 0, yPos, sigFont, writeData);
                }
            }
            catch (Exception)
            {
            }
        }

        //Refresh (invert) LCD at left button to indicate to user that this option has been sucessfully chosen
        private void toggleButtonLeft()
        {
            try
            {
                //sigPlusNET1.ClearSigWindow(1);
                sigPlusNET1.LCDRefresh(1, 98, yPosButtons, widthButtonLeft, 17);
            }
            catch (Exception)
            {
            }
        }

        //Refresh (invert) LCD at right button to indicate to user that this option has been sucessfully chosen
        private void toggleButtonRight()
        {
            //sigPlusNET1.ClearSigWindow(1);
            sigPlusNET1.LCDRefresh(1, 198, yPosButtons, widthButtonRight, 17);
        }


        private void sigPlusNET1_PenDown(object sender, EventArgs e)
        {
            try
            {
                switch (promptType)
                {
                    case 0:
                        if (sigPlusNET1.KeyPadQueryHotSpot(0) > 0) //If the Yes hotspot is tapped, then...
                        {
                            toggleButtonLeft();
                            showPromptScreen(screenId + 1);
                        }
                        else if (sigPlusNET1.KeyPadQueryHotSpot(1) > 0) //If the Exit hotspot is tapped, then...
                        {
                            toggleButtonRight();
                            if (screenId == 0)
                            {
                                sigPlusNET1.SetLCDCaptureMode(1);
                                statLabel.Text = "Quit Pressed";
                                //reset hardware
                                sigPlusNET1.SetTabletState(0);
                                //raise event
                            }
                            else
                            {
                                showPromptScreen(screenId - 1);
                            }
                        }
                        break;
                    default:
                        if (sigPlusNET1.KeyPadQueryHotSpot(2) > 0) //If the CLEAR hotspot is tapped, then...
                        {
                            sigPlusNET1.ClearSigWindow(1);
                            sigPlusNET1.LCDRefresh(1, 10, yPosButtons - 1, widthButtonLeft, 17); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
                            showSignScreen();
                        }
                        else if (sigPlusNET1.KeyPadQueryHotSpot(3) > 0) //If the OK hotspot is tapped, then...
                        {
                            sigPlusNET1.ClearSigWindow(1);
                            sigPlusNET1.LCDRefresh(1, 200, yPosButtons - 1, widthButtonRight, 17); //Refresh LCD at 'OK' to indicate to user that this option has been sucessfully chosen

                            if (sigPlusNET1.NumberOfTabletPoints() > 0)
                            {
                                ShowTimeStamp();
                                sigPlusNET1.BackColor = Color.LightGreen;
                                haveSignature = true;
                                statLabel.Text = "Signed";
                            }
                            else     //No Signature entered
                            {
                                refreshLCD();
                                writeLCDMsg("Please Sign Before Pressing Ok..");
                                //sigPlusNET1.LCDWriteBitmap(0, 2, 4, 20, 234, 21, ptrPlease.ToInt32());
                                System.Threading.Thread.Sleep(1000);
                                showSignScreen();
                            }
                        }
                        break;
                }
                //sigPlusNET1.SetEventEnableMask(1);
            }
            catch (Exception)
            {
            }
        }

        public void ResetTablet()
        {
            try
            {
                sigPlusNET1.ClearTablet();
                sigPlusNET1.SetKeyString("0000000000000000");
                sigPlusNET1.SetSigCompressionMode(0);
                sigPlusNET1.SetEncryptionMode(0);
                sigPlusNET1.SetLCDCaptureMode(1);
                sigPlusNET1.SetTimeStamp("");
                BackLight(false);
                /*CTRL-D is sent to the tablet, which clears the tablet and sets 
                  capture mode to be active with Autoerase in the tablet. */
                sigPlusNET1.SetTabletState(0);
                haveSignature = false;
                screenId = -1;
                //Detaches from the port and stops gathering data.
            }
            catch (Exception)
            {
            }
        }

        public Image GetImage()
        {
            try
            {
                sigPlusNET1.SetImageXSize(480);
                sigPlusNET1.SetImageYSize(128);
                sigPlusNET1.SetImagePenWidth(5);
                sigPlusNET1.SetImageFileFormat(0);
                return sigPlusNET1.GetSigImage();
                /*
                FileFormat-File format for Image files.
                0=Compressed BMP (default) must have .bmp ext. 
                1=Uncompressed BMP must have .bmp ext. 
                2=Mono. BMP must have .bmp ext. 
                3=JPG Q=20 must have .jpg ext. 
                4=JPG Q=100 must have .jpg ext. 
                5=Uncompressed TIF must have .tif ext. 
                6=Compressed TIF must have .tif ext. 
                7=WMF (windows metafile) must have .wmf ext. 
                8=EMF (enhanced metafile) must have .emf ext. 
                9=TIF (1-bit) must have .tif ext. 
                10=TIF (1-bit inverted) must have .tif ext. 
                */

            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetSignature()
        {
            try
            {
                return sigPlusNET1.GetSigString();
            }
            catch (Exception)
            {
            }
            return "";
        }

        public string GetSigDate()
        {
            try
            {
                return sigPlusNET1.GetTimeStamp(); 
            }
            catch (Exception)
            {
            }
            return "";
        }

        public void BackLight(bool on)
        {
            try
            {
                string cmdStr = Convert.ToString(Convert.ToChar(3));
                String strResult = " ";
                if (on == true)
                {
                    cmdStr = Convert.ToString(Convert.ToChar(2));
                }
                int sigState = sigPlusNET1.GetTabletState();
                sigPlusNET1.SetTabletState(1);
                sigPlusNET1.LCDSendCmdString(cmdStr, 1, strResult, 10);
                sigPlusNET1.SetTabletState(sigState);
            }
            catch (Exception)
            {
            }
        }

        public void ShowTimeStamp()
        {
            try
            {
                sigPlusNET1.SetTimeStamp((System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)DateTime.Now.DayOfWeek].ToString().Substring(0, 3))
                                                    + " " + DateTime.Now.ToShortDateString()
                                                    + " " + DateTime.Now.ToShortTimeString());
                sigPlusNET1.SetDisplayTimeStampPosX(1);
                sigPlusNET1.SetDisplayTimeStampPosY(1);
                sigPlusNET1.SetDisplayTimeStampSize(160);
                sigPlusNET1.SetDisplayTimeStamp(true);
                sigPlusNET1.SetImageTimeStampPosX(1);
                sigPlusNET1.SetImageTimeStampPosY(1);
                sigPlusNET1.SetImageTimeStampSize(100);
                sigPlusNET1.SetImageTimeStamp(true);
            }
            catch (Exception)
            {
            }
        }

        public bool IsSigned
        {
            get
            {
                return haveSignature;
            }
        }

        public int ScreenMode
        {
            get
            {
                return screenId;
            }
        }

        public string Signature
        {
            get { return sigPlusNET1.GetSigString(); }
            set 
            { 
                sigPlusNET1.SetSigString(value);
                haveSignature = true;
            }
        }

        public bool SaveSigImage(string filename)
        {
            try
            {
                try
                {
                    if (File.Exists(filename) == true)
                    {
                        File.Delete(filename);
                    }
                }
                catch (Exception)
                {
                }
                Image sigImage = GetImage();
                sigImage.Save(filename,System.Drawing.Imaging.ImageFormat.Bmp);
                return File.Exists(filename);
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}
