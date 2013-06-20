#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Collections.Generic;
using System.IO;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Given a file name or extention the service will work out the most appropriate editor to use.
	/// </summary>
	/// <remarks>
	/// 	The editors need to be registered using the <see cref = "Register" /> method.
	/// 	<example>
	/// 		<code>
	/// 			IEditor editor = resolver.ResolveEditorInstance("c:\data.sql");
	/// 			// will resolve to the SQL editor
	/// 
	/// 			IEditor editor = resolver.ResolveEditorInstance("c:\foo.txt");
	/// 			// will resolve to the basic text editor</code>
	/// 	</example>
	/// </remarks>
	public class FileEditorResolverService : IFileEditorResolver
	{
		/// <summary>
		/// 	The extention map of files types to descriptors.
		/// </summary>
		private readonly Dictionary<string, FileEditorDescriptor> _extentionMap;

		/// <summary>
		/// 	The file editor descriptors.
		/// </summary>
		private readonly List<FileEditorDescriptor> _fileEditorDescriptors;

		/// <summary>
		/// 	The application services.
		/// </summary>
		private readonly IApplicationServices _services;

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "FileEditorResolverService" /> class.
		/// </summary>
		/// <param name = "services">The application services.</param>
		public FileEditorResolverService(IApplicationServices services)
		{
			_services = services;
			_extentionMap = new Dictionary<string, FileEditorDescriptor>();
			_fileEditorDescriptors = new List<FileEditorDescriptor>();
		}

		/// <summary>
		/// 	Gets an array of the file descriptiors.
		/// </summary>
		/// <returns>An array of <see cref = "FileEditorDescriptor" /> objects.</returns>
		public FileEditorDescriptor[] GetFileTypes()
		{
			return _fileEditorDescriptors.ToArray();
		}

		/// <summary>
		/// 	Registers the specified file editor descriptor.
		/// 	It is recommended to use the <see cref = "IApplicationServices.RegisterEditor{TEditor}" /> method to
		/// 	set up the container correctly.
		/// </summary>
		/// <param name = "fileEditorDescriptor">The file editor descriptor.</param>
		public void Register(FileEditorDescriptor fileEditorDescriptor)
		{
			_fileEditorDescriptors.Add(fileEditorDescriptor);
			if (fileEditorDescriptor.Extensions == null || fileEditorDescriptor.Extensions.Length == 0)
			{
				_extentionMap.Add("*", fileEditorDescriptor);
			}
			else
			{
				// create a map of all ext to editors
				foreach (string extention in fileEditorDescriptor.Extensions)
				{
					_extentionMap.Add(extention, fileEditorDescriptor);
				}
			}
		}

		/// <summary>
		/// 	Resolves the editor instance from the container based on the filename.
		/// </summary>
		/// <param name = "filename">The filename.</param>
		/// <returns>An editor.</returns>
		public IEditor ResolveEditorInstance(string filename)
		{
			string ext = Path.GetExtension(filename);
			string editorName = ResolveEditorNameByExtension(ext);
			return _services.Resolve<IEditor>(editorName);
		}

		/// <summary>
		/// 	Works out the "name" of the editor to use based on the <paramref name = "extension" />.
		/// </summary>
		/// <param name = "extension">The extention ("sql", "txt"/".txt" etc).</param>
		/// <returns>The name of an editor in the container.</returns>
		public string ResolveEditorNameByExtension(string extension)
		{
			string editorName = _extentionMap["*"].EditorKeyName;

			if (extension != null)
			{
				if (extension.StartsWith("."))
				{
					extension = extension.Substring(1);
				}

				// is there a specific editor for this file type
				if (_extentionMap.ContainsKey(extension))
				{
					editorName = _extentionMap[extension].EditorKeyName;
				}
			}

			return editorName;
		}
	}
}