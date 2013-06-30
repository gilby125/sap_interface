﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using Autofac;

using MiniSqlQuery.PlugIns.DatabaseInspector.Commands;
using Autofac.Core;


namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	/// <summary>The database inspector loader.</summary>
	public class DatabaseInspectorLoader : PluginLoaderBase
	{
		/// <summary>Initializes a new instance of the <see cref="DatabaseInspectorLoader"/> class.</summary>
		public DatabaseInspectorLoader()
			: base(
				"Database Inspector", 
				"A Mini SQL Query Plugin for displaying the database schema in a tree view", 
				20)
		{
		}

		/// <summary>Iinitialize the plug in.</summary>
		public override void InitializePlugIn()
		{
			//Services.RegisterSingletonComponent<IDatabaseInspector, DatabaseInspectorForm>("DatabaseInspector");
            Services.RegisterComponent<IDatabaseInspector, DatabaseInspectorForm>();
            //var builder = new ContainerBuilder();
            ////builder.Register(c=>new DatabaseInspectorForm(c.Resolve<IApplicationServices>(),c.Resolve<IHostWindow>()));

            //builder.RegisterType<DatabaseInspectorForm>().As<IDatabaseInspector>().WithParameters(
            //new[] { new ResolvedParameter((p,c)=>p.ParameterType == typeof(IApplicationServices),(p,c)=>c.Resolve<IApplicationServices>()),
            //        new ResolvedParameter((p,c)=>p.ParameterType == typeof(IHostWindow),(p,c)=>c.Resolve<IHostWindow>())
            //    });

            //builder.Update(Services.Container);
			//Services.RegisterComponent<FindObjectForm>("FindObjectForm");
            Services.RegisterComponent<FindObjectForm>();
			IHostWindow hostWindow = Services.HostWindow;
			hostWindow.AddPluginCommand<ShowDatabaseInspectorCommand>();
			CommandManager.GetCommandInstance<ShowDatabaseInspectorCommand>().Execute();

			ToolStripMenuItem editMenu = hostWindow.GetMenuItem("edit");
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowFindObjectFormCommand>());

			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateSelectStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateSelectCountStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateInsertStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateUpdateStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateDeleteStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CopyTableNameCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<TruncateTableCommand>());

			hostWindow.DatabaseInspector.ColumnMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<LocateFkReferenceColumnCommand>());

			// todo: bug - the opening event is not firing....
			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(hostWindow.DatabaseInspector.ColumnMenu);
		}
	}
}