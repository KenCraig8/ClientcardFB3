using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CCFB3sigplusCapture
{
    public partial class Form1 : Form
    {
        IntPtr  ptrSignBMP;
        int lcdX, lcdY, lcdSize;
        //string prompt1, prompt2;
        short widthButtonLeft, widthButtonRight;
        string[] screenList = new string[2] {"I am declaring that members of my household are in need of this food and we meet the income eligibility standards.",
                                             "This is the ONLY Food Bank where I receive federal commodities."};
        string[] rightbuttonList = new string[2] { "Exit", "Back" };

        int promptType = 0;
        int screenId = 0;
        public Form1()
        {
            InitializeComponent();
            /******************************************************************'
			 The following parameters are set in case the user's INI file is not correctly set up for an LCD 1X5 tablet
			 Otherwise, if the INI is correctly set up, these parameters do not need to be set*/
            //axSigPlus1.TabletXStart = 400;
            //axSigPlus1.TabletXStop = 2400;
            //axSigPlus1.TabletYStart = 350;
            //axSigPlus1.TabletYStop = 1050;
            //axSigPlus1.TabletLogicalXSize = 2000;
            //axSigPlus1.TabletLogicalYSize = 700;
            /*******************************************************************/

            //The following code will write BMP images out to the LCD 1X5 screen

            //Bitmap pleaseMap = new System.Drawing.Bitmap(Application.StartupPath + "\\please.bmp");

            //ptrPlease = pleaseMap.GetHbitmap();
            Bitmap signMap = new System.Drawing.Bitmap(Application.StartupPath + "\\Sign.bmp");
            ptrSignBMP = signMap.GetHbitmap();
            axSigPlus1.LCDWriteBitmap(1, 2, 0, 20, 240, 45, ptrSignBMP.ToInt32());
        }

        private void showPromptScreen(int newScreenId)
        {
            if (newScreenId < screenList.Length)
            {
                screenId = newScreenId;
                //staPage1.BackColor = Color.Yellow;
                //staPage2.BackColor = Color.Gainsboro;
                //staSignature.BackColor = Color.Gainsboro;
                axSigPlus1.ClearTablet();
                refreshLCD();
                writeLCDMsg(screenList[screenId]);
                //Buttons
                defineButtons(20, "YES", 20, rightbuttonList[screenId]);

                axSigPlus1.ClearTablet();
                axSigPlus1.LCDSetWindow(0, 0, 1, 1);
                axSigPlus1.SetSigWindow(1, 0, 0, 1, 1); //Sets the area where ink is permitted in the SigPlus object

                promptType = 0;
                axSigPlus1.SetEventEnableMask(1);
            }
            else
            {
                showSignScreen();
            }
        }

        private void axSigPlus1_PenDown(object sender, EventArgs e)
        {
            switch (promptType)
            {
                case 0:
                    if (getKeyPressed(0) > 0) //If the Yes hotspot is tapped, then...
                    {
                        toggleButtonLeft();
                        showPromptScreen(screenId + 1);
                    }
                    else if (getKeyPressed(1) > 0) //If the Exit hotspot is tapped, then...
                    {
                        toggleButtonRight();
                        if (screenId == 0)
                        {
                            axSigPlus1.LCDCaptureMode = 1;

                            //reset hardware
                            axSigPlus1.TabletState = 0;
                            btnStart.Enabled = true;
                            btnSave.Visible = false;
                        }
                        else
                        {
                            showPromptScreen(screenId - 1);
                        }
                    }
                    break;
                default:
                    if (getKeyPressed(2) > 0) //If the CLEAR hotspot is tapped, then...
                    {
                        axSigPlus1.ClearSigWindow(1);
                        axSigPlus1.LCDRefresh(1, 10, 0, 53, 17); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
                        showSignScreen();
                    }
                    else if (getKeyPressed(3) > 0) //If the OK hotspot is tapped, then...
                    {
                        axSigPlus1.ClearSigWindow(1);
                        staSignature.BackColor = Color.YellowGreen;
                        axSigPlus1.LCDRefresh(1, 200, 3, 14, 14); //Refresh LCD at 'OK' to indicate to user that this option has been sucessfully chosen

                        if (axSigPlus1.NumberOfTabletPoints() > 0)
                        {
                            btnSave.Visible = true;
                        }
                        else     //No Signature entered
                        {
                            refreshLCD();
                            writeLCDMsg("Please Sign Before Pressing Ok..");
                            //axSigPlus1.LCDWriteBitmap(0, 2, 4, 20, 234, 21, ptrPlease.ToInt32());
                            System.Threading.Thread.Sleep(1000);
                            showSignScreen();
                        }
                    }
                    break;
            }
            axSigPlus1.SetEventEnableMask(1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSignature();
            refreshLCD();
            statusStrip1.Visible = false;
            axSigPlus1.ClearTablet();
            btnSave.Visible = false;
            btnStart.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            axSigPlus1.TabletState = 1;  //Turns tablet on to collect signature
            axSigPlus1.LCDCaptureMode = 2;   //Sets mode so ink will not disappear after a few seconds
            refreshLCD();

            //Check for LCD size in pixels.
            lcdSize = axSigPlus1.GetLCDSize();
            lcdX = lcdSize & 0xFFFF;
            lcdY = (lcdSize >> 16) & 0xFFFF;
            statusStrip1.Visible = true;
            showPromptScreen(0);
        }

        private void defineButtons(short widthLeft, string textLeft, short widthRight, string textRight)
        {
            widthButtonLeft = widthLeft;
            axSigPlus1.LCDWriteString(0, 2, 15, 45, 5, 5, 0, textLeft);
            axSigPlus1.KeyPadAddHotSpot(0, 1, 12, 40, widthButtonLeft, 15); 
            widthButtonRight = widthRight;
            axSigPlus1.LCDWriteString(0, 2, 200, 45, 5, 5, 0, textRight);
            axSigPlus1.KeyPadAddHotSpot(1, 1, 195, 40, widthButtonRight, 15);
        }
        private int getKeyPressed(short hotspot)
        {
            return axSigPlus1.KeyPadQueryHotSpot(hotspot);
        }

        //private void showScreen1()
        //{
        //    staPage1.BackColor = Color.Yellow;
        //    staPage2.BackColor = Color.Gainsboro;
        //    staSignature.BackColor = Color.Gainsboro;
        //    axSigPlus1.ClearTablet();
        //    refreshLCD();
        //    //Demo text
        //    //prompt1 = "These are sample terms and conditions. Please press Continue.";
        //    prompt1 = "I am declaring that members of my household are in need of this food and we meet the income eligibility standards.";
        //    writeLCDMsg(prompt1);
        //    //Buttons
        //    defineButtons(20, "YES", 20, "Exit");

        //    axSigPlus1.ClearTablet();

        //    axSigPlus1.LCDSetWindow(0, 0, 1, 1);
        //    axSigPlus1.SetSigWindow(1, 0, 0, 1, 1); //Sets the area where ink is permitted in the SigPlus object

        //    screenId = 1;
        //    axSigPlus1.SetEventEnableMask(1);
        //}

        //private void showScreen2()
        //{
        //    staPage1.BackColor = Color.YellowGreen;
        //    staPage2.BackColor = Color.Yellow;
        //    staSignature.BackColor = Color.Gainsboro;
        //    axSigPlus1.ClearTablet();
        //    refreshLCD();
        //    //Demo text
        //    //prompt2 = "We'll bind the signature to all the displayed text. Please press Continue.";
        //    prompt2 = "This is the ONLY Food Bank where I receive federal commodities.";
        //    writeLCDMsg(prompt2);

        //    defineButtons(20, "YES", 25, "Back");

        //    screenId = 2;
        //    axSigPlus1.SetEventEnableMask(1);
        //}

        private void showSignScreen()
        {
            //staPage1.BackColor = Color.YellowGreen;
            //staPage2.BackColor = Color.YellowGreen;
            //staSignature.BackColor = Color.Yellow;
            axSigPlus1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
            axSigPlus1.ClearTablet();
            axSigPlus1.KeyPadClearHotSpotList();
            axSigPlus1.LCDWriteString(0, 2, 10, 5, 50, 15, 0, "Clear");
            axSigPlus1.KeyPadAddHotSpot(2, 1, 10, 5, 53, 17); //For CLEAR button
            axSigPlus1.LCDWriteString(0, 2, 200, 5, 15, 15, 0, "OK");
            axSigPlus1.KeyPadAddHotSpot(3, 1, 197, 5, 19, 17); //For OK button
            
            axSigPlus1.LCDSetWindow(0, 22, 238, 40);
            axSigPlus1.SetSigWindow(1, 0, 22, 240, 40); //Sets the area where ink is permitted in the SigPlus object
            promptType = 1;
            axSigPlus1.SetEventEnableMask(1);
        }

        private void saveSignature()
        {
            string strSig;
            axSigPlus1.ClearSigWindow(1);

            /*the following code is used to cryptographically bind the
              signature to some specific data, passed in
              using the AutoKeyData property
              the signature will not be decrypted without this data*/
            axSigPlus1.AutoKeyStart();
            for (int i = 0; i < screenList.Length; i++)
            {
                axSigPlus1.AutoKeyData = screenList[i];    
            }
            axSigPlus1.AutoKeyFinish();

            /*********************Two ways to save the signature*
            *************************************'
            Method 1--storing as an ASCII string value*/
            strSig = axSigPlus1.SigString;
            /*the strSig String variable now holds the signature as a long ASCII string.
            this can be stored as desired, in a database, etc.

            Method 2--storing as a SIG file on the hard drive
            axSigPlus1.ExportSigFile "C:\SigFile1.sig"
            The commented-out function above will export the signature to the SIG file
            specified (in this case C:\SigFile1.sig, saving the signature as a file on your hardrive
            *****************************************************************************************/
        }

        //Refresh (invert) LCD at left button to indicate to user that this option has been sucessfully chosen
        private void toggleButtonLeft()
        {
            axSigPlus1.ClearSigWindow(1);
            axSigPlus1.LCDRefresh(1, 16, 45, widthButtonLeft, 15); 
        }

        //Refresh (invert) LCD at right button to indicate to user that this option has been sucessfully chosen
        private void toggleButtonRight()
        {
            axSigPlus1.ClearSigWindow(1);
            axSigPlus1.LCDRefresh(1, 200, 45, widthButtonRight, 15); 
        }

        private void refreshLCD()
        {
            axSigPlus1.LCDRefresh(0, 0, 0, 240, 64);
        }

        private void writeLCDMsg(string data)
        {
            axSigPlus1.LCDSetFont(15, 0, 0, 0, 0, 0, "Arial");
            string[] words = data.Split(new char[] { ' ' });
            string writeData = "", tempData = "";

            int lineSize, xSize, ySize, i;
            short yPos = 0;

            for (i = 0; i < words.Length; i++)
            {
                tempData += words[i];

                lineSize = axSigPlus1.GetLCDStringSize(tempData);
                xSize = lineSize & 0xFFFF;

                if (xSize < lcdX)
                {
                    writeData = tempData;
                    tempData += " ";

                    lineSize = axSigPlus1.GetLCDStringSize(tempData);
                    xSize = lineSize & 0xFFFF;

                    if (xSize < lcdX)
                    {
                        writeData = tempData;
                    }
                }
                else
                {
                    axSigPlus1.LCDWriteString(0, 2, 0, yPos, 5, 5, 0, writeData);

                    tempData = "";
                    writeData = "";
                    ySize = (lineSize >> 16) & 0xFFFF;
                    yPos += (short)ySize;
                    i--;
                }
            }

            if (writeData != "")
            {
                axSigPlus1.LCDWriteString(0, 2, 0, yPos, 5, 5, 0, writeData);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshLCD();
            axSigPlus1.ClearTablet();
            axSigPlus1.TabletMode = 0;
        }
    }
}