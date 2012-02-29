namespace Vema.PerformanceTracker.UI.Forms
{
    partial class EditMeasurementsForm
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
            this.grpBoxCurrentMeasurements = new System.Windows.Forms.GroupBox();
            this.lvwMeasurements = new System.Windows.Forms.ListView();
            this.valueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.unitColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.categoryColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.subCategoryColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.remarkColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpBoxNewPlayer = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.cbxUnit = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxtRemark = new System.Windows.Forms.RichTextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cbxSubCategory = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpBoxCurrentMeasurements.SuspendLayout();
            this.grpBoxNewPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxCurrentMeasurements
            // 
            this.grpBoxCurrentMeasurements.Controls.Add(this.lvwMeasurements);
            this.grpBoxCurrentMeasurements.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxCurrentMeasurements.Location = new System.Drawing.Point(12, 240);
            this.grpBoxCurrentMeasurements.Name = "grpBoxCurrentMeasurements";
            this.grpBoxCurrentMeasurements.Size = new System.Drawing.Size(625, 302);
            this.grpBoxCurrentMeasurements.TabIndex = 31;
            this.grpBoxCurrentMeasurements.TabStop = false;
            this.grpBoxCurrentMeasurements.Text = "Leistungsdiagnostische Messdaten";
            // 
            // lvwMeasurements
            // 
            this.lvwMeasurements.BackColor = System.Drawing.Color.Azure;
            this.lvwMeasurements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.valueColumn,
            this.unitColumn,
            this.dateColumn,
            this.categoryColumn,
            this.subCategoryColumn,
            this.remarkColumn});
            this.lvwMeasurements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwMeasurements.FullRowSelect = true;
            this.lvwMeasurements.GridLines = true;
            this.lvwMeasurements.Location = new System.Drawing.Point(3, 19);
            this.lvwMeasurements.Name = "lvwMeasurements";
            this.lvwMeasurements.Size = new System.Drawing.Size(619, 280);
            this.lvwMeasurements.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwMeasurements.TabIndex = 0;
            this.lvwMeasurements.UseCompatibleStateImageBehavior = false;
            this.lvwMeasurements.View = System.Windows.Forms.View.Details;
            // 
            // valueColumn
            // 
            this.valueColumn.Text = "Messwert";
            // 
            // unitColumn
            // 
            this.unitColumn.Text = "Einheit";
            // 
            // dateColumn
            // 
            this.dateColumn.Text = "Zeitstempel";
            // 
            // categoryColumn
            // 
            this.categoryColumn.Text = "Kategorie";
            // 
            // subCategoryColumn
            // 
            this.subCategoryColumn.Text = "Unterkategorie";
            // 
            // remarkColumn
            // 
            this.remarkColumn.Text = "Bemerkung";
            // 
            // grpBoxNewPlayer
            // 
            this.grpBoxNewPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxNewPlayer.Controls.Add(this.btnReset);
            this.grpBoxNewPlayer.Controls.Add(this.cbxCategory);
            this.grpBoxNewPlayer.Controls.Add(this.cbxUnit);
            this.grpBoxNewPlayer.Controls.Add(this.btnNew);
            this.grpBoxNewPlayer.Controls.Add(this.btnModify);
            this.grpBoxNewPlayer.Controls.Add(this.label1);
            this.grpBoxNewPlayer.Controls.Add(this.rtxtRemark);
            this.grpBoxNewPlayer.Controls.Add(this.lblRemark);
            this.grpBoxNewPlayer.Controls.Add(this.lblValue);
            this.grpBoxNewPlayer.Controls.Add(this.lblUnit);
            this.grpBoxNewPlayer.Controls.Add(this.lblCategory);
            this.grpBoxNewPlayer.Controls.Add(this.lblCountry);
            this.grpBoxNewPlayer.Controls.Add(this.txtValue);
            this.grpBoxNewPlayer.Controls.Add(this.cbxSubCategory);
            this.grpBoxNewPlayer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxNewPlayer.Location = new System.Drawing.Point(12, 12);
            this.grpBoxNewPlayer.Name = "grpBoxNewPlayer";
            this.grpBoxNewPlayer.Size = new System.Drawing.Size(770, 222);
            this.grpBoxNewPlayer.TabIndex = 29;
            this.grpBoxNewPlayer.TabStop = false;
            this.grpBoxNewPlayer.Text = "Neuer Spieler";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Navy;
            this.btnReset.Enabled = false;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReset.Location = new System.Drawing.Point(365, 178);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(116, 27);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Zurücksetzen";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cbxCategory
            // 
            this.cbxCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCategory.BackColor = System.Drawing.SystemColors.Control;
            this.cbxCategory.DropDownHeight = 50;
            this.cbxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategory.Enabled = false;
            this.cbxCategory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.IntegralHeight = false;
            this.cbxCategory.Location = new System.Drawing.Point(522, 30);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(229, 24);
            this.cbxCategory.Sorted = true;
            this.cbxCategory.TabIndex = 2;
            this.cbxCategory.SelectedIndexChanged += new System.EventHandler(this.cbxCategory_SelectedIndexChanged);
            // 
            // cbxUnit
            // 
            this.cbxUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUnit.BackColor = System.Drawing.SystemColors.Control;
            this.cbxUnit.DropDownHeight = 50;
            this.cbxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUnit.Enabled = false;
            this.cbxUnit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbxUnit.FormattingEnabled = true;
            this.cbxUnit.IntegralHeight = false;
            this.cbxUnit.Location = new System.Drawing.Point(121, 78);
            this.cbxUnit.Name = "cbxUnit";
            this.cbxUnit.Size = new System.Drawing.Size(218, 24);
            this.cbxUnit.Sorted = true;
            this.cbxUnit.TabIndex = 1;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Navy;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNew.Location = new System.Drawing.Point(648, 178);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(103, 27);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "Neu";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.Navy;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnModify.Location = new System.Drawing.Point(522, 178);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(103, 27);
            this.btnModify.TabIndex = 6;
            this.btnModify.Text = "Ändern";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "* Pflichtfeld";
            // 
            // rtxtRemark
            // 
            this.rtxtRemark.BackColor = System.Drawing.Color.Azure;
            this.rtxtRemark.Enabled = false;
            this.rtxtRemark.Location = new System.Drawing.Point(121, 130);
            this.rtxtRemark.Name = "rtxtRemark";
            this.rtxtRemark.Size = new System.Drawing.Size(218, 75);
            this.rtxtRemark.TabIndex = 4;
            this.rtxtRemark.Text = "";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(6, 130);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(85, 16);
            this.lblRemark.TabIndex = 27;
            this.lblRemark.Text = "Bemerkung:";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(6, 33);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(85, 16);
            this.lblValue.TabIndex = 14;
            this.lblValue.Text = "Messwert*:";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit.Location = new System.Drawing.Point(6, 81);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(64, 16);
            this.lblUnit.TabIndex = 15;
            this.lblUnit.Text = "Einheit*:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(362, 35);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(84, 16);
            this.lblCategory.TabIndex = 16;
            this.lblCategory.Text = "Kategorie*:";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(362, 83);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(119, 16);
            this.lblCountry.TabIndex = 17;
            this.lblCountry.Text = "Unterkategorie*:";
            // 
            // txtValue
            // 
            this.txtValue.BackColor = System.Drawing.Color.Azure;
            this.txtValue.Enabled = false;
            this.txtValue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(121, 30);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(218, 23);
            this.txtValue.TabIndex = 0;
            this.txtValue.Validated += new System.EventHandler(this.ValidatedDouble);
            // 
            // cbxSubCategory
            // 
            this.cbxSubCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSubCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSubCategory.BackColor = System.Drawing.SystemColors.Control;
            this.cbxSubCategory.DropDownHeight = 50;
            this.cbxSubCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSubCategory.Enabled = false;
            this.cbxSubCategory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSubCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbxSubCategory.FormattingEnabled = true;
            this.cbxSubCategory.IntegralHeight = false;
            this.cbxSubCategory.Location = new System.Drawing.Point(522, 80);
            this.cbxSubCategory.Name = "cbxSubCategory";
            this.cbxSubCategory.Size = new System.Drawing.Size(229, 24);
            this.cbxSubCategory.Sorted = true;
            this.cbxSubCategory.TabIndex = 3;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Navy;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRemove.Location = new System.Drawing.Point(660, 344);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(103, 27);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Entfernen";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Navy;
            this.btnAdd.Enabled = false;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.Location = new System.Drawing.Point(660, 259);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(103, 27);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Hinzufügen";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Navy;
            this.btnConfirm.Enabled = false;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnConfirm.Location = new System.Drawing.Point(660, 302);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(103, 27);
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.Text = "Übernehmen";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Navy;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCancel.Location = new System.Drawing.Point(660, 473);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 27);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Navy;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.Location = new System.Drawing.Point(660, 515);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 27);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // EditMeasurementsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grpBoxNewPlayer);
            this.Controls.Add(this.grpBoxCurrentMeasurements);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditMeasurementsForm";
            this.Load += new System.EventHandler(this.EditMeasurementsForm_Load);
            this.grpBoxCurrentMeasurements.ResumeLayout(false);
            this.grpBoxNewPlayer.ResumeLayout(false);
            this.grpBoxNewPlayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxCurrentMeasurements;
        private System.Windows.Forms.ListView lvwMeasurements;
        private System.Windows.Forms.ColumnHeader valueColumn;
        private System.Windows.Forms.ColumnHeader unitColumn;
        private System.Windows.Forms.ColumnHeader dateColumn;
        private System.Windows.Forms.ColumnHeader categoryColumn;
        private System.Windows.Forms.ColumnHeader subCategoryColumn;
        private System.Windows.Forms.ColumnHeader remarkColumn;
        private System.Windows.Forms.GroupBox grpBoxNewPlayer;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cbxSubCategory;
        private System.Windows.Forms.ComboBox cbxUnit;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
    }
}
