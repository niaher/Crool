namespace Microsoft.Crool_Package
{
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
    /// usually implemented by the package implementer.
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
    /// implementation of the IVsUIElementPane interface.
    /// </summary>
    [Guid("8b973258-2446-4136-bc78-49b1234781d8")]
    public class MyToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public MyToolWindow() :
            base(null)
        {
            // Set the window title reading it from the resources.
            this.Caption = Resources.ToolWindowTitle;

            // Set the image that will appear on the tab of the window frame
            // when docked with an other window
            // The resource ID correspond to the one defined in the resx file
            // while the Index is the offset in the bitmap strip. Each image in
            // the strip being 16x16.
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            // Create the toolbar.
            this.ToolBar = new CommandID(GuidList.GuidCroolPackageCmdSet, PkgCmdIDList.ToolbarID);
            this.ToolBarLocation = (int)VSTWT_LOCATION.VSTWT_TOP;

            // Create the handlers for the toolbar commands.
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Command for the combo's list
                var comboListCmdID = new CommandID(GuidList.GuidCroolPackageCmdSet, PkgCmdIDList.CmdidWindowsMediaFilenameGetList);
                var comboMenuList = new OleMenuCommand(new EventHandler(this.ComboListHandler), comboListCmdID);
                mcs.AddCommand(comboMenuList);
            }

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
            // the object returned by the Content property.
            this.Content = new MyControl();
        }

        private void ComboListHandler(object sender, EventArgs arguments)
        {
        }
    }
}
