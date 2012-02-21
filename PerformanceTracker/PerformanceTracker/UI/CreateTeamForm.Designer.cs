namespace Vema.PerformanceTracker.UI
{
    partial class CreateTeamForm
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
            this.txtDescriptor = new System.Windows.Forms.TextBox();
            this.lblDescriptor = new System.Windows.Forms.Label();
            this.txtAgeGroup = new System.Windows.Forms.TextBox();
            this.lblAgeGroup = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDescriptor
            // 
            this.txtDescriptor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescriptor.Location = new System.Drawing.Point(120, 12);
            this.txtDescriptor.Name = "txtDescriptor";
            this.txtDescriptor.Size = new System.Drawing.Size(225, 23);
            this.txtDescriptor.TabIndex = 0;
            // 
            // lblDescriptor
            // 
            this.lblDescriptor.AutoSize = true;
            this.lblDescriptor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptor.Location = new System.Drawing.Point(12, 15);
            this.lblDescriptor.Name = "lblDescriptor";
            this.lblDescriptor.Size = new System.Drawing.Size(102, 16);
            this.lblDescriptor.TabIndex = 1;
            this.lblDescriptor.Text = "Bezeichnung:*";
            // 
            // txtAgeGroup
            // 
            this.txtAgeGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAgeGroup.Location = new System.Drawing.Point(120, 52);
            this.txtAgeGroup.Name = "txtAgeGroup";
            this.txtAgeGroup.Size = new System.Drawing.Size(225, 23);
            this.txtAgeGroup.TabIndex = 2;
            // 
            // lblAgeGroup
            // 
            this.lblAgeGroup.AutoSize = true;
            this.lblAgeGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgeGroup.Location = new System.Drawing.Point(12, 55);
            this.lblAgeGroup.Name = "lblAgeGroup";
            this.lblAgeGroup.Size = new System.Drawing.Size(81, 16);
            this.lblAgeGroup.TabIndex = 3;
            this.lblAgeGroup.Text = "Jahrgänge:";
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(260, 94);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(85, 29);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Erzeugen";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.LightSkyBlue;
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.status.Location = new System.Drawing.Point(0, 136);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(357, 22);
            this.status.TabIndex = 5;
            this.status.Text = "Status";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.LightSkyBlue;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "* Pflichtfeld";
            // 
            // CreateTeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(357, 158);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblAgeGroup);
            this.Controls.Add(this.txtAgeGroup);
            this.Controls.Add(this.lblDescriptor);
            this.Controls.Add(this.txtDescriptor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateTeamForm";
            this.Text = "";
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescriptor;
        private System.Windows.Forms.Label lblDescriptor;
        private System.Windows.Forms.TextBox txtAgeGroup;
        private System.Windows.Forms.Label lblAgeGroup;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label label1;
    }
}
