namespace WinFormsTcpChatServer
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
            btnStart = new Button();
            txtChatMsg = new TextBox();
            lblMsg = new Label();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(422, 339);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(133, 45);
            btnStart.TabIndex = 0;
            btnStart.Text = "서버 시작";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtChatMsg
            // 
            txtChatMsg.Location = new Point(32, 27);
            txtChatMsg.Multiline = true;
            txtChatMsg.Name = "txtChatMsg";
            txtChatMsg.ScrollBars = ScrollBars.Vertical;
            txtChatMsg.Size = new Size(523, 293);
            txtChatMsg.TabIndex = 1;
            // 
            // lblMsg
            // 
            lblMsg.AutoSize = true;
            lblMsg.Location = new Point(32, 339);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new Size(84, 15);
            lblMsg.TabIndex = 2;
            lblMsg.Tag = "Stop";
            lblMsg.Text = "Server 중지 됨";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 411);
            Controls.Add(lblMsg);
            Controls.Add(txtChatMsg);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private TextBox txtChatMsg;
        private Label lblMsg;
    }
}