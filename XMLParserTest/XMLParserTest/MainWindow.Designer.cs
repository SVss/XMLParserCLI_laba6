namespace XMLParserTest
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxDocTree = new System.Windows.Forms.GroupBox();
            this.treeViewDoc = new System.Windows.Forms.TreeView();
            this.groupBoxDocument = new System.Windows.Forms.GroupBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.openFileDialogInput = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonParse = new System.Windows.Forms.Button();
            this.splitContainerFields = new System.Windows.Forms.SplitContainer();
            this.saveFileDialogOutput = new System.Windows.Forms.SaveFileDialog();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxDocTree.SuspendLayout();
            this.groupBoxDocument.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainerFields.Panel1.SuspendLayout();
            this.splitContainerFields.Panel2.SuspendLayout();
            this.splitContainerFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDocTree
            // 
            this.groupBoxDocTree.AutoSize = true;
            this.groupBoxDocTree.Controls.Add(this.treeViewDoc);
            this.groupBoxDocTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDocTree.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDocTree.Name = "groupBoxDocTree";
            this.groupBoxDocTree.Size = new System.Drawing.Size(228, 314);
            this.groupBoxDocTree.TabIndex = 2;
            this.groupBoxDocTree.TabStop = false;
            this.groupBoxDocTree.Text = "Document Tree:";
            // 
            // treeViewDoc
            // 
            this.treeViewDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDoc.Location = new System.Drawing.Point(3, 16);
            this.treeViewDoc.Name = "treeViewDoc";
            this.treeViewDoc.Size = new System.Drawing.Size(222, 295);
            this.treeViewDoc.TabIndex = 0;
            // 
            // groupBoxDocument
            // 
            this.groupBoxDocument.AutoSize = true;
            this.groupBoxDocument.Controls.Add(this.textBoxInput);
            this.groupBoxDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDocument.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDocument.Name = "groupBoxDocument";
            this.groupBoxDocument.Size = new System.Drawing.Size(245, 314);
            this.groupBoxDocument.TabIndex = 3;
            this.groupBoxDocument.TabStop = false;
            this.groupBoxDocument.Text = "Document:";
            // 
            // textBoxInput
            // 
            this.textBoxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxInput.Location = new System.Drawing.Point(3, 16);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInput.Size = new System.Drawing.Size(239, 295);
            this.textBoxInput.TabIndex = 0;
            // 
            // openFileDialogInput
            // 
            this.openFileDialogInput.DefaultExt = "xml";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonProcess);
            this.panel1.Controls.Add(this.buttonOpenFile);
            this.panel1.Controls.Add(this.buttonParse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 314);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(477, 32);
            this.panel1.TabIndex = 6;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(390, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProcess.Location = new System.Drawing.Point(309, 3);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonProcess.TabIndex = 9;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenFile.Location = new System.Drawing.Point(12, 3);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenFile.TabIndex = 8;
            this.buttonOpenFile.Text = "Open";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonParse
            // 
            this.buttonParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonParse.Location = new System.Drawing.Point(93, 3);
            this.buttonParse.Name = "buttonParse";
            this.buttonParse.Size = new System.Drawing.Size(75, 23);
            this.buttonParse.TabIndex = 7;
            this.buttonParse.Text = "Parse";
            this.buttonParse.UseVisualStyleBackColor = true;
            this.buttonParse.Click += new System.EventHandler(this.buttonParse_Click);
            // 
            // splitContainerFields
            // 
            this.splitContainerFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFields.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFields.Name = "splitContainerFields";
            // 
            // splitContainerFields.Panel1
            // 
            this.splitContainerFields.Panel1.Controls.Add(this.groupBoxDocument);
            // 
            // splitContainerFields.Panel2
            // 
            this.splitContainerFields.Panel2.Controls.Add(this.groupBoxDocTree);
            this.splitContainerFields.Size = new System.Drawing.Size(477, 314);
            this.splitContainerFields.SplitterDistance = 245;
            this.splitContainerFields.TabIndex = 7;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClear.Location = new System.Drawing.Point(197, 3);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 346);
            this.Controls.Add(this.splitContainerFields);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(493, 384);
            this.Name = "MainWindow";
            this.Text = "Test XML Parser";
            this.groupBoxDocTree.ResumeLayout(false);
            this.groupBoxDocument.ResumeLayout(false);
            this.groupBoxDocument.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainerFields.Panel1.ResumeLayout(false);
            this.splitContainerFields.Panel1.PerformLayout();
            this.splitContainerFields.Panel2.ResumeLayout(false);
            this.splitContainerFields.Panel2.PerformLayout();
            this.splitContainerFields.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDocTree;
        private System.Windows.Forms.TreeView treeViewDoc;
        private System.Windows.Forms.GroupBox groupBoxDocument;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.OpenFileDialog openFileDialogInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonParse;
        private System.Windows.Forms.SplitContainer splitContainerFields;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.SaveFileDialog saveFileDialogOutput;
        private System.Windows.Forms.Button buttonClear;
    }
}

