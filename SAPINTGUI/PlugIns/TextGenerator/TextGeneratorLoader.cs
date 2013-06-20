﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using SAPINT.Gui.PlugIns.TextGenerator.Commands;

namespace SAPINT.Gui.PlugIns.TextGenerator
{
	public class TextGeneratorLoader : PluginLoaderBase
	{
		public TextGeneratorLoader()
			: base(
				"Text Generator Tools", 
				"A Mini SQL Query Plugin for generating test from... text :-)", 
				21)
		{
		}

		public override void InitializePlugIn()
		{
			IHostWindow hostWindow = Services.HostWindow;
			hostWindow.AddPluginCommand<RunTextGeneratorCommand>();
		}
	}
}