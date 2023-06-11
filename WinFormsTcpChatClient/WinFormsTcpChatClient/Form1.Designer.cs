namespace WinFormsTcpChatClient
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
            btnConnect = new Button();
            txtName = new TextBox();
            label1 = new Label();
            txtChatMsg = new TextBox();
            txtMsg = new TextBox();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(446, 33);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(108, 32);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "입장";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(77, 33);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 36);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 2;
            label1.Text = "대화명";
            // 
            // txtChatMsg
            // 
            txtChatMsg.Location = new Point(28, 83);
            txtChatMsg.Multiline = true;
            txtChatMsg.Name = "txtChatMsg";
            txtChatMsg.ScrollBars = ScrollBars.Vertical;
            txtChatMsg.Size = new Size(526, 238);
            txtChatMsg.TabIndex = 3;
            // 
            // txtMsg
            // 
            txtMsg.Location = new Point(28, 353);
            txtMsg.Name = "txtMsg";
            txtMsg.Size = new Size(526, 23);
            txtMsg.TabIndex = 4;
            txtMsg.KeyPress += txtMsg_KeyPress;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 411);
            Controls.Add(txtMsg);
            Controls.Add(txtChatMsg);
            Controls.Add(label1);
            Controls.Add(txtName);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private TextBox txtName;
        private Label label1;
        private TextBox txtChatMsg;
        private TextBox txtMsg;
    }
}