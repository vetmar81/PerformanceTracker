namespace Vema.PerformanceTracker.UI.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox grpBoxPlayer;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.GroupBox grpBoxTeam;
            this.tabPlayerData = new System.Windows.Forms.TabControl();
            this.contextMenuStripPlayerData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabPagePhysicalData = new System.Windows.Forms.TabPage();
            this.dgvPlayerHistory = new System.Windows.Forms.DataGridView();
            this.playerHeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerWeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerHistoryRemarkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerHistoryDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPagePerformanceData = new System.Windows.Forms.TabPage();
            this.dgvPlayerMeasurements = new System.Windows.Forms.DataGridView();
            this.playerMeasurementValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerMeasurementUnitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerMeasurementCategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerMeasurementSubCategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerMeasurementRemarkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerMeasurementTimestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlayers = new System.Windows.Forms.DataGridView();
            this.lastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthdayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripPlayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblCreationDateValue = new System.Windows.Forms.Label();
            this.lblAgeGroupValue = new System.Windows.Forms.Label();
            this.lblNameValue = new System.Windows.Forms.Label();
            this.lblCreationDate = new System.Windows.Forms.Label();
            this.lblAgeGroup = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTeamInfo = new System.Windows.Forms.Label();
            this.lblTeams = new System.Windows.Forms.Label();
            this.cbxTeams = new System.Windows.Forms.ComboBox();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTeamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editTeamPlayersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editPlayerDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newMeasurementMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMeasurementMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTeam = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipTeamSelection = new System.Windows.Forms.ToolTip(this.components);
            grpBoxPlayer = new System.Windows.Forms.GroupBox();
            grpBoxTeam = new System.Windows.Forms.GroupBox();
            grpBoxPlayer.SuspendLayout();
            this.tabPlayerData.SuspendLayout();
            this.tabPagePhysicalData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerHistory)).BeginInit();
            this.tabPagePerformanceData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerMeasurements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).BeginInit();
            grpBoxTeam.SuspendLayout();
            this.titlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxPlayer
            // 
            grpBoxPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grpBoxPlayer.Controls.Add(this.tabPlayerData);
            grpBoxPlayer.Controls.Add(this.dgvPlayers);
            grpBoxPlayer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            grpBoxPlayer.Location = new System.Drawing.Point(208, 97);
            grpBoxPlayer.Name = "grpBoxPlayer";
            grpBoxPlayer.Size = new System.Drawing.Size(798, 605);
            grpBoxPlayer.TabIndex = 2;
            grpBoxPlayer.TabStop = false;
            grpBoxPlayer.Text = "Spieler Informationen:";
            // 
            // tabPlayerData
            // 
            this.tabPlayerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPlayerData.ContextMenuStrip = this.contextMenuStripPlayerData;
            this.tabPlayerData.Controls.Add(this.tabPagePhysicalData);
            this.tabPlayerData.Controls.Add(this.tabPagePerformanceData);
            this.tabPlayerData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPlayerData.Location = new System.Drawing.Point(28, 365);
            this.tabPlayerData.Name = "tabPlayerData";
            this.tabPlayerData.SelectedIndex = 0;
            this.tabPlayerData.Size = new System.Drawing.Size(742, 234);
            this.tabPlayerData.TabIndex = 1;
            // 
            // contextMenuStripPlayerData
            // 
            this.contextMenuStripPlayerData.Name = "contextMenuStripPlayerData";
            this.contextMenuStripPlayerData.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStripPlayerData.Size = new System.Drawing.Size(61, 4);
            // 
            // tabPagePhysicalData
            // 
            this.tabPagePhysicalData.Controls.Add(this.dgvPlayerHistory);
            this.tabPagePhysicalData.Location = new System.Drawing.Point(4, 25);
            this.tabPagePhysicalData.Name = "tabPagePhysicalData";
            this.tabPagePhysicalData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePhysicalData.Size = new System.Drawing.Size(734, 205);
            this.tabPagePhysicalData.TabIndex = 0;
            this.tabPagePhysicalData.Text = "Physische Daten";
            this.tabPagePhysicalData.ToolTipText = "Zeigt die physischen Daten des ausgewählten Spielers an.";
            this.tabPagePhysicalData.UseVisualStyleBackColor = true;
            // 
            // dgvPlayerHistory
            // 
            this.dgvPlayerHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlayerHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayerHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerHeightColumn,
            this.playerWeightColumn,
            this.playerHistoryRemarkColumn,
            this.playerHistoryDateColumn});
            this.dgvPlayerHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlayerHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPlayerHistory.Location = new System.Drawing.Point(3, 3);
            this.dgvPlayerHistory.Name = "dgvPlayerHistory";
            this.dgvPlayerHistory.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPlayerHistory.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPlayerHistory.RowTemplate.Height = 25;
            this.dgvPlayerHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayerHistory.ShowEditingIcon = false;
            this.dgvPlayerHistory.Size = new System.Drawing.Size(728, 199);
            this.dgvPlayerHistory.TabIndex = 2;
            // 
            // playerHeightColumn
            // 
            this.playerHeightColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerHeightColumn.DataPropertyName = "Height";
            dataGridViewCellStyle1.NullValue = "-";
            this.playerHeightColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.playerHeightColumn.HeaderText = "Grösse [cm]";
            this.playerHeightColumn.Name = "playerHeightColumn";
            this.playerHeightColumn.ReadOnly = true;
            // 
            // playerWeightColumn
            // 
            this.playerWeightColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerWeightColumn.DataPropertyName = "Weight";
            dataGridViewCellStyle2.Format = "N1";
            dataGridViewCellStyle2.NullValue = "-";
            this.playerWeightColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.playerWeightColumn.HeaderText = "Gewicht [kg]";
            this.playerWeightColumn.Name = "playerWeightColumn";
            this.playerWeightColumn.ReadOnly = true;
            // 
            // playerHistoryRemarkColumn
            // 
            this.playerHistoryRemarkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerHistoryRemarkColumn.DataPropertyName = "Remark";
            dataGridViewCellStyle3.NullValue = "-";
            this.playerHistoryRemarkColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.playerHistoryRemarkColumn.HeaderText = "Bemerkung";
            this.playerHistoryRemarkColumn.Name = "playerHistoryRemarkColumn";
            this.playerHistoryRemarkColumn.ReadOnly = true;
            // 
            // playerHistoryDateColumn
            // 
            this.playerHistoryDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerHistoryDateColumn.DataPropertyName = "Date";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.playerHistoryDateColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.playerHistoryDateColumn.HeaderText = "Datum";
            this.playerHistoryDateColumn.Name = "playerHistoryDateColumn";
            this.playerHistoryDateColumn.ReadOnly = true;
            // 
            // tabPagePerformanceData
            // 
            this.tabPagePerformanceData.Controls.Add(this.dgvPlayerMeasurements);
            this.tabPagePerformanceData.Location = new System.Drawing.Point(4, 25);
            this.tabPagePerformanceData.Name = "tabPagePerformanceData";
            this.tabPagePerformanceData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePerformanceData.Size = new System.Drawing.Size(734, 205);
            this.tabPagePerformanceData.TabIndex = 1;
            this.tabPagePerformanceData.Text = "Leistungsdiagnostik Daten";
            this.tabPagePerformanceData.ToolTipText = "Zeigt die leistungsdiagnostischen Daten des ausgewählten Spielers an";
            this.tabPagePerformanceData.UseVisualStyleBackColor = true;
            // 
            // dgvPlayerMeasurements
            // 
            this.dgvPlayerMeasurements.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlayerMeasurements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayerMeasurements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerMeasurementValueColumn,
            this.playerMeasurementUnitColumn,
            this.playerMeasurementCategoryColumn,
            this.playerMeasurementSubCategoryColumn,
            this.playerMeasurementRemarkColumn,
            this.playerMeasurementTimestampColumn});
            this.dgvPlayerMeasurements.ContextMenuStrip = this.contextMenuStripPlayerData;
            this.dgvPlayerMeasurements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlayerMeasurements.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPlayerMeasurements.Location = new System.Drawing.Point(3, 3);
            this.dgvPlayerMeasurements.Name = "dgvPlayerMeasurements";
            this.dgvPlayerMeasurements.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPlayerMeasurements.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPlayerMeasurements.RowTemplate.Height = 25;
            this.dgvPlayerMeasurements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayerMeasurements.ShowEditingIcon = false;
            this.dgvPlayerMeasurements.Size = new System.Drawing.Size(728, 199);
            this.dgvPlayerMeasurements.TabIndex = 2;
            // 
            // playerMeasurementValueColumn
            // 
            this.playerMeasurementValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerMeasurementValueColumn.DataPropertyName = "Value";
            this.playerMeasurementValueColumn.HeaderText = "Messwert";
            this.playerMeasurementValueColumn.Name = "playerMeasurementValueColumn";
            this.playerMeasurementValueColumn.ReadOnly = true;
            // 
            // playerMeasurementUnitColumn
            // 
            this.playerMeasurementUnitColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerMeasurementUnitColumn.DataPropertyName = "Unit";
            this.playerMeasurementUnitColumn.HeaderText = "Einheit";
            this.playerMeasurementUnitColumn.Name = "playerMeasurementUnitColumn";
            this.playerMeasurementUnitColumn.ReadOnly = true;
            // 
            // playerMeasurementCategoryColumn
            // 
            this.playerMeasurementCategoryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerMeasurementCategoryColumn.DataPropertyName = "Category";
            this.playerMeasurementCategoryColumn.HeaderText = "Kategorie";
            this.playerMeasurementCategoryColumn.Name = "playerMeasurementCategoryColumn";
            this.playerMeasurementCategoryColumn.ReadOnly = true;
            // 
            // playerMeasurementSubCategoryColumn
            // 
            this.playerMeasurementSubCategoryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerMeasurementSubCategoryColumn.DataPropertyName = "SubCategory";
            this.playerMeasurementSubCategoryColumn.HeaderText = "Unterkategorie";
            this.playerMeasurementSubCategoryColumn.Name = "playerMeasurementSubCategoryColumn";
            this.playerMeasurementSubCategoryColumn.ReadOnly = true;
            // 
            // playerMeasurementRemarkColumn
            // 
            this.playerMeasurementRemarkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerMeasurementRemarkColumn.DataPropertyName = "Remark";
            dataGridViewCellStyle6.NullValue = "-";
            this.playerMeasurementRemarkColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.playerMeasurementRemarkColumn.HeaderText = "Bemerkung";
            this.playerMeasurementRemarkColumn.Name = "playerMeasurementRemarkColumn";
            this.playerMeasurementRemarkColumn.ReadOnly = true;
            // 
            // playerMeasurementTimestampColumn
            // 
            this.playerMeasurementTimestampColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerMeasurementTimestampColumn.DataPropertyName = "Timestamp";
            dataGridViewCellStyle7.Format = "G";
            this.playerMeasurementTimestampColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.playerMeasurementTimestampColumn.HeaderText = "Zeitstempel";
            this.playerMeasurementTimestampColumn.Name = "playerMeasurementTimestampColumn";
            this.playerMeasurementTimestampColumn.ReadOnly = true;
            // 
            // dgvPlayers
            // 
            this.dgvPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPlayers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPlayers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPlayers.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvPlayers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lastNameColumn,
            this.firstNameColumn,
            this.birthdayColumn,
            this.ageColumn,
            this.countryColumn});
            this.dgvPlayers.ContextMenuStrip = this.contextMenuStripPlayer;
            this.dgvPlayers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPlayers.Location = new System.Drawing.Point(28, 34);
            this.dgvPlayers.MultiSelect = false;
            this.dgvPlayers.Name = "dgvPlayers";
            this.dgvPlayers.ReadOnly = true;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPlayers.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvPlayers.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvPlayers.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPlayers.RowTemplate.Height = 25;
            this.dgvPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayers.ShowEditingIcon = false;
            this.dgvPlayers.Size = new System.Drawing.Size(742, 312);
            this.dgvPlayers.TabIndex = 1;
            // 
            // lastNameColumn
            // 
            this.lastNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lastNameColumn.DataPropertyName = "LastName";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastNameColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.lastNameColumn.HeaderText = "Nachname";
            this.lastNameColumn.Name = "lastNameColumn";
            this.lastNameColumn.ReadOnly = true;
            // 
            // firstNameColumn
            // 
            this.firstNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.firstNameColumn.DataPropertyName = "FirstName";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstNameColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.firstNameColumn.HeaderText = "Vorname";
            this.firstNameColumn.Name = "firstNameColumn";
            this.firstNameColumn.ReadOnly = true;
            // 
            // birthdayColumn
            // 
            this.birthdayColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.birthdayColumn.DataPropertyName = "Birthday";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.Format = "d";
            this.birthdayColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.birthdayColumn.HeaderText = "Geburtstag";
            this.birthdayColumn.Name = "birthdayColumn";
            this.birthdayColumn.ReadOnly = true;
            // 
            // ageColumn
            // 
            this.ageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ageColumn.DataPropertyName = "Age";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.ageColumn.HeaderText = "Alter";
            this.ageColumn.Name = "ageColumn";
            this.ageColumn.ReadOnly = true;
            // 
            // countryColumn
            // 
            this.countryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.countryColumn.DataPropertyName = "Country";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.countryColumn.HeaderText = "Nationalität";
            this.countryColumn.Name = "countryColumn";
            this.countryColumn.ReadOnly = true;
            // 
            // contextMenuStripPlayer
            // 
            this.contextMenuStripPlayer.Name = "contextMenuStripPlayer";
            this.contextMenuStripPlayer.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStripPlayer.Size = new System.Drawing.Size(61, 4);
            // 
            // grpBoxTeam
            // 
            grpBoxTeam.Controls.Add(this.lblCreationDateValue);
            grpBoxTeam.Controls.Add(this.lblAgeGroupValue);
            grpBoxTeam.Controls.Add(this.lblNameValue);
            grpBoxTeam.Controls.Add(this.lblCreationDate);
            grpBoxTeam.Controls.Add(this.lblAgeGroup);
            grpBoxTeam.Controls.Add(this.lblName);
            grpBoxTeam.Controls.Add(this.lblTeamInfo);
            grpBoxTeam.Controls.Add(this.lblTeams);
            grpBoxTeam.Controls.Add(this.cbxTeams);
            grpBoxTeam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            grpBoxTeam.Location = new System.Drawing.Point(12, 97);
            grpBoxTeam.Name = "grpBoxTeam";
            grpBoxTeam.Size = new System.Drawing.Size(190, 346);
            grpBoxTeam.TabIndex = 0;
            grpBoxTeam.TabStop = false;
            grpBoxTeam.Text = "Team Navigation:";
            this.toolTipTeamSelection.SetToolTip(grpBoxTeam, "Wählen Sie eine Mannschaft für den aktuellen Kontext aus.\r\nAlle weiteren Operatio" +
        "nen werden für die ausgewählte Mannschaft ausgeführt.");
            // 
            // lblCreationDateValue
            // 
            this.lblCreationDateValue.AutoSize = true;
            this.lblCreationDateValue.Location = new System.Drawing.Point(86, 315);
            this.lblCreationDateValue.Name = "lblCreationDateValue";
            this.lblCreationDateValue.Size = new System.Drawing.Size(80, 16);
            this.lblCreationDateValue.TabIndex = 12;
            this.lblCreationDateValue.Text = "creationDate";
            // 
            // lblAgeGroupValue
            // 
            this.lblAgeGroupValue.AutoSize = true;
            this.lblAgeGroupValue.Location = new System.Drawing.Point(86, 289);
            this.lblAgeGroupValue.Name = "lblAgeGroupValue";
            this.lblAgeGroupValue.Size = new System.Drawing.Size(63, 16);
            this.lblAgeGroupValue.TabIndex = 11;
            this.lblAgeGroupValue.Text = "ageGroup";
            // 
            // lblNameValue
            // 
            this.lblNameValue.AutoSize = true;
            this.lblNameValue.Location = new System.Drawing.Point(86, 263);
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.Size = new System.Drawing.Size(70, 16);
            this.lblNameValue.TabIndex = 10;
            this.lblNameValue.Text = "teamName";
            // 
            // lblCreationDate
            // 
            this.lblCreationDate.AutoSize = true;
            this.lblCreationDate.Location = new System.Drawing.Point(6, 315);
            this.lblCreationDate.Name = "lblCreationDate";
            this.lblCreationDate.Size = new System.Drawing.Size(74, 16);
            this.lblCreationDate.TabIndex = 9;
            this.lblCreationDate.Text = "Erfasst am:";
            // 
            // lblAgeGroup
            // 
            this.lblAgeGroup.AutoSize = true;
            this.lblAgeGroup.Location = new System.Drawing.Point(6, 289);
            this.lblAgeGroup.Name = "lblAgeGroup";
            this.lblAgeGroup.Size = new System.Drawing.Size(65, 16);
            this.lblAgeGroup.TabIndex = 8;
            this.lblAgeGroup.Text = "Jahrgang:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 263);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 16);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Name:";
            // 
            // lblTeamInfo
            // 
            this.lblTeamInfo.AutoSize = true;
            this.lblTeamInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeamInfo.Location = new System.Drawing.Point(6, 231);
            this.lblTeamInfo.Name = "lblTeamInfo";
            this.lblTeamInfo.Size = new System.Drawing.Size(139, 16);
            this.lblTeamInfo.TabIndex = 6;
            this.lblTeamInfo.Text = "Mannschaftsdetails:";
            // 
            // lblTeams
            // 
            this.lblTeams.AutoSize = true;
            this.lblTeams.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeams.Location = new System.Drawing.Point(6, 30);
            this.lblTeams.Name = "lblTeams";
            this.lblTeams.Size = new System.Drawing.Size(143, 16);
            this.lblTeams.TabIndex = 5;
            this.lblTeams.Text = "Mannschaft wählen: ";
            // 
            // cbxTeams
            // 
            this.cbxTeams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTeams.BackColor = System.Drawing.Color.White;
            this.cbxTeams.DropDownHeight = 120;
            this.cbxTeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTeams.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTeams.FormattingEnabled = true;
            this.cbxTeams.IntegralHeight = false;
            this.cbxTeams.Location = new System.Drawing.Point(9, 58);
            this.cbxTeams.MaxDropDownItems = 10;
            this.cbxTeams.Name = "cbxTeams";
            this.cbxTeams.Size = new System.Drawing.Size(165, 24);
            this.cbxTeams.Sorted = true;
            this.cbxTeams.TabIndex = 4;
            this.toolTipTeamSelection.SetToolTip(this.cbxTeams, "Wählen Sie eine Mannschaft für den aktuellen Kontext aus.\r\nAlle weiteren Operatio" +
        "nen werden für die ausgewählte Mannschaft ausgeführt.\r\n");
            // 
            // titlePanel
            // 
            this.titlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel.Controls.Add(this.pbxLogo);
            this.titlePanel.Controls.Add(this.lblTitle);
            this.titlePanel.Location = new System.Drawing.Point(0, 24);
            this.titlePanel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(1018, 67);
            this.titlePanel.TabIndex = 1;
            // 
            // pbxLogo
            // 
            this.pbxLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbxLogo.Image = global::Vema.PerformanceTracker.Properties.Resources.PerformanceTracker_64x64;
            this.pbxLogo.Location = new System.Drawing.Point(948, 4);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(67, 61);
            this.pbxLogo.TabIndex = 1;
            this.pbxLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(588, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(174, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "toolName";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.LightSkyBlue;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.teamMenuItem,
            this.playerMenuItem,
            this.analysisMenuItem,
            this.infoMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(46, 20);
            this.fileMenuItem.Text = "&Datei";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exitMenuItem.Text = "Beenden";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // teamMenuItem
            // 
            this.teamMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTeamMenuItem,
            this.toolStripSeparator3,
            this.editTeamPlayersMenuItem});
            this.teamMenuItem.Name = "teamMenuItem";
            this.teamMenuItem.Size = new System.Drawing.Size(82, 20);
            this.teamMenuItem.Text = "Mannschaft";
            // 
            // newTeamMenuItem
            // 
            this.newTeamMenuItem.Name = "newTeamMenuItem";
            this.newTeamMenuItem.Size = new System.Drawing.Size(226, 22);
            this.newTeamMenuItem.Text = "Neue Mannschaft erfassen ...";
            this.newTeamMenuItem.Click += new System.EventHandler(this.newTeamMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(223, 6);
            // 
            // editTeamPlayersMenuItem
            // 
            this.editTeamPlayersMenuItem.Name = "editTeamPlayersMenuItem";
            this.editTeamPlayersMenuItem.Size = new System.Drawing.Size(226, 22);
            this.editTeamPlayersMenuItem.Text = "Spieler bearbeiten ...";
            this.editTeamPlayersMenuItem.Click += new System.EventHandler(this.editTeamPlayersMenuItem_Click);
            // 
            // playerMenuItem
            // 
            this.playerMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlayerMenuItem,
            this.toolStripSeparator1,
            this.editPlayerDataMenuItem,
            this.toolStripSeparator2,
            this.newMeasurementMenuItem,
            this.editMeasurementMenuItem});
            this.playerMenuItem.Name = "playerMenuItem";
            this.playerMenuItem.Size = new System.Drawing.Size(54, 20);
            this.playerMenuItem.Text = "Spieler";
            // 
            // newPlayerMenuItem
            // 
            this.newPlayerMenuItem.Name = "newPlayerMenuItem";
            this.newPlayerMenuItem.Size = new System.Drawing.Size(292, 22);
            this.newPlayerMenuItem.Text = "Spieler erfassen ...";
            this.newPlayerMenuItem.Click += new System.EventHandler(this.newPlayerMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(289, 6);
            // 
            // editPlayerDataMenuItem
            // 
            this.editPlayerDataMenuItem.Name = "editPlayerDataMenuItem";
            this.editPlayerDataMenuItem.Size = new System.Drawing.Size(292, 22);
            this.editPlayerDataMenuItem.Text = "Physische Daten bearbeiten ...";
            this.editPlayerDataMenuItem.Click += new System.EventHandler(this.editPlayerDataMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(289, 6);
            // 
            // newMeasurementMenuItem
            // 
            this.newMeasurementMenuItem.Name = "newMeasurementMenuItem";
            this.newMeasurementMenuItem.Size = new System.Drawing.Size(292, 22);
            this.newMeasurementMenuItem.Text = "Leistungsdiagnostik - Daten erfassen ...";
            // 
            // editMeasurementMenuItem
            // 
            this.editMeasurementMenuItem.Name = "editMeasurementMenuItem";
            this.editMeasurementMenuItem.Size = new System.Drawing.Size(292, 22);
            this.editMeasurementMenuItem.Text = "Leistungsdiagnostik - Daten bearbeiten ...";
            // 
            // analysisMenuItem
            // 
            this.analysisMenuItem.Name = "analysisMenuItem";
            this.analysisMenuItem.Size = new System.Drawing.Size(60, 20);
            this.analysisMenuItem.Text = "Analyse";
            // 
            // infoMenuItem
            // 
            this.infoMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.infoMenuItem.Name = "infoMenuItem";
            this.infoMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoMenuItem.Text = "&Info";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(111, 22);
            this.aboutMenuItem.Text = "&Über ...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // contextMenuStripTeam
            // 
            this.contextMenuStripTeam.Name = "contextMenuStripTeam";
            this.contextMenuStripTeam.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStripTeam.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.LightSkyBlue;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 718);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1018, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "Status";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Status";
            // 
            // toolTipTeamSelection
            // 
            this.toolTipTeamSelection.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipTeamSelection.ToolTipTitle = "Mannschaft wählen";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(grpBoxTeam);
            this.Controls.Add(grpBoxPlayer);
            this.Controls.Add(this.titlePanel);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.MainForm_Load);
            grpBoxPlayer.ResumeLayout(false);
            this.tabPlayerData.ResumeLayout(false);
            this.tabPagePhysicalData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerHistory)).EndInit();
            this.tabPagePerformanceData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerMeasurements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).EndInit();
            grpBoxTeam.ResumeLayout(false);
            grpBoxTeam.PerformLayout();
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbxLogo;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.DataGridView dgvPlayerMeasurements;
        private System.Windows.Forms.DataGridView dgvPlayerHistory;
        private System.Windows.Forms.DataGridView dgvPlayers;
        private System.Windows.Forms.TabControl tabPlayerData;
        private System.Windows.Forms.TabPage tabPagePhysicalData;
        private System.Windows.Forms.TabPage tabPagePerformanceData;
        private System.Windows.Forms.ToolStripMenuItem teamMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPlayerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editPlayerDataMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem newMeasurementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMeasurementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTeamMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPlayer;
        private System.Windows.Forms.ToolStripMenuItem analysisMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPlayerData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTeam;
        private System.Windows.Forms.Label lblTeams;
        private System.Windows.Forms.ComboBox cbxTeams;
        private System.Windows.Forms.Label lblAgeGroup;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTeamInfo;
        private System.Windows.Forms.Label lblCreationDateValue;
        private System.Windows.Forms.Label lblAgeGroupValue;
        private System.Windows.Forms.Label lblNameValue;
        private System.Windows.Forms.Label lblCreationDate;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthdayColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countryColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editTeamPlayersMenuItem;
        private System.Windows.Forms.ToolTip toolTipTeamSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerHeightColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerWeightColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerHistoryRemarkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerHistoryDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMeasurementValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMeasurementUnitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMeasurementCategoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMeasurementSubCategoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMeasurementRemarkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMeasurementTimestampColumn;
    }
}

