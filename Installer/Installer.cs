﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.RevitAddIns;

namespace Installer
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            WriteRevitAddin();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            DeleteRevitAddin();
        }

        private void WriteRevitAddin()
        {
            foreach (var product in RevitProductUtility.GetAllInstalledRevitProducts())
            {
                if (product.Version == RevitVersion.Revit2019)
                {
                    string pathAddin = product.AllUsersAddInFolder + "\\ReplaceValueParameter.addin";
                    Guid guid = new Guid("61217e6a-7a87-4ed8-ae8e-ef74580812f8");
                    string assembly = GetAssembly();
                    string fullClassName = "BBI.JD.CrtlApplication";
                    string vendorId = "JDS";
                    string vendorDescription = "Juan Daniel SANTANA";

                    RevitAddInManifest manifest = File.Exists(pathAddin) ? AddInManifestUtility.GetRevitAddInManifest(pathAddin) : new RevitAddInManifest();

                    RevitAddInApplication app = manifest.AddInApplications.FirstOrDefault(x => x.AddInId == guid);

                    if (app == null)
                    {
                        app = new RevitAddInApplication("ReplaceValueParameter", assembly, guid, fullClassName, vendorId);
                        app.VendorDescription = vendorDescription;

                        manifest.AddInApplications.Add(app);
                    }
                    else
                    {
                        app.Assembly = assembly;
                        app.FullClassName = fullClassName;
                    }

                    if (manifest.Name == null)
                    {
                        manifest.SaveAs(pathAddin);
                    }
                    else
                    {
                        manifest.Save();
                    }
                }
            }
        }

        private void DeleteRevitAddin()
        {
            foreach (var product in RevitProductUtility.GetAllInstalledRevitProducts())
            {
                if (product.Version == RevitVersion.Revit2019)
                {
                    string pathAddin = product.AllUsersAddInFolder + "\\ReplaceValueParameter.addin";
                    
                    if (File.Exists(pathAddin))
                    {
                        File.Delete(pathAddin);
                    }
                }
            }
        }

        private string GetAssembly()
        {
            string pathDir = Context.Parameters["targetdir"];

            pathDir = pathDir.Remove(pathDir.Length - 1, 1);

            return pathDir + "ReplaceValueParameter.dll";
        }
    }
}
