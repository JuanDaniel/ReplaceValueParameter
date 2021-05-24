namespace BBI.JD.Forms
{
    partial class ReplaceValueParameter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceValueParameter));
            this.cmb_SetType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.chk_Overwrite = new System.Windows.Forms.CheckBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Parameter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmb_SetType
            // 
            this.cmb_SetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SetType.FormattingEnabled = true;
            this.cmb_SetType.Items.AddRange(new object[] {
            resources.GetString("cmb_SetType.Items"),
            resources.GetString("cmb_SetType.Items1"),
            resources.GetString("cmb_SetType.Items2")});
            resources.ApplyResources(this.cmb_SetType, "cmb_SetType");
            this.cmb_SetType.Name = "cmb_SetType";
            this.cmb_SetType.SelectedIndexChanged += new System.EventHandler(this.cmb_SetType_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txt_Value
            // 
            resources.ApplyResources(this.txt_Value, "txt_Value");
            this.txt_Value.Name = "txt_Value";
            // 
            // chk_Overwrite
            // 
            resources.ApplyResources(this.chk_Overwrite, "chk_Overwrite");
            this.chk_Overwrite.Name = "chk_Overwrite";
            this.chk_Overwrite.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            resources.ApplyResources(this.btn_Ok, "btn_Ok");
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_Cancel, "btn_Cancel");
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmb_Parameter
            // 
            this.cmb_Parameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Parameter.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_Parameter, "cmb_Parameter");
            this.cmb_Parameter.Name = "cmb_Parameter";
            this.cmb_Parameter.Sorted = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // ReplaceValueParameter
            // 
            this.AcceptButton = this.btn_Ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_Parameter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.chk_Overwrite);
            this.Controls.Add(this.txt_Value);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_SetType);
            this.MaximizeBox = false;
            this.Name = "ReplaceValueParameter";
            this.Text = /*GetTiTleForm();*/ "ReplaceValueParameter";
            this.Load += new System.EventHandler(this.ReplaceValueParameter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_SetType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.CheckBox chk_Overwrite;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Parameter;
        private System.Windows.Forms.Label label5;
    }
}