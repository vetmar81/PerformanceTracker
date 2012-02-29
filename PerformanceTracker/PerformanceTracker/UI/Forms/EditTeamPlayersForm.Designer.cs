namespace Vema.PerformanceTracker.UI.Forms
{
    partial class EditTeamPlayersForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpBoxCurrentPlayers = new System.Windows.Forms.GroupBox();
            this.lvwPlayers = new System.Windows.Forms.ListView();
            this.firstNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.birthdayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.countryColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.heightColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.weightColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.remarkColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSave = new System.Windows.Forms.Button();
            this.grpBoxNewPlayer = new System.Windows.Forms.GroupBox();
            this.btnNewPlayer = new System.Windows.Forms.Button();
            this.btnExistingPlayer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxtRemark = new System.Windows.Forms.RichTextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.cbxCountries = new System.Windows.Forms.ComboBox();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.grpBoxCurrentPlayers.SuspendLayout();
            this.grpBoxNewPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(655, 482);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(655, 449);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(103, 27);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Entfernen";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(655, 416);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(103, 27);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Hinzufügen";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpBoxCurrentPlayers
            // 
            this.grpBoxCurrentPlayers.Controls.Add(this.lvwPlayers);
            this.grpBoxCurrentPlayers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxCurrentPlayers.Location = new System.Drawing.Point(12, 285);
            this.grpBoxCurrentPlayers.Name = "grpBoxCurrentPlayers";
            this.grpBoxCurrentPlayers.Size = new System.Drawing.Size(599, 257);
            this.grpBoxCurrentPlayers.TabIndex = 30;
            this.grpBoxCurrentPlayers.TabStop = false;
            this.grpBoxCurrentPlayers.Text = "Aktuelle Spielerliste";
            // 
            // lvwPlayers
            // 
            this.lvwPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.firstNameColumn,
            this.lastNameColumn,
            this.birthdayColumn,
            this.countryColumn,
            this.heightColumn,
            this.weightColumn,
            this.remarkColumn});
            this.lvwPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPlayers.FullRowSelect = true;
            this.lvwPlayers.GridLines = true;
            this.lvwPlayers.Location = new System.Drawing.Point(3, 19);
            this.lvwPlayers.Name = "lvwPlayers";
            this.lvwPlayers.Size = new System.Drawing.Size(593, 235);
            this.lvwPlayers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwPlayers.TabIndex = 0;
            this.lvwPlayers.UseCompatibleStateImageBehavior = false;
            this.lvwPlayers.View = System.Windows.Forms.View.Details;
            // 
            // firstNameColumn
            // 
            this.firstNameColumn.Text = "Vorname";
            // 
            // lastNameColumn
            // 
            this.lastNameColumn.Text = "Nachname";
            // 
            // birthdayColumn
            // 
            this.birthdayColumn.Text = "Geburtstag";
            // 
            // countryColumn
            // 
            this.countryColumn.Text = "Nationalität";
            // 
            // heightColumn
            // 
            this.heightColumn.Text = "Grösse [cm]";
            // 
            // weightColumn
            // 
            this.weightColumn.Text = "Gewicht [kg]";
            // 
            // remarkColumn
            // 
            this.remarkColumn.Text = "Bemerkung";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(655, 515);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 27);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpBoxNewPlayer
            // 
            this.grpBoxNewPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxNewPlayer.Controls.Add(this.btnNewPlayer);
            this.grpBoxNewPlayer.Controls.Add(this.btnExistingPlayer);
            this.grpBoxNewPlayer.Controls.Add(this.label1);
            this.grpBoxNewPlayer.Controls.Add(this.rtxtRemark);
            this.grpBoxNewPlayer.Controls.Add(this.lblRemark);
            this.grpBoxNewPlayer.Controls.Add(this.lblFirstName);
            this.grpBoxNewPlayer.Controls.Add(this.lblLastName);
            this.grpBoxNewPlayer.Controls.Add(this.txtWeight);
            this.grpBoxNewPlayer.Controls.Add(this.lblBirthday);
            this.grpBoxNewPlayer.Controls.Add(this.lblWeight);
            this.grpBoxNewPlayer.Controls.Add(this.lblCountry);
            this.grpBoxNewPlayer.Controls.Add(this.txtHeight);
            this.grpBoxNewPlayer.Controls.Add(this.txtFirstName);
            this.grpBoxNewPlayer.Controls.Add(this.lblHeight);
            this.grpBoxNewPlayer.Controls.Add(this.txtLastName);
            this.grpBoxNewPlayer.Controls.Add(this.cbxCountries);
            this.grpBoxNewPlayer.Controls.Add(this.datePicker);
            this.grpBoxNewPlayer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxNewPlayer.Location = new System.Drawing.Point(12, 12);
            this.grpBoxNewPlayer.Name = "grpBoxNewPlayer";
            this.grpBoxNewPlayer.Size = new System.Drawing.Size(752, 267);
            this.grpBoxNewPlayer.TabIndex = 28;
            this.grpBoxNewPlayer.TabStop = false;
            this.grpBoxNewPlayer.Text = "Neuer Spieler";
            // 
            // btnNewPlayer
            // 
            this.btnNewPlayer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPlayer.Location = new System.Drawing.Point(643, 225);
            this.btnNewPlayer.Name = "btnNewPlayer";
            this.btnNewPlayer.Size = new System.Drawing.Size(103, 27);
            this.btnNewPlayer.TabIndex = 30;
            this.btnNewPlayer.Text = "Neuer Spieler";
            this.btnNewPlayer.UseVisualStyleBackColor = true;
            this.btnNewPlayer.Click += new System.EventHandler(this.btnNewPlayer_Click);
            // 
            // btnExistingPlayer
            // 
            this.btnExistingPlayer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExistingPlayer.Location = new System.Drawing.Point(504, 225);
            this.btnExistingPlayer.Name = "btnExistingPlayer";
            this.btnExistingPlayer.Size = new System.Drawing.Size(133, 27);
            this.btnExistingPlayer.TabIndex = 29;
            this.btnExistingPlayer.Text = "Existierender Spieler";
            this.btnExistingPlayer.UseVisualStyleBackColor = true;
            this.btnExistingPlayer.Click += new System.EventHandler(this.btnExistingPlayer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "* Pflichtfeld";
            // 
            // rtxtRemark
            // 
            this.rtxtRemark.Enabled = false;
            this.rtxtRemark.Location = new System.Drawing.Point(504, 131);
            this.rtxtRemark.Name = "rtxtRemark";
            this.rtxtRemark.Size = new System.Drawing.Size(242, 75);
            this.rtxtRemark.TabIndex = 7;
            this.rtxtRemark.Text = "";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(362, 134);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(85, 16);
            this.lblRemark.TabIndex = 27;
            this.lblRemark.Text = "Bemerkung:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(6, 33);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(79, 16);
            this.lblFirstName.TabIndex = 14;
            this.lblFirstName.Text = "Vorname*:";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(6, 81);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(87, 16);
            this.lblLastName.TabIndex = 15;
            this.lblLastName.Text = "Nachname*:";
            // 
            // txtWeight
            // 
            this.txtWeight.Enabled = false;
            this.txtWeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeight.Location = new System.Drawing.Point(121, 183);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(218, 23);
            this.txtWeight.TabIndex = 6;
            this.txtWeight.Validated += new System.EventHandler(this.ValidatedDouble);
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBirthday.Location = new System.Drawing.Point(362, 35);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(113, 16);
            this.lblBirthday.TabIndex = 16;
            this.lblBirthday.Text = "Geburtsdatum*:";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.Location = new System.Drawing.Point(6, 186);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(95, 16);
            this.lblWeight.TabIndex = 24;
            this.lblWeight.Text = "Gewicht [kg]:";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(362, 83);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(96, 16);
            this.lblCountry.TabIndex = 17;
            this.lblCountry.Text = "Nationalität*:";
            // 
            // txtHeight
            // 
            this.txtHeight.Enabled = false;
            this.txtHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(121, 131);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(218, 23);
            this.txtHeight.TabIndex = 5;
            this.txtHeight.Validated += new System.EventHandler(this.ValidatedInteger);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Enabled = false;
            this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(121, 30);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(218, 23);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Validated += new System.EventHandler(this.ValidatedString);
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeight.Location = new System.Drawing.Point(6, 134);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(91, 16);
            this.lblHeight.TabIndex = 22;
            this.lblHeight.Text = "Grösse [cm]:";
            // 
            // txtLastName
            // 
            this.txtLastName.Enabled = false;
            this.txtLastName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(121, 78);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(218, 23);
            this.txtLastName.TabIndex = 2;
            this.txtLastName.Validated += new System.EventHandler(this.ValidatedString);
            // 
            // cbxCountries
            // 
            this.cbxCountries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCountries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCountries.DropDownHeight = 50;
            this.cbxCountries.Enabled = false;
            this.cbxCountries.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCountries.FormattingEnabled = true;
            this.cbxCountries.IntegralHeight = false;
            this.cbxCountries.Location = new System.Drawing.Point(504, 80);
            this.cbxCountries.Name = "cbxCountries";
            this.cbxCountries.Size = new System.Drawing.Size(242, 24);
            this.cbxCountries.TabIndex = 4;
            this.cbxCountries.Validated += new System.EventHandler(this.ValidatedString);
            // 
            // datePicker
            // 
            this.datePicker.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePicker.Enabled = false;
            this.datePicker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(504, 28);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(242, 23);
            this.datePicker.TabIndex = 3;
            // 
            // EditTeamPlayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(776, 554);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grpBoxCurrentPlayers);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpBoxNewPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTeamPlayersForm";
            this.Load += new System.EventHandler(this.EditPlayersForm_Load);
            this.grpBoxCurrentPlayers.ResumeLayout(false);
            this.grpBoxNewPlayer.ResumeLayout(false);
            this.grpBoxNewPlayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.RichTextBox rtxtRemark;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.ComboBox cbxCountries;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblBirthday;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.GroupBox grpBoxNewPlayer;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpBoxCurrentPlayers;
        private System.Windows.Forms.ListView lvwPlayers;
        private System.Windows.Forms.ColumnHeader firstNameColumn;
        private System.Windows.Forms.ColumnHeader lastNameColumn;
        private System.Windows.Forms.ColumnHeader birthdayColumn;
        private System.Windows.Forms.ColumnHeader countryColumn;
        private System.Windows.Forms.ColumnHeader heightColumn;
        private System.Windows.Forms.ColumnHeader weightColumn;
        private System.Windows.Forms.ColumnHeader remarkColumn;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExistingPlayer;
        private System.Windows.Forms.Button btnNewPlayer;
    }
}
