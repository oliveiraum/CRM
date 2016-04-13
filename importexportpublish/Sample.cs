
// =====================================================================
//  File:		Sample.cs
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
using System.IO;
using System.Xml;
using System.IO.Compression;
using Nini;
using Nini.Config;


namespace Microsoft.Crm.Sdk.FullSample.ImportExportPublish
{
    using CrmSdk;
    using System.Collections.Generic;
    using System.Linq;    /// <summary>
    /// The following sample demonstrates how to export, import, and publish Microsoft CRM
    /// customizations.  The scenario is that you are an ISV that would like to import
    /// two custom entities into an existing system.  The entities include a Bank Account
    /// and a Safe Deposit Box.  Each Safe Deposit Box has a Bank Account as its parent.
    /// 
    /// In this scenario, we will perform the following actions:
    /// 
    /// - Export the systems current customizations (customization_original.xml).
    /// - Import the two new entities (new_customizations.xml).
    /// - Publish the two new entities.
    ///
    /// Make sure to back up your database before running this sample.
    /// </summary>
    public class Sample
    {
        public Sample()
        {

        }



        public bool Run()
        {
            try
            {

                string configFileName = "demo.ini";
                IniConfigSource configSource = new IniConfigSource(configFileName);

                IConfig demoConfigSection = configSource.Configs["Demo"];
                string extractPath = demoConfigSection.Get("extractPath", string.Empty);

                // Set up the CRM Service.
                CrmService service = new CrmService();
                service.Credentials = System.Net.CredentialCache.DefaultCredentials;

                CrmAuthenticationToken token = new CrmAuthenticationToken();
                // TODO Replace 'AdventureWorksCycle' with your Microsoft CRM organization name.
                token.OrganizationName = "IBMRational";

                service.CrmAuthenticationTokenValue = token;
                // TODO Replace 'localhost:5555' with your CRM server information.
                service.Url = @"http://ptmtscetdev01:5555/MSCRMServices/2007/CrmService.asmx";

                #region Export the systems current customizations

                // Create the request.
                /*ExportAllXmlRequest request = new ExportAllXmlRequest();

                // Execute the request.
                ExportAllXmlResponse response = (ExportAllXmlResponse)service.Execute(request);

                // Create an instance of StreamWriter to write text to a file.
                // The using statement also closes the StreamWriter.
                using (StreamWriter sw = new StreamWriter(extractPath + "customization_original.xml")) 
                {
                    // Write the customization XML to the file
                    sw.Write(response.ExportXml);
                }*/

                #endregion

                #region Import new entities into system


                // Create the import request.
                ImportXmlRequest importRequest = new ImportXmlRequest();

                // Define the location of the customization XML.
                string customizationPath = Path.Combine(extractPath);
                const string sourceDir = @"C:\Users\rsdiasoliveira\Downloads\sdk4\sdk\server\fullsample\importexportpublish\cs\bin\Debug\";
                //const string customizationPath = @"C:\AppProject\Smart\ExternalSmartStaff\site\document";
                Console.WriteLine(customizationPath);
                foreach (var file in Directory.GetFiles(customizationPath))

                    File.Copy(file, Path.Combine(sourceDir, Path.GetFileName(file)), true);


                // Pass the stream of customization XML to the import request.
                using (StreamReader sr = new StreamReader(Path.Combine(customizationPath + "customizations.xml")))
                {
                    string customizationXml = sr.ReadToEnd();
                    importRequest.CustomizationXml = customizationXml;

                }

                // Supply the request that is being imported with the Bank Accounts and Safe Deposit Boxes.

                // Supply the request that is being imported with the Bank Accounts and Safe Deposit Boxes.
                importRequest.ParameterXml = @"	<importexportxml>
													<entities>
                                                        <entity>contact</entity>
														<entity>incident</entity>
														<entity>credifin_campanhapromo</entity>
													</entities>
													<nodes/>
                                                    <securityroles/>
                                                    <settings/>
                                                    <workflows/>
												</importexportxml>";

                // Execute the import.
                ImportXmlResponse importResponse = (ImportXmlResponse)service.Execute(importRequest);

                #endregion

                #region Publish new entities

                // Create the request.
                PublishXmlRequest publishRequest = new PublishXmlRequest();

                // Supply the request that is being published with the Bank Account and Safe Deposit Box.

                publishRequest.ParameterXml = @"<importexportxml>
													<entities>
                                                        <entity>contact</entity>
														<entity>incident</entity>
														<entity>credifin_campanhapromo</entity>
													</entities>
													<nodes/>
                                                    <securityroles/>
                                                    <settings/>
                                                    <workflows/>
												</importexportxml>";

                // Execute the request.
                PublishXmlResponse publishResponse = (PublishXmlResponse)service.Execute(publishRequest);

                #endregion
            }
            catch (System.Web.Services.Protocols.SoapException)
            {
                // Add your error handling code here.
            }

            return true;
        }
    }
}
