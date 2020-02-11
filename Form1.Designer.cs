namespace Compiler_Project_Phase1
{
    partial class Form1
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
            this.txt_code = new System.Windows.Forms.TextBox();
            this.txt_token = new System.Windows.Forms.TextBox();
            this.btn_Compile_Textbox = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_file_address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_error = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_col = new System.Windows.Forms.Label();
            this.lbl_row = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txt_code
            // 
            this.txt_code.Location = new System.Drawing.Point(12, 33);
            this.txt_code.Multiline = true;
            this.txt_code.Name = "txt_code";
            this.txt_code.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_code.Size = new System.Drawing.Size(529, 459);
            this.txt_code.TabIndex = 0;
            this.txt_code.WordWrap = false;
            this.txt_code.CursorChanged += new System.EventHandler(this.txt_code_cursor_changed);
            // 
            // txt_token
            // 
            this.txt_token.Location = new System.Drawing.Point(547, 33);
            this.txt_token.Multiline = true;
            this.txt_token.Name = "txt_token";
            this.txt_token.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_token.Size = new System.Drawing.Size(529, 459);
            this.txt_token.TabIndex = 1;
            this.txt_token.WordWrap = false;
            // 
            // btn_Compile_Textbox
            // 
            this.btn_Compile_Textbox.Location = new System.Drawing.Point(131, 498);
            this.btn_Compile_Textbox.Name = "btn_Compile_Textbox";
            this.btn_Compile_Textbox.Size = new System.Drawing.Size(198, 92);
            this.btn_Compile_Textbox.TabIndex = 2;
            this.btn_Compile_Textbox.Text = "Compile Textbox";
            this.btn_Compile_Textbox.UseVisualStyleBackColor = true;
            this.btn_Compile_Textbox.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(335, 498);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(206, 92);
            this.button2.TabIndex = 3;
            this.button2.Text = "Compile File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_file_address
            // 
            this.txt_file_address.Location = new System.Drawing.Point(647, 533);
            this.txt_file_address.Name = "txt_file_address";
            this.txt_file_address.Size = new System.Drawing.Size(751, 22);
            this.txt_file_address.TabIndex = 5;
            this.txt_file_address.Text = "F:\\C# Projects\\Compiler Project\\Compiler Project Phase1\\Compiler Project Phase1\\F" +
    "iles\\ExternalCode.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(547, 538);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "File Address :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(547, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Token";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1404, 498);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(207, 92);
            this.button3.TabIndex = 9;
            this.button3.Text = "Choose File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_error
            // 
            this.txt_error.Location = new System.Drawing.Point(1082, 33);
            this.txt_error.Multiline = true;
            this.txt_error.Name = "txt_error";
            this.txt_error.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_error.Size = new System.Drawing.Size(529, 459);
            this.txt_error.TabIndex = 10;
            this.txt_error.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1079, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Error";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 517);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label5.Size = new System.Drawing.Size(61, 27);
            this.label5.TabIndex = 12;
            this.label5.Text = "column :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 554);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label6.Size = new System.Drawing.Size(58, 27);
            this.label6.TabIndex = 13;
            this.label6.Text = "row      :";
            // 
            // lbl_col
            // 
            this.lbl_col.AutoSize = true;
            this.lbl_col.Location = new System.Drawing.Point(79, 517);
            this.lbl_col.Name = "lbl_col";
            this.lbl_col.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lbl_col.Size = new System.Drawing.Size(46, 27);
            this.lbl_col.TabIndex = 14;
            this.lbl_col.Text = "label7";
            // 
            // lbl_row
            // 
            this.lbl_row.AutoSize = true;
            this.lbl_row.Location = new System.Drawing.Point(79, 554);
            this.lbl_row.Name = "lbl_row";
            this.lbl_row.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lbl_row.Size = new System.Drawing.Size(46, 27);
            this.lbl_row.TabIndex = 15;
            this.lbl_row.Text = "label8";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.txt_code_cursor_changed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1623, 602);
            this.Controls.Add(this.lbl_row);
            this.Controls.Add(this.lbl_col);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_error);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_file_address);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_Compile_Textbox);
            this.Controls.Add(this.txt_token);
            this.Controls.Add(this.txt_code);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_code;
        public System.Windows.Forms.TextBox txt_token;
        private System.Windows.Forms.Button btn_Compile_Textbox;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox txt_file_address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox txt_error;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_col;
        private System.Windows.Forms.Label lbl_row;
        private System.Windows.Forms.Timer timer1;
    }
}

