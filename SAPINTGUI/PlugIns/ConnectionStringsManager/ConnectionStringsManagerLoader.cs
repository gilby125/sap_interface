﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using SAPINT.Gui.PlugIns.ConnectionStringsManager;
using SAPINT.Gui.PlugIns.ConnectionStringsManager.Commands;

namespace SAPINT.Gui.PlugIns.ConnectionStringsManager
{
	/// <summary>The connection strings manager loader.</summary>
	public class ConnectionStringsManagerLoader : PluginLoaderBase
	{
		/// <summary>Initializes a new instance of the <see cref="ConnectionStringsManagerLoader"/> class.</summary>
		public ConnectionStringsManagerLoader() : base(
			"Connection String Manager", 
			"A Mini SQL Query Plugin for managing the list of connection strings.", 
			10)
		{
		}

		/// <summary>Iinitialize the plug in.</summary>
		public override void InitializePlugIn()
		{
			//Services.RegisterComponent<DbConnectionsForm>("DbConnectionsForm");
            Services.RegisterComponent<DbConnectionsForm>();
			ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<EditConnectionsFormCommand>());
			Services.HostWindow.AddToolStripCommand<EditConnectionsFormCommand>(null);
		}
	}
}