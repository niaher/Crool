namespace Crool.AddIn
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Controls;
    using EnvDTE;
    using EnvDTE80;
    using Extensibility;

    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    [GuidAttribute("8FD0E084-B116-4A94-9E85-0BE6A3DC3181"), ProgId("Crool.AddIn.Connect")]
    public class Connect : IDTExtensibility2
    {
        private DTE2 _ApplicationObject;
        private AddIn _AddInInstance;
        private MyControl _UserControl;

        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
        /// <param name='application'>Root object of the host application.</param>
        /// <param name='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param name='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            this.InitializeConfig();

            this._ApplicationObject = (DTE2)application;
            this._AddInInstance = (EnvDTE.AddIn)addInInst;
            
            try
            {
                // ctlProgID - the ProgID for your user control.
                // asmPath - the path to your user control DLL.
                // guidStr - a unique GUID for the user control.
                const string CtlProgId = "Crool.AddIn.Controls.MyControl";
                string asmPath = Assembly.GetExecutingAssembly().Location;
                const string GuidStr = "{1FD0E084-B116-4A94-9E85-0BE6A3DC3181}";

                var toolWins = (Windows2)this._ApplicationObject.Windows;

                // Create the new tool window, adding your user control.
                object objTemp = null;
                Window toolWin = toolWins.CreateToolWindow2(this._AddInInstance, asmPath, CtlProgId, "Crool Window", GuidStr, ref objTemp);

                this._UserControl = (MyControl)objTemp;
                this._UserControl.Application = this._ApplicationObject;
                this._UserControl.Initialize();

                // The tool window must be visible before you do anything 
                // with it, or you will get an error.
                toolWin.Visible = true;
                
                // Set the new tool window's height and width.
                toolWin.Height = 600;
                toolWin.Width = 400;
                toolWin.IsFloating = false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Exception: " + ex.Message);
            }
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param name='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param name='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
        /// <param name='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
        /// <param name='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        private void InitializeConfig()
        {
            Config.ConnectionString = @"Server=.\SQLEXPRESS;Database=Crool;Trusted_Connection=true;";
        }
    }
}