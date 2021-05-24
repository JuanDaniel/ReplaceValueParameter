using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReplaceValueParameter.Resources;

namespace BBI.JD.Forms
{
    public partial class ReplaceValueParameter : System.Windows.Forms.Form
    {
        private CultureInfo culture;
        private UIApplication application;
        private UIDocument uiDoc;
        private Document document;
        private IList<Element> elements;

        public ReplaceValueParameter(UIApplication application)
        {
            culture = Command.CultureByDefault();

            InitializeComponent();

            this.application = application;
            uiDoc = application.ActiveUIDocument;
            document = uiDoc.Document;
        }

        private void ReplaceValueParameter_Load(object sender, EventArgs e)
        {
            FilteredElementCollector collector = new FilteredElementCollector(document)
                .WherePasses(new RoomFilter());

            if (elements.Count() > 0)
            {
                LoadRoomParameters();

                if (uiDoc.Selection.GetElementIds().Count > 0)
                {
                    collector = new FilteredElementCollector(document, uiDoc.Selection.GetElementIds())
                        .WherePasses(new RoomFilter());

                    if (collector.Count() > 0)
                    {
                        cmb_SetType.SelectedIndex = 2;
                    }
                    else
                    {
                        cmb_SetType.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmb_SetType.SelectedIndex = 0;
                }
            }
            else
            {
                btn_Ok.Enabled = false;

                MessageBox.Show(
                    ReplaceValueParameterResouce.ResourceManager.GetString("NoRoomsDesc", culture),
                    ReplaceValueParameterResouce.ResourceManager.GetString("NoRooms", culture), 
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmb_SetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilteredElementCollector collector = null;

            switch (cmb_SetType.SelectedIndex)
            {
                case 0:
                    collector = new FilteredElementCollector(document);
                    break;

                case 1:
                    collector = new FilteredElementCollector(document, document.ActiveView.Id);
                    break;

                case 2:
                    var selectedIds = uiDoc.Selection.GetElementIds();
                    if (selectedIds.Count > 0)
                    {
                        collector = new FilteredElementCollector(document, selectedIds);
                    }
                    break;

                default:
                    collector = new FilteredElementCollector(document);
                    break;
            }

            if (collector != null)
            {
                collector.WherePasses(new RoomFilter());

                elements = collector.ToElements();
            }
            else
            {
                elements = new List<Element>();
            }

            label4.Text = elements.Count.ToString();

            txt_Value.Clear();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (elements.Count == 0)
            {
                MessageBox.Show(
                    ReplaceValueParameterResouce.ResourceManager.GetString("NoRoomsCollectionDesc", culture),
                    ReplaceValueParameterResouce.ResourceManager.GetString("NoRoomsCollection", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }
            if (cmb_Parameter.SelectedIndex == -1)
            {
                MessageBox.Show(
                    ReplaceValueParameterResouce.ResourceManager.GetString("SelectParameterDesc", culture),
                    ReplaceValueParameterResouce.ResourceManager.GetString("SelectParameter", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }
            if (string.IsNullOrEmpty(txt_Value.Text) || !CheckValidValue())
            {
                MessageBox.Show(
                    ReplaceValueParameterResouce.ResourceManager.GetString("InvalidValueDesc", culture),
                    ReplaceValueParameterResouce.ResourceManager.GetString("InvalidValue", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            if (ReplaceValue())
            {
                MessageBox.Show(
                    ReplaceValueParameterResouce.ResourceManager.GetString("SuccessfulReplaceDesc", culture),
                    ReplaceValueParameterResouce.ResourceManager.GetString("SuccessfulReplace", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    ReplaceValueParameterResouce.ResourceManager.GetString("ErrorReplaceDesc", culture),
                    ReplaceValueParameterResouce.ResourceManager.GetString("ErrorReplace", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTiTleForm()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            return string.Format(
                "{0} ({1}.{2}.{3}.{4})",
                ReplaceValueParameterResouce.ResourceManager.GetString("WindowsTitle", culture), 
                version.Major, version.Minor, version.Build, version.Revision);
        }

        private void LoadRoomParameters()
        {
            cmb_Parameter.Items.Clear();

            Element room = elements.First();

            foreach (Parameter parameter in room.GetOrderedParameters())
            {
                if (!parameter.IsReadOnly)
                {
                    if (parameter.StorageType == StorageType.Double || parameter.StorageType == StorageType.Integer || parameter.StorageType == StorageType.String)
                    {

                        cmb_Parameter.Items.Add(parameter.Definition.Name);
                    }
                }
            }
        }

        private bool CheckValidValue()
        {
            Element room = elements.First();

            Parameter parameter = room.LookupParameter(cmb_Parameter.Text);

            double valueDouble;
            int valueInt;

            switch (parameter.StorageType)
            {
                case StorageType.Double:
                    return double.TryParse(txt_Value.Text, out valueDouble);
                    break;

                case StorageType.ElementId:
                    //return true;
                    break;

                case StorageType.Integer:
                    return int.TryParse(txt_Value.Text, out valueInt);

                case StorageType.None:
                    //return true;
                    break;

                case StorageType.String:
                    //return true;
                    break;

                default:
                    break;
            }

            return true;
        }

        private bool ReplaceValue()
        {
            Transaction transaction = null;
            int ignoreEditing = 0;

            try
            {
                transaction = new Transaction(document, "Replace Rooms's values by parameter");

                transaction.Start();

                foreach (Element room in elements)
                {
                    if (IsEditingByAnother(room))
                    {
                        if (ignoreEditing == 0)
                        {
                            TaskDialog td = new TaskDialog(
                                ReplaceValueParameterResouce.ResourceManager.GetString("WindowsTitle", culture));

                            td.Title = ReplaceValueParameterResouce.ResourceManager.GetString("WindowsTitle", culture);
                            td.MainInstruction = ReplaceValueParameterResouce.ResourceManager.GetString("IgnoreEdited", culture);
                            td.MainContent = string.Format("{0}\n{1}",
                                ReplaceValueParameterResouce.ResourceManager.GetString("IgnoreEditedDesc", culture),
                                ReplaceValueParameterResouce.ResourceManager.GetString("IgnoreEditedDesc1", culture));
                            td.FooterText = string.Format("BBI {0}", ReplaceValueParameterResouce.ResourceManager.GetString("ReplaceValueParameter", culture));
                            td.TitleAutoPrefix = false;
                            td.AllowCancellation = false;
                            td.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
                            td.DefaultButton = TaskDialogResult.Yes;

                            TaskDialogResult result = td.Show();

                            if (result == TaskDialogResult.Yes)
                            {
                                ignoreEditing = 1;
                            }
                            else
                            {
                                transaction.RollBack();

                                return false;
                            }
                        }

                        if (ignoreEditing == 1)
                        {
                            continue;
                        }
                    }

                    Parameter parameter = room.LookupParameter(cmb_Parameter.Text);

                    if (parameter.HasValue)
                    {
                        if (chk_Overwrite.Checked)
                        {
                            SetValue(parameter);
                        }
                    }
                    else
                    {
                        SetValue(parameter);
                    }
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                if (null != transaction)
                {
                    transaction.RollBack();
                }

                return false;
            }

            return true;
        }

        private void SetValue(Parameter parameter)
        {
            double valueDouble;
            int valueInt;
            string valueString = txt_Value.Text;

            switch (parameter.StorageType)
            {
                case StorageType.Double:
                    double.TryParse(valueString, out valueDouble);
                    parameter.Set(valueDouble);
                    parameter.SetValueString(valueString);
                    break;

                case StorageType.ElementId:
                    break;

                case StorageType.Integer:
                    int.TryParse(valueString, out valueInt);
                    parameter.Set(valueInt);
                    parameter.SetValueString(valueString);
                    break;

                case StorageType.None:
                    break;

                case StorageType.String:
                    parameter.Set(valueString);
                    parameter.SetValueString(valueString);
                    break;

                default:
                    break;
            }
        }

        private bool IsEditingByAnother(Element element)
        {
            if (!document.IsWorkshared || document.IsDetached)
            {
                return false;
            }

            // Checkout attempt
            ICollection<ElementId> checkedOutIds = WorksharingUtils.CheckoutElements(document, new ElementId[] { element.Id });

            // Confirm checkout
            bool checkedOutSuccessfully = checkedOutIds.Contains(element.Id);

            if (!checkedOutSuccessfully)
            {
                return true;
            }

            ModelUpdatesStatus updatesStatus = WorksharingUtils.GetModelUpdatesStatus(document, element.Id);

            return updatesStatus == ModelUpdatesStatus.DeletedInCentral || updatesStatus == ModelUpdatesStatus.UpdatedInCentral;
        }
    }
}
