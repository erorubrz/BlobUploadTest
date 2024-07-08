namespace BlobUploadTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxTenantId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBoxClientId = new TextBox();
            label3 = new Label();
            textBoxClientSecret = new TextBox();
            checkBoxUseTokenAuth = new CheckBox();
            buttonUploadFile = new Button();
            label4 = new Label();
            textBoxFile = new TextBox();
            textBoxLog = new TextBox();
            label5 = new Label();
            textBoxBlobUrl = new TextBox();
            label6 = new Label();
            label7 = new Label();
            textBoxBlobCS = new TextBox();
            label8 = new Label();
            textBoxBlobContainerName = new TextBox();
            SuspendLayout();
            // 
            // textBoxTenantId
            // 
            textBoxTenantId.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTenantId.Location = new Point(125, 27);
            textBoxTenantId.Margin = new Padding(3, 2, 3, 2);
            textBoxTenantId.Name = "textBoxTenantId";
            textBoxTenantId.Size = new Size(382, 23);
            textBoxTenantId.TabIndex = 0;
            textBoxTenantId.Text = "";
            textBoxTenantId.TextChanged += textBoxTenantId_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 29);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 1;
            label1.Text = "TenantId:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 54);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 3;
            label2.Text = "ClientId:";
            // 
            // textBoxClientId
            // 
            textBoxClientId.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxClientId.Location = new Point(125, 52);
            textBoxClientId.Margin = new Padding(3, 2, 3, 2);
            textBoxClientId.Name = "textBoxClientId";
            textBoxClientId.Size = new Size(382, 23);
            textBoxClientId.TabIndex = 2;
            textBoxClientId.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 79);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 5;
            label3.Text = "ClientSecret:";
            // 
            // textBoxClientSecret
            // 
            textBoxClientSecret.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxClientSecret.Location = new Point(125, 76);
            textBoxClientSecret.Margin = new Padding(3, 2, 3, 2);
            textBoxClientSecret.Name = "textBoxClientSecret";
            textBoxClientSecret.Size = new Size(382, 23);
            textBoxClientSecret.TabIndex = 4;
            textBoxClientSecret.Text = "";
            // 
            // checkBoxUseTokenAuth
            // 
            checkBoxUseTokenAuth.AutoSize = true;
            checkBoxUseTokenAuth.Checked = true;
            checkBoxUseTokenAuth.CheckState = CheckState.Checked;
            checkBoxUseTokenAuth.Location = new Point(13, 9);
            checkBoxUseTokenAuth.Margin = new Padding(3, 2, 3, 2);
            checkBoxUseTokenAuth.Name = "checkBoxUseTokenAuth";
            checkBoxUseTokenAuth.Size = new Size(108, 19);
            checkBoxUseTokenAuth.TabIndex = 6;
            checkBoxUseTokenAuth.Text = "Use Token Auth";
            checkBoxUseTokenAuth.UseVisualStyleBackColor = true;
            checkBoxUseTokenAuth.CheckedChanged += checkBoxUseTokenAuth_CheckedChanged;
            // 
            // buttonUploadFile
            // 
            buttonUploadFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonUploadFile.Location = new Point(414, 194);
            buttonUploadFile.Margin = new Padding(3, 2, 3, 2);
            buttonUploadFile.Name = "buttonUploadFile";
            buttonUploadFile.Size = new Size(85, 23);
            buttonUploadFile.TabIndex = 7;
            buttonUploadFile.Text = "Upload File";
            buttonUploadFile.UseVisualStyleBackColor = true;
            buttonUploadFile.Click += buttonUploadFile_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 196);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 9;
            label4.Text = "File to upload:";
            // 
            // textBoxFile
            // 
            textBoxFile.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxFile.Location = new Point(126, 194);
            textBoxFile.Margin = new Padding(3, 2, 3, 2);
            textBoxFile.Name = "textBoxFile";
            textBoxFile.Size = new Size(283, 23);
            textBoxFile.TabIndex = 8;
            textBoxFile.Text = "C:\\test.zip";
            // 
            // textBoxLog
            // 
            textBoxLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxLog.Location = new Point(6, 253);
            textBoxLog.Margin = new Padding(3, 2, 3, 2);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.Size = new Size(517, 152);
            textBoxLog.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 227);
            label5.Name = "label5";
            label5.Size = new Size(30, 15);
            label5.TabIndex = 11;
            label5.Text = "Log:";
            // 
            // textBoxBlobUrl
            // 
            textBoxBlobUrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxBlobUrl.Location = new Point(126, 169);
            textBoxBlobUrl.Margin = new Padding(3, 2, 3, 2);
            textBoxBlobUrl.Name = "textBoxBlobUrl";
            textBoxBlobUrl.Size = new Size(382, 23);
            textBoxBlobUrl.TabIndex = 12;
            textBoxBlobUrl.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 174);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 13;
            label6.Text = "Blob URL:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 116);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 15;
            label7.Text = "Blob CS:";
            label7.Click += label7_Click;
            // 
            // textBoxBlobCS
            // 
            textBoxBlobCS.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxBlobCS.Enabled = false;
            textBoxBlobCS.Location = new Point(72, 113);
            textBoxBlobCS.Margin = new Padding(3, 2, 3, 2);
            textBoxBlobCS.Name = "textBoxBlobCS";
            textBoxBlobCS.Size = new Size(428, 23);
            textBoxBlobCS.TabIndex = 14;
            textBoxBlobCS.Text = "";
            textBoxBlobCS.TextChanged += textBoxBlobCS_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 144);
            label8.Name = "label8";
            label8.Size = new Size(121, 15);
            label8.TabIndex = 17;
            label8.Text = "Blob ContainerName:";
            // 
            // textBoxBlobContainerName
            // 
            textBoxBlobContainerName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxBlobContainerName.Location = new Point(170, 144);
            textBoxBlobContainerName.Margin = new Padding(3, 2, 3, 2);
            textBoxBlobContainerName.Name = "textBoxBlobContainerName";
            textBoxBlobContainerName.Size = new Size(337, 23);
            textBoxBlobContainerName.TabIndex = 16;
            textBoxBlobContainerName.Text = "cs-pushes";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(530, 407);
            Controls.Add(label8);
            Controls.Add(textBoxBlobContainerName);
            Controls.Add(label7);
            Controls.Add(textBoxBlobCS);
            Controls.Add(label6);
            Controls.Add(textBoxBlobUrl);
            Controls.Add(label5);
            Controls.Add(textBoxLog);
            Controls.Add(label4);
            Controls.Add(textBoxFile);
            Controls.Add(buttonUploadFile);
            Controls.Add(checkBoxUseTokenAuth);
            Controls.Add(label3);
            Controls.Add(textBoxClientSecret);
            Controls.Add(label2);
            Controls.Add(textBoxClientId);
            Controls.Add(label1);
            Controls.Add(textBoxTenantId);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxTenantId;
        private Label label1;
        private Label label2;
        private TextBox textBoxClientId;
        private Label label3;
        private TextBox textBoxClientSecret;
        private CheckBox checkBoxUseTokenAuth;
        private Button buttonUploadFile;
        private Label label4;
        private TextBox textBoxFile;
        private TextBox textBoxLog;
        private Label label5;
        private TextBox textBoxBlobUrl;
        private Label label6;
        private Label label7;
        private TextBox textBoxBlobCS;
        private Label label8;
        private TextBox textBoxBlobContainerName;
    }
}
