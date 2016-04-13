
// =====================================================================
//  File:		ImportExportPublishConsole.cs
//  Summary:	This sample includes an XML file with Bank Accounts and 
//              Safe Deposit Boxes as new entities that need to be 
//              imported. The sample shows the following process: 
//              Export current customizations for the purposes of 
//              providing a backup. 
//              Import the included XML file that contains new customizations. 
//              Publish the imported customizations.
// =====================================================================
//
//  This file is part of the Microsoft CRM 3.0 SDK Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================

using System;
using Microsoft.Crm.Sdk.FullSample.ImportExportPublish;

namespace ImportExportPublishConsole
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class ImportExportPublishConsole
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Sample sample = new Sample();
			sample.Run();
			Console.WriteLine("ImportExportPublish Complete");
           
		}
	}
}
