﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Helper class for building controls out of <see cref = "ICommand" /> objects.
	/// </summary>
	public class CommandControlBuilder
	{
		/// <summary>
		/// 	Handles the click event of a tool strip item, if the <see cref = "ToolStripItem.Tag" /> is 
		/// 	an <see cref = "ICommand" /> instance the action is executed.
		/// </summary>
		/// <param name = "sender">The sender.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		[DebuggerNonUserCode]
		public static void CommandItemClick(object sender, EventArgs e)
		{
			var item = sender as ToolStripItem;

			if (item != null)
			{
				var cmd = item.Tag as ICommand;

				if (cmd != null)
				{
					cmd.Execute();
				}
			}
		}

		/// <summary>
		/// 	Creates a link label given the <typeparamref name = "TCommand" /> definition.
		/// </summary>
		/// <typeparam name = "TCommand">The type of the command.</typeparam>
		/// <returns>A link label wired to the commands <see cref = "ICommand.Execute" /> method.</returns>
		public static LinkLabel CreateLinkLabel<TCommand>() where TCommand : ICommand, new()
		{
			var linkLabel = new LinkLabel();
			var cmd = CommandManager.GetCommandInstance<TCommand>();

			linkLabel.AutoSize = true;
			linkLabel.Name = cmd.GetType().Name + "LinkLabel";
			linkLabel.TabStop = true;
			linkLabel.Text = cmd.Name.Replace("&", string.Empty);
			linkLabel.Tag = cmd;
			linkLabel.Padding = new Padding(4);
			linkLabel.LinkClicked += LinkLabelLinkClicked;

			return linkLabel;
		}

		/// <summary>
		/// 	Creates a tool strip button given the <typeparamref name = "TCommand" /> definition.
		/// </summary>
		/// <typeparam name = "TCommand">The type of the command.</typeparam>
		/// <returns>A tool strip button</returns>
		public static ToolStripButton CreateToolStripButton<TCommand>() where TCommand : ICommand, new()
		{
			var button = new ToolStripButton();
			var cmd = CommandManager.GetCommandInstance<TCommand>();

			button.DisplayStyle = ToolStripItemDisplayStyle.Image;
			button.Image = cmd.SmallImage;
			button.ImageTransparentColor = Color.Magenta;
			button.Name = cmd.GetType().Name + "ToolStripButton";
			button.Tag = cmd;
			button.Text = cmd.Name;
			button.Click += CommandItemClick;

			return button;
		}

		/// <summary>
		/// 	Creates a tool strip menu item given the <typeparamref name = "TCommand" /> definition.
		/// </summary>
		/// <typeparam name = "TCommand">The type of the command.</typeparam>
		/// <returns>A tool strip menu item wired to the commands <see cref = "ICommand.Execute" /> method.</returns>
		public static ToolStripMenuItem CreateToolStripMenuItem<TCommand>() where TCommand : ICommand, new()
		{
			var menuItem = new ToolStripMenuItem();
			var cmd = CommandManager.GetCommandInstance<TCommand>();

			menuItem.Name = cmd.GetType().Name + "ToolStripMenuItem";
			menuItem.Text = cmd.Name;
			menuItem.Tag = cmd;
			menuItem.ShortcutKeys = cmd.ShortcutKeys;
			menuItem.Image = cmd.SmallImage;
			menuItem.Click += CommandItemClick;

			// store the host for callback
			cmd.Host = menuItem;

			// todo...
			// if (!string.IsNullOrEmpty(cmd.ShortcutKeysText))
			// {
			// menuItem.ToolTipText = string.Format("{0} ({1})", cmd.Name, cmd.ShortcutKeysText);
			// }

			return menuItem;
		}

		/// <summary>
		/// 	Creates a tool strip menu item seperator.
		/// </summary>
		/// <returns>A tool strip seperator.</returns>
		public static ToolStripSeparator CreateToolStripMenuItemSeparator()
		{
			return new ToolStripSeparator();
		}

		/// <summary>
		/// 	Assigns an event handler (<see cref = "TopLevelMenuDropDownOpening" />) to the opening event
		/// 	for menu strip items which in turn hadles enableing and disabling.
		/// </summary>
		/// <param name = "menuStrip">The menu strip to monitor.</param>
		public static void MonitorMenuItemsOpeningForEnabling(ToolStrip menuStrip)
		{
			if (menuStrip is ContextMenuStrip || menuStrip is MenuStrip)
			{
				foreach (ToolStripItem item in menuStrip.Items)
				{
					var topLevelMenu = item as ToolStripMenuItem;
					if (topLevelMenu != null)
					{
						// Debug.WriteLine("MonitorMenuItemsOpeningForEnabling :: " + topLevelMenu.Text);
						topLevelMenu.DropDownOpening += TopLevelMenuDropDownOpening;
						topLevelMenu.DropDownClosed += TopLevelMenuDropDownClosed;
					}
				}
			}
		}

		/// <summary>
		/// 	Used when a menu is opening, handles enabling/disabling of items for display.
		/// </summary>
		/// <param name = "sender">The sender.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		[DebuggerNonUserCode]
		public static void TopLevelMenuDropDownOpening(object sender, EventArgs e)
		{
			var menuItem = sender as ToolStripMenuItem;

			if (menuItem != null)
			{
				foreach (ToolStripItem item in menuItem.DropDownItems)
				{
					var cmd = item.Tag as ICommand;

					if (cmd != null)
					{
						item.Enabled = cmd.Enabled;
					}
				}
			}
		}

		/// <summary>
		/// 	The link label link clicked.
		/// </summary>
		/// <param name = "sender">The sender.</param>
		/// <param name = "e">The <see cref = "System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> instance containing the event data.</param>
		private static void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var linkLabel = sender as Control;

			if (linkLabel != null)
			{
				var cmd = linkLabel.Tag as ICommand;

				if (cmd != null)
				{
					cmd.Execute();
				}
			}
		}

		/// <summary>
		/// 	We need to re-enable all the menu items so that the shortcut keys are available.
		/// 	This is because the model uses a continuous check approach rather than individual events
		/// 	for the enabling.
		/// </summary>
		/// <param name = "sender">The sender.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		[DebuggerNonUserCode]
		private static void TopLevelMenuDropDownClosed(object sender, EventArgs e)
		{
			var menuItem = sender as ToolStripMenuItem;

			if (menuItem != null)
			{
				foreach (ToolStripItem item in menuItem.DropDownItems)
				{
					var cmd = item.Tag as ICommand;

					if (cmd != null)
					{
						item.Enabled = true;
					}
				}
			}
		}
	}
}