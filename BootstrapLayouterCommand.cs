//------------------------------------------------------------------------------
// <copyright file="BootstrapLayouterCommand.cs" company="AvoBright">
//     Copyright (c) AvoBright Technology.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace AvoBright.BootstrapLayouter
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class BootstrapLayouterCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid MenuGroup = new Guid("96bb7fcf-6167-4836-99c8-cae1976de3fb");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        private static LayoutWindow LayoutWindow { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapLayouterCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private BootstrapLayouterCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                CommandID menuCommandID = new CommandID(MenuGroup, CommandId);
                EventHandler eventHandler = this.ShowLayoutWindow;
                MenuCommand menuItem = new MenuCommand(eventHandler, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static BootstrapLayouterCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new BootstrapLayouterCommand(package);
        }

        private void ShowLayoutWindow(object sender, EventArgs e)
        {
            if (LayoutWindow == null)
            {
                LayoutWindow = new LayoutWindow();
            }

            LayoutWindow.ShowModal();
        }
    }
}
