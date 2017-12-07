using System.Drawing;

namespace ControllerGamer
{
    partial class CreateProfileDetail
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_listen = new System.Windows.Forms.Button();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListBox_controllerlist = new System.Windows.Forms.CheckedListBox();
            this.textBox_targetprocess = new System.Windows.Forms.TextBox();
            this.button_lockunlock = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Reload = new System.Windows.Forms.Button();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSaveAsNew = new System.Windows.Forms.Button();
            this.buttonCreateNew = new System.Windows.Forms.Button();
            this.listViewProfile = new System.Windows.Forms.ListView();
            this.richTextBoxCSharpCode = new System.Windows.Forms.RichTextBox();
            this.pictureBox_profileicon = new System.Windows.Forms.PictureBox();
            this.buttonSaveCurrent = new System.Windows.Forms.Button();
            this.textBox_profilename = new System.Windows.Forms.TextBox();
            this.richTextBox_description = new System.Windows.Forms.RichTextBox();
            this.textBox_csharpsourcefilename = new System.Windows.Forms.TextBox();
            this.comboBox_controller = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label_Exit = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_profileicon)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button_listen);
            this.panel1.Controls.Add(this.textBox_port);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.checkedListBox_controllerlist);
            this.panel1.Controls.Add(this.textBox_targetprocess);
            this.panel1.Controls.Add(this.button_lockunlock);
            this.panel1.Controls.Add(this.button_Stop);
            this.panel1.Controls.Add(this.button_Reload);
            this.panel1.Controls.Add(this.richTextBox_log);
            this.panel1.Controls.Add(this.buttonApply);
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonSaveAsNew);
            this.panel1.Controls.Add(this.buttonCreateNew);
            this.panel1.Controls.Add(this.listViewProfile);
            this.panel1.Controls.Add(this.richTextBoxCSharpCode);
            this.panel1.Controls.Add(this.pictureBox_profileicon);
            this.panel1.Controls.Add(this.buttonSaveCurrent);
            this.panel1.Controls.Add(this.textBox_profilename);
            this.panel1.Controls.Add(this.richTextBox_description);
            this.panel1.Controls.Add(this.textBox_csharpsourcefilename);
            this.panel1.Controls.Add(this.comboBox_controller);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(62, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 829);
            this.panel1.TabIndex = 2;
            // 
            // button_listen
            // 
            this.button_listen.BackColor = System.Drawing.Color.Silver;
            this.button_listen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_listen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_listen.Location = new System.Drawing.Point(896, 696);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(144, 33);
            this.button_listen.TabIndex = 40;
            this.button_listen.Text = "Listen";
            this.button_listen.UseMnemonic = false;
            this.button_listen.UseVisualStyleBackColor = false;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // textBox_port
            // 
            this.textBox_port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBox_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.textBox_port.ForeColor = System.Drawing.Color.White;
            this.textBox_port.Location = new System.Drawing.Point(801, 701);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(83, 23);
            this.textBox_port.TabIndex = 39;
            this.textBox_port.Text = "2017";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(748, 700);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 24);
            this.label6.TabIndex = 38;
            this.label6.Text = "Port";
            // 
            // checkedListBox_controllerlist
            // 
            this.checkedListBox_controllerlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.checkedListBox_controllerlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox_controllerlist.ForeColor = System.Drawing.SystemColors.Window;
            this.checkedListBox_controllerlist.FormattingEnabled = true;
            this.checkedListBox_controllerlist.Location = new System.Drawing.Point(752, 278);
            this.checkedListBox_controllerlist.Name = "checkedListBox_controllerlist";
            this.checkedListBox_controllerlist.Size = new System.Drawing.Size(288, 235);
            this.checkedListBox_controllerlist.TabIndex = 37;
            this.checkedListBox_controllerlist.SelectedValueChanged += new System.EventHandler(this.checkedListBox_controllerlist_SelectedValueChanged);
            // 
            // textBox_targetprocess
            // 
            this.textBox_targetprocess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBox_targetprocess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_targetprocess.ForeColor = System.Drawing.Color.White;
            this.textBox_targetprocess.Location = new System.Drawing.Point(896, 158);
            this.textBox_targetprocess.Name = "textBox_targetprocess";
            this.textBox_targetprocess.ReadOnly = true;
            this.textBox_targetprocess.Size = new System.Drawing.Size(144, 23);
            this.textBox_targetprocess.TabIndex = 36;
            // 
            // button_lockunlock
            // 
            this.button_lockunlock.BackColor = System.Drawing.Color.Silver;
            this.button_lockunlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_lockunlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_lockunlock.Location = new System.Drawing.Point(745, 735);
            this.button_lockunlock.Name = "button_lockunlock";
            this.button_lockunlock.Size = new System.Drawing.Size(139, 33);
            this.button_lockunlock.TabIndex = 35;
            this.button_lockunlock.Text = "Unlock";
            this.button_lockunlock.UseMnemonic = false;
            this.button_lockunlock.UseVisualStyleBackColor = false;
            this.button_lockunlock.Click += new System.EventHandler(this.button_lockunlock_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.BackColor = System.Drawing.Color.Silver;
            this.button_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Stop.Location = new System.Drawing.Point(896, 735);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(144, 33);
            this.button_Stop.TabIndex = 34;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseMnemonic = false;
            this.button_Stop.UseVisualStyleBackColor = false;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // button_Reload
            // 
            this.button_Reload.BackColor = System.Drawing.Color.Silver;
            this.button_Reload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Reload.Location = new System.Drawing.Point(745, 774);
            this.button_Reload.Name = "button_Reload";
            this.button_Reload.Size = new System.Drawing.Size(139, 33);
            this.button_Reload.TabIndex = 33;
            this.button_Reload.Text = "Reload";
            this.button_Reload.UseVisualStyleBackColor = false;
            this.button_Reload.Click += new System.EventHandler(this.button_Reload_Click);
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.AcceptsTab = true;
            this.richTextBox_log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.richTextBox_log.DetectUrls = false;
            this.richTextBox_log.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_log.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBox_log.Location = new System.Drawing.Point(0, 532);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.ReadOnly = true;
            this.richTextBox_log.Size = new System.Drawing.Size(739, 275);
            this.richTextBox_log.TabIndex = 32;
            this.richTextBox_log.TabStop = false;
            this.richTextBox_log.Text = "";
            this.richTextBox_log.WordWrap = false;
            this.richTextBox_log.TextChanged += new System.EventHandler(this.richTextBox_log_TextChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.Silver;
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApply.Location = new System.Drawing.Point(896, 774);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(144, 33);
            this.buttonApply.TabIndex = 31;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseMnemonic = false;
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Silver;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.Location = new System.Drawing.Point(746, 46);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(144, 37);
            this.buttonDelete.TabIndex = 30;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Visible = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSaveAsNew
            // 
            this.buttonSaveAsNew.BackColor = System.Drawing.Color.Silver;
            this.buttonSaveAsNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveAsNew.Location = new System.Drawing.Point(896, 3);
            this.buttonSaveAsNew.Name = "buttonSaveAsNew";
            this.buttonSaveAsNew.Size = new System.Drawing.Size(144, 37);
            this.buttonSaveAsNew.TabIndex = 29;
            this.buttonSaveAsNew.Text = "Save as New";
            this.buttonSaveAsNew.UseVisualStyleBackColor = false;
            this.buttonSaveAsNew.Click += new System.EventHandler(this.buttonSaveAsNew_Click);
            // 
            // buttonCreateNew
            // 
            this.buttonCreateNew.BackColor = System.Drawing.Color.Silver;
            this.buttonCreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateNew.Location = new System.Drawing.Point(745, 3);
            this.buttonCreateNew.Name = "buttonCreateNew";
            this.buttonCreateNew.Size = new System.Drawing.Size(145, 37);
            this.buttonCreateNew.TabIndex = 28;
            this.buttonCreateNew.Text = "Create New";
            this.buttonCreateNew.UseVisualStyleBackColor = false;
            this.buttonCreateNew.Click += new System.EventHandler(this.buttonCreateNew_Click);
            // 
            // listViewProfile
            // 
            this.listViewProfile.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.listViewProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewProfile.ForeColor = System.Drawing.Color.White;
            this.listViewProfile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewProfile.HideSelection = false;
            this.listViewProfile.Location = new System.Drawing.Point(0, 0);
            this.listViewProfile.Margin = new System.Windows.Forms.Padding(5);
            this.listViewProfile.MultiSelect = false;
            this.listViewProfile.Name = "listViewProfile";
            this.listViewProfile.Size = new System.Drawing.Size(214, 526);
            this.listViewProfile.TabIndex = 27;
            this.listViewProfile.TileSize = new System.Drawing.Size(100, 300);
            this.listViewProfile.UseCompatibleStateImageBehavior = false;
            this.listViewProfile.View = System.Windows.Forms.View.List;
            this.listViewProfile.SelectedIndexChanged += new System.EventHandler(this.listViewProfile_SelectedIndexChanged);
            // 
            // richTextBoxCSharpCode
            // 
            this.richTextBoxCSharpCode.AcceptsTab = true;
            this.richTextBoxCSharpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.richTextBoxCSharpCode.DetectUrls = false;
            this.richTextBoxCSharpCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxCSharpCode.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBoxCSharpCode.Location = new System.Drawing.Point(223, -1);
            this.richTextBoxCSharpCode.Name = "richTextBoxCSharpCode";
            this.richTextBoxCSharpCode.ReadOnly = true;
            this.richTextBoxCSharpCode.Size = new System.Drawing.Size(516, 527);
            this.richTextBoxCSharpCode.TabIndex = 5;
            this.richTextBoxCSharpCode.TabStop = false;
            this.richTextBoxCSharpCode.Text = "";
            this.richTextBoxCSharpCode.WordWrap = false;
            // 
            // pictureBox_profileicon
            // 
            this.pictureBox_profileicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_profileicon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_profileicon.Image = global::ControllerGamer.Properties.Resources.icon;
            this.pictureBox_profileicon.Location = new System.Drawing.Point(752, 579);
            this.pictureBox_profileicon.Name = "pictureBox_profileicon";
            this.pictureBox_profileicon.Size = new System.Drawing.Size(100, 100);
            this.pictureBox_profileicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_profileicon.TabIndex = 23;
            this.pictureBox_profileicon.TabStop = false;
            this.pictureBox_profileicon.Click += new System.EventHandler(this.pictureBox_profileicon_Click);
            // 
            // buttonSaveCurrent
            // 
            this.buttonSaveCurrent.BackColor = System.Drawing.Color.Silver;
            this.buttonSaveCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveCurrent.Location = new System.Drawing.Point(896, 46);
            this.buttonSaveCurrent.Name = "buttonSaveCurrent";
            this.buttonSaveCurrent.Size = new System.Drawing.Size(144, 37);
            this.buttonSaveCurrent.TabIndex = 26;
            this.buttonSaveCurrent.Text = "Save";
            this.buttonSaveCurrent.UseVisualStyleBackColor = false;
            this.buttonSaveCurrent.Visible = false;
            this.buttonSaveCurrent.Click += new System.EventHandler(this.buttonSaveCurrent_Click);
            // 
            // textBox_profilename
            // 
            this.textBox_profilename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBox_profilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_profilename.ForeColor = System.Drawing.Color.White;
            this.textBox_profilename.Location = new System.Drawing.Point(896, 119);
            this.textBox_profilename.Name = "textBox_profilename";
            this.textBox_profilename.ReadOnly = true;
            this.textBox_profilename.Size = new System.Drawing.Size(144, 23);
            this.textBox_profilename.TabIndex = 1;
            // 
            // richTextBox_description
            // 
            this.richTextBox_description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.richTextBox_description.DetectUrls = false;
            this.richTextBox_description.ForeColor = System.Drawing.Color.White;
            this.richTextBox_description.Location = new System.Drawing.Point(896, 535);
            this.richTextBox_description.Name = "richTextBox_description";
            this.richTextBox_description.ReadOnly = true;
            this.richTextBox_description.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.richTextBox_description.Size = new System.Drawing.Size(144, 144);
            this.richTextBox_description.TabIndex = 5;
            this.richTextBox_description.Text = "";
            // 
            // textBox_csharpsourcefilename
            // 
            this.textBox_csharpsourcefilename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBox_csharpsourcefilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.textBox_csharpsourcefilename.ForeColor = System.Drawing.Color.White;
            this.textBox_csharpsourcefilename.Location = new System.Drawing.Point(896, 197);
            this.textBox_csharpsourcefilename.Name = "textBox_csharpsourcefilename";
            this.textBox_csharpsourcefilename.ReadOnly = true;
            this.textBox_csharpsourcefilename.Size = new System.Drawing.Size(144, 23);
            this.textBox_csharpsourcefilename.TabIndex = 3;
            // 
            // comboBox_controller
            // 
            this.comboBox_controller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.comboBox_controller.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_controller.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.comboBox_controller.ForeColor = System.Drawing.Color.White;
            this.comboBox_controller.FormattingEnabled = true;
            this.comboBox_controller.Location = new System.Drawing.Point(896, 236);
            this.comboBox_controller.Name = "comboBox_controller";
            this.comboBox_controller.Size = new System.Drawing.Size(144, 25);
            this.comboBox_controller.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(747, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Profile Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(747, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "Target Process";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(747, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "C# File Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(747, 535);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 24);
            this.label5.TabIndex = 21;
            this.label5.Text = "Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(747, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 24);
            this.label4.TabIndex = 20;
            this.label4.Text = "Support Devices";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label_Exit
            // 
            this.label_Exit.BackColor = System.Drawing.Color.DarkBlue;
            this.label_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Exit.ForeColor = System.Drawing.Color.White;
            this.label_Exit.Location = new System.Drawing.Point(1159, 9);
            this.label_Exit.Name = "label_Exit";
            this.label_Exit.Size = new System.Drawing.Size(39, 37);
            this.label_Exit.TabIndex = 3;
            this.label_Exit.Text = "X";
            this.label_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Exit.Click += new System.EventHandler(this.label_Exit_Click);
            // 
            // CreateProfileDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::ControllerGamer.Properties.Resources.formbg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1210, 911);
            this.Controls.Add(this.label_Exit);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateProfileDetail";
            this.Text = "CreateProfileDetail";
            this.Load += new System.EventHandler(this.CreateProfileDetail_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CreateProfileDetail_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CreateProfileDetail_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CreateProfileDetail_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_profileicon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox_profileicon;
        private System.Windows.Forms.Button buttonSaveCurrent;
        private System.Windows.Forms.TextBox textBox_profilename;
        private System.Windows.Forms.RichTextBox richTextBox_description;
        private System.Windows.Forms.TextBox textBox_csharpsourcefilename;
        private System.Windows.Forms.ComboBox comboBox_controller;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBoxCSharpCode;
        private System.Windows.Forms.ListView listViewProfile;
        private System.Windows.Forms.Button buttonSaveAsNew;
        private System.Windows.Forms.Button buttonCreateNew;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.Button button_Reload;
        private System.Windows.Forms.Label label_Exit;
        private System.Windows.Forms.Button button_lockunlock;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.TextBox textBox_targetprocess;
        private System.Windows.Forms.CheckedListBox checkedListBox_controllerlist;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label6;
    }
}